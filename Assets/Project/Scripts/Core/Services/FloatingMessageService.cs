using UnityEngine;
using Zenject;

public class FloatingMessageService : IMessageService
{
    private readonly FloatingMessageView.Factory messageFactory;
    private readonly Canvas mainCanvas;

    private const float MESSAGE_SPAWN_Y = -200f;
    public FloatingMessageService(FloatingMessageView.Factory messageFactory, Canvas canvas)
    {
        this.messageFactory = messageFactory;
        this.mainCanvas = canvas;
    }

    public void ShowMessage(string message)
    {
        var messageInstance = messageFactory.Create();

        messageInstance.transform.SetParent(mainCanvas.transform, false);

        messageInstance.transform.localPosition = new Vector3(0, MESSAGE_SPAWN_Y, 0);

        messageInstance.Initialize(message);
    }
}