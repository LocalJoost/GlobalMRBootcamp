using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using HoloToolkitExtensions.Messaging;
using HoloToolkitExtensions.Utilities;
using UnityEngine;

public class ObjectManipulator : MonoBehaviour, IInputClickHandler, IManipulationHandler
{
    private CommandType _activeManipulationCommand;

    private bool _isSelected;
    private AudioSource _sound;

    private void Start()
    {
        Messenger.Instance.AddListener<CommandMessage>(CommandMessageHandler);
        Messenger.Instance.AddListener<SelectedMessage>(SelectMessageMandler);
        _sound = GetComponent<AudioSource>();
    }

    private void SelectMessageMandler(SelectedMessage msg)
    {
        _isSelected = msg.SelectedObject == gameObject;
        if (!_isSelected)
        {
            _activeManipulationCommand = CommandType.None;
        }
    }

    private void CommandMessageHandler(CommandMessage cmd)
    {
        if (_isSelected)
        {
            switch (cmd.Command)
            {
                case CommandType.None:
                    _activeManipulationCommand = cmd.Command;
                    break;
                case CommandType.Move:     
                case CommandType.Rotate:
                case CommandType.Scale:
                    _activeManipulationCommand = cmd.Command;
                    PlayConfirmationSound(cmd);
                    break;
                case CommandType.Delete:
                    PlayConfirmationSound(cmd);
                    Destroy(gameObject);
                    break;
            }
        }
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        TryPlaySound();
        Messenger.Instance.Broadcast(new SelectedMessage(gameObject));
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        if (_isSelected)
        {
            switch (_activeManipulationCommand)
            {
                case CommandType.Move:
                    transform.position += eventData.CumulativeDelta / 30f;
                    break;
                case CommandType.Rotate:
                    var rotFix = CameraCache.Main.transform.position.z - transform.position.z;
                    transform.RotateAround(transform.transform.position, transform.up,
                        Mathf.Sign(rotFix) * eventData.CumulativeDelta.x * 10f);
                    break;
                case CommandType.Scale:
                    transform.localScale *= 1.0f + (eventData.CumulativeDelta.y * 0.1f);
                    break;
            }
        }
    }


    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
    }

    private void PlayConfirmationSound(CommandMessage command)
    {
        if (!command.Silent)
        {
            Messenger.Instance.Broadcast(new ConfirmSoundMessage());
        }
    }

    private void TryPlaySound()
    {
        if (_sound != null && _sound.clip != null)
        {
            _sound.Play();
        }
    }

    private void OnDestroy()
    {
        Messenger.Instance.RemoveListener<CommandMessage>(CommandMessageHandler);
        Messenger.Instance.RemoveListener<SelectedMessage>(SelectMessageMandler);
    }
}
