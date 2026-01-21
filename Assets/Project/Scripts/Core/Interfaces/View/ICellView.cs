using System;
using UnityEngine;

public interface ICellView
{
    Cell CellData { get; }
    IChipView CurrentChipView { get; }
    Vector2Int GridPosition { get; }
    Vector3 WorldPosition { get; }
    RectTransform RectTransform { get; }
    void Initialize(Cell cell);
    bool CanAcceptChip(Chip chip);
    void SetChip(IChipView chipView);
    IChipView RemoveChip();
    void SetHighlight(bool highlighted);

}