using System.Collections.Generic;
using HoloToolkit.Unity;
using HoloToolkitExtensions.Messaging;
using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _objectsToPlace = new List<GameObject>();

    [SerializeField]
    private GameObject _container;

    [SerializeField]
    private float _scale = 0.07f;

    [SerializeField]
    private AudioClip _selectAudioClip;

    [SerializeField]
    private float _initialDistance = 2;

    [SerializeField]
    private float _deltaZ = 0f;

    private void Start()
    {
        Messenger.Instance.AddListener<CommandMessage>(CommandMessageHandler);
    }

    private void CommandMessageHandler(CommandMessage cmd)
    {
        switch (cmd.Command)
        {
            case CommandType.Place1:
                PlaceObject(0);
                break;
            case CommandType.Place2:
                PlaceObject(1);
                break;
            case CommandType.Place3:
                PlaceObject(2);
                break;
            case CommandType.Place4:
                PlaceObject(3);
                break;
        }
    }

    private void PlaceObject(int objIndex)
    {
        if (_objectsToPlace.Count > objIndex)
        {
            var newObj = Instantiate(_objectsToPlace[objIndex], 
                CalculatePositionDeadAhead(_initialDistance) + Vector3.up *_deltaZ, 
                Quaternion.identity, _container.transform);
            newObj.transform.localScale *= _scale;
            var audioSource = newObj.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = _selectAudioClip;
            var rigidBody = newObj.GetComponent<Rigidbody>();
            if (rigidBody == null)
            {
                rigidBody = newObj.AddComponent<Rigidbody>();
            }
            rigidBody.useGravity = false;

            if (newObj.GetComponent<Collider>() == null)
            {
                newObj.AddComponent<CapsuleCollider>();
            }

            newObj.AddComponent<ObjectManipulator>();

        }
    }



    private static Vector3 CalculatePositionDeadAhead(float distance = 2)
    {
        return CameraCache.Main.transform.position + CameraCache.Main.transform.forward.normalized * distance;
    }
}
