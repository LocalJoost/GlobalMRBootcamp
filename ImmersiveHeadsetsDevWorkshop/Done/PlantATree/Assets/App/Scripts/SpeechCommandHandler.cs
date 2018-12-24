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

    public void Place2()
    {
        SendMessage(CommandType.Place2);
        PlaySound();
    }

    public void Place3()
    {
        SendMessage(CommandType.Place3);
        PlaySound();
    }

    public void Place4()
    {
        SendMessage(CommandType.Place4);
        PlaySound();
    }

    public void Move()
    {
        SendMessage(CommandType.Move);
    }

    public void Rotate()
    {
        SendMessage(CommandType.Rotate);
    }

    public void Scale()
    {
        SendMessage(CommandType.Scale);
    }

    public void Delete()
    {
        SendMessage(CommandType.Delete);
    }

    private void SendMessage(CommandType cmdType)
    {
        Messenger.Instance.Broadcast(new CommandMessage(cmdType, false));
    }

    private void PlaySound()
    {
        Messenger.Instance.Broadcast( new ConfirmSoundMessage());
    }
}
