using System;
using UnityEngine;

public interface IBoardView
{
    Board BoardData { get; }
    void Initialize(Board board);
    ICellView GetCellView(Cell cellData);
    ICellView GetCellView(int x, int y);
    ICellView GetCellView(Vector2Int position);
    IChipView CreateChipView(Chip chip);
    void DestroyChipView(IChipView chipView);
    void RefreshAll();
}