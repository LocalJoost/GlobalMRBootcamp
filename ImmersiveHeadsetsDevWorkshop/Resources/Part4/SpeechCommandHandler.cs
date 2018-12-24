using HoloToolkitExtensions.Messaging;
using HoloToolkitExtensions.Utilities;
using UnityEngine;

public class SpeechCommandHandler : MonoBehaviour
{
    public void Place1()
    {
        SendMessage(CommandType.Place1);
        PlaySound();
    }

 
    private void SendMessage(CommandType cmdType)
    {
        Messenger.Instance.Broadcast(new CommandMessage(cmdType, false));
    }

    private void PlaySound()
    {
        
    }
}
