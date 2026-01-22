using Zenject;
using UnityEngine;

public class GameController : IInitializable, IGameController
{
    private readonly IBoardView boardView;
    private readonly ISpawnService spawnService;
    private readonly IMessageService messageService;
    private Board boardData;

    public GameController(
        IBoardView boardView, 
        ISpawnService spawnService,
        IMessageService messageService)
    {
        this.boardView = boardView;
        this.spawnService = spawnService;
        this.messageService = messageService;
    }

    public void Initialize()
    {
        boardData = new Board(3, 3);

        boardView.Initialize(boardData);
    }

    public void OnSpawnButtonClicked()
    {
        bool spawned = spawnService.TrySpawn(boardData, (cell, chip) =>
        {
            var chipView = boardView.CreateChipView(chip);

            var cellView = boardView.GetCellView(cell);
            cellView.SetChip(chipView);

            chipView.PlaySpawnAnimation();
        });
        if (!spawned)
        {
            messageService.ShowMessage("There are no empty cells!");
        }
    }

    public bool TryMoveChip(Cell fromCell, Cell toCell) => false;
}