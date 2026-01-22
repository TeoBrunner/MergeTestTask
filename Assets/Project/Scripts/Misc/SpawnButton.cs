using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnButton : MonoBehaviour
{
    private IGameController gameController;
    [Inject]
    private void Construct(IGameController gameController)
    {
        this.gameController = gameController;
    }
    public void OnSpawnButton()
    {
        gameController.OnSpawnButtonClicked();
    }
}
