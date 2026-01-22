using Zenject;
using UnityEngine;

public class GameController : IInitializable, IGameController
{
    private readonly IBoardView boardView;
    private readonly ISpawnService spawnService;
    private readonly IDragDropHandler dragDropHandler;
    private readonly IMergeService mergeService;
    private readonly IMessageService messageService;
    private Board boardData;

    public GameController(
        IBoardView boardView, 
        ISpawnService spawnService,
        IDragDropHandler dragDropHandler,
        IMergeService mergeService,
        IMessageService messageService)
    {
        this.boardView = boardView;
        this.spawnService = spawnService;
        this.dragDropHandler = dragDropHandler;
        this.mergeService = mergeService;
        this.messageService = messageService;

        SubscribeToDragDropEvents();
    }
    private void SubscribeToDragDropEvents()
    {
        dragDropHandler.OnDragCompleted += HandleDragCompleted;
        dragDropHandler.OnDragCancelled += HandleDragCancelled;
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

    private void HandleDragCompleted(ICellView sourceCellView, ICellView targetCellView)
    {
        if (sourceCellView == null || targetCellView == null)
            return;

        if (sourceCellView == targetCellView)
        {
            dragDropHandler.CancelDrag();
            return;
        }
            

        var sourceCell = sourceCellView.CellData;
        var targetCell = targetCellView.CellData;

        if (targetCell.IsEmpty)
        {
            MoveChip(sourceCell, targetCell, sourceCellView, targetCellView);
        }

        else if (mergeService.TryMerge(sourceCell, targetCell, (mergedCell, mergedChip) =>
        {
            HandleMergeAnimation(sourceCellView, targetCellView, mergedChip);
        }))
        {
            // Слияние обрабатывается в колбэке
        }
        else
        {
            dragDropHandler.CancelDrag();
            messageService.ShowMessage("Cannot merge different types!");
        }
    }

    private void MoveChip(Cell fromCell, Cell toCell, ICellView fromView, ICellView toView)
    {
        var chip = fromCell.Clear();
        toCell.TryOccupy(chip);

        var chipView = fromView.RemoveChip();
        toView.SetChip(chipView);

        chipView.PlayMergeAnimation(() =>
        {
            chipView.SetPosition(toView.WorldPosition);
        });
    }

    private void HandleMergeAnimation(ICellView sourceView, ICellView targetView, Chip mergedChip)
    {
        var sourceChipView = sourceView.CurrentChipView;
        var targetChipView = targetView.CurrentChipView;

        // Проигрываем анимацию исчезновения для обеих фишек
        sourceChipView.PlayDisappearAnimation(() =>
        {
            boardView.DestroyChipView(sourceChipView);
        });

        targetChipView.PlayDisappearAnimation(() =>
        {
            boardView.DestroyChipView(targetChipView);

            // Создаем новую объединенную фишку
            var mergedChipView = boardView.CreateChipView(mergedChip);
            targetView.SetChip(mergedChipView);


            mergedChipView.SetVisible(true);

            mergedChipView.PlayMergeAnimation();
        });
    }

    private void HandleDragCancelled()
    {
        
    }

    public bool TryMoveChip(Cell fromCell, Cell toCell)
    {
        // Реализация для прямого перемещения (например, по кнопке)
        if (fromCell == null || toCell == null || fromCell.IsEmpty)
            return false;

        if (toCell.IsEmpty)
        {
            MoveChip(
                fromCell,
                toCell,
                boardView.GetCellView(fromCell),
                boardView.GetCellView(toCell)
            );
            return true;
        }

        return false;
    }
}