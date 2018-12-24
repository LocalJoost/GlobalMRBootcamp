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
            case InteractionSourcePressInfo.Menu:
                Messenger.Instance.Broadcast(new CommandMessage(CommandType.Rotate));
                break;
            case InteractionSourcePressInfo.Touchpad:
                Messenger.Instance.Broadcast(new CommandMessage(CommandType.Scale));
                break;
        }
    }

    public void OnInputUp(InputEventData eventData)
    {
        if (eventData.PressType == InteractionSourcePressInfo.Grasp || 
            eventData.PressType == InteractionSourcePressInfo.Menu ||
            eventData.PressType == InteractionSourcePressInfo.Touchpad)
        {
            Messenger.Instance.Broadcast(new CommandMessage(CommandType.None));
        }
    }
}
