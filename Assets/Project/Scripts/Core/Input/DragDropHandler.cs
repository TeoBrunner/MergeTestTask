using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class DragDropHandler : IDragDropHandler
{
    public event Action<ICellView> OnDragStarted;
    public event Action<ICellView, ICellView> OnDragCompleted;
    public event Action OnDragCancelled;

    public bool IsDragging { get; private set; }
    public IChipView DraggedChipView { get; private set; }
    public ICellView SourceCell { get; private set; }

    private readonly IInputHandler inputHandler;
    private readonly IViewRaycaster viewRaycaster;
    private Canvas parentCanvas;
    private Vector3 dragOffset;

    public DragDropHandler(
        IInputHandler inputHandler,
        IViewRaycaster viewRaycaster,
        Canvas canvas)
    {
        this.inputHandler = inputHandler;
        this.viewRaycaster = viewRaycaster;
        this.parentCanvas = canvas;

        SubscribeToInput();
    }

    private void SubscribeToInput()
    {
        inputHandler.OnPointerDown += HandlePointerDown;
        inputHandler.OnPointerDrag += HandlePointerDrag;
        inputHandler.OnPointerUp += HandlePointerUp;
    }

    public bool TryStartDrag(ICellView cellView)
    {
        if (cellView == null || cellView.CurrentChipView == null || IsDragging)
            return false;

        SourceCell = cellView;
        DraggedChipView = cellView.CurrentChipView;

        Vector3 worldPointerPosition = GetWorldPosition(inputHandler.CurrentPointerPosition);

        dragOffset = DraggedChipView.RectTransform.position - worldPointerPosition;

        IsDragging = true;
        OnDragStarted?.Invoke(SourceCell);

        DisableRaycastForDraggedChip();

        SourceCell.RectTransform.SetAsLastSibling();

        return true;
    }

    private void HandlePointerDown(Vector2 screenPosition)
    {
        if (IsDragging) return;

        var cellUnderPointer = viewRaycaster.GetCellAtScreenPosition(screenPosition);
        if (cellUnderPointer != null && cellUnderPointer.CurrentChipView != null)
        {
            TryStartDrag(cellUnderPointer);
        }
    }

    private void HandlePointerDrag(Vector2 screenPosition)
    {
        if (!IsDragging || DraggedChipView == null) return;

        UpdateDragPosition(screenPosition);
    }

    public void UpdateDragPosition(Vector3 screenPosition)
    {
        if (!IsDragging || DraggedChipView == null) return;

        Vector3 worldPointerPosition = GetWorldPosition(screenPosition);

        DraggedChipView.RectTransform.position = worldPointerPosition + dragOffset;
    }

    private void HandlePointerUp(Vector2 screenPosition)
    {
        if (!IsDragging) return;

        var targetCell = viewRaycaster.GetCellAtScreenPosition(screenPosition);
        if (targetCell != null)
        {
            CompleteDrag(targetCell);
            targetCell.RectTransform.SetAsLastSibling();
        }
        else
        {
            CancelDrag();
        }
    }

    public void CompleteDrag(ICellView targetCell)
    {
        if (!IsDragging) return;

        OnDragCompleted?.Invoke(SourceCell, targetCell);
        ResetDragState();
    }

    public void CancelDrag()
    {
        if (!IsDragging) return;

        OnDragCancelled?.Invoke();

        if (DraggedChipView != null && SourceCell != null)
        {
            EnableRaycastForDraggedChip();
            DraggedChipView.SetPosition(SourceCell.WorldPosition);
        }
        ResetDragState();
    }

    private Vector3 GetWorldPosition(Vector2 screenPosition)
    {
        float distanceToCanvas = (parentCanvas.transform.position - parentCanvas.worldCamera.transform.position).magnitude;
        return parentCanvas.worldCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, distanceToCanvas));

    }

    private void DisableRaycastForDraggedChip()
    {
        var canvasGroup = DraggedChipView.RectTransform.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;
    }

    private void EnableRaycastForDraggedChip()
    {
        var canvasGroup = DraggedChipView.RectTransform.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;
    }

    private void ResetDragState()
    {
        IsDragging = false;
        DraggedChipView = null;
        SourceCell = null;
        dragOffset = Vector3.zero;
    }
}