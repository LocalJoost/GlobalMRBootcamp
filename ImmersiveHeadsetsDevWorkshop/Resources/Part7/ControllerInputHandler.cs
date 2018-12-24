using HoloToolkit.Unity.InputModule;
using HoloToolkitExtensions.Messaging;
using UnityEngine;

public class ControllerInputHandler : MonoBehaviour, IInputHandler
{
    public void OnInputDown(InputEventData eventData)
    {
        switch (eventData.PressType)
        {
            case InteractionSourcePressInfo.Grasp:
                Messenger.Instance.Broadcast(new CommandMessage(CommandType.Move));
                break;
         }
    }

    public void OnInputUp(InputEventData eventData)
    {
        if (eventData.PressType == InteractionSourcePressInfo.Grasp)

        {
            Messenger.Instance.Broadcast(new CommandMessage(CommandType.None));
        }
    }
}
