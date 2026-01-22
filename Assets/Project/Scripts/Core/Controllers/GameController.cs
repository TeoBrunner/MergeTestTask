using Zenject;
using UnityEngine;

public class GameController : IInitializable, IGameController
{
    private readonly IBoardView boardView;
    private readonly ISpawnService spawnService;
    private Board boardData;

    public GameController(IBoardView boardView, ISpawnService spawnService)
    {
        this.boardView = boardView;
        this.spawnService = spawnService;
    }

    public void Initialize()
    {
        boardData = new Board(3, 3);

        boardView.Initialize(boardData);
    }

    public void OnSpawnButtonClicked()
    {
        spawnService.TrySpawn(boardData, (cell, chip) =>
        {
            var chipView = boardView.CreateChipView(chip);

            var cellView = boardView.GetCellView(cell);
            cellView.SetChip(chipView);

            chipView.PlaySpawnAnimation();
        });
    }

    public bool TryMoveChip(Cell fromCell, Cell toCell) => false;
}