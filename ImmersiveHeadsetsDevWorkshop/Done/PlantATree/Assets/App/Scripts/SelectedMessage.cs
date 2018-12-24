using UnityEngine;

public class SelectedMessage
{
    public GameObject SelectedObject { get; private set; }
    public SelectedMessage(GameObject selectedGameObject)
    {
        SelectedObject = selectedGameObject;
    }
}
