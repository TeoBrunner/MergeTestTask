using System;
using UnityEngine;


public interface IDragDropHandler
{
    event Action<ICellView> OnDragStarted;
    event Action<ICellView, ICellView> OnDragCompleted;
    event Action OnDragCancelled;
    bool IsDragging { get; }
    IChipView DraggedChipView { get; }
    ICellView SourceCell { get; }
    bool TryStartDrag(ICellView cellView);
    void UpdateDragPosition(Vector3 worldPosition);
    void CompleteDrag(ICellView targetCell);
    void CancelDrag();
}