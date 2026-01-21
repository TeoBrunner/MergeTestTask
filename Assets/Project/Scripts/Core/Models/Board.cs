using System.Collections.Generic;
using UnityEngine;
public class Board
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    private Cell[,] cells;
    public Board(int width = 3, int height = 3)
    {
        Width = width;
        Height = height;
        cells = new Cell[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x, y] = new Cell(x, y);
            }
        }
    }
    public Cell GetCell(int x, int y)
    {
        if (!IsValidPosition(x, y))
        {
            Debug.LogWarning($"Invalid cell position: ({x}, {y})");
            return null;
        }
        return cells[x, y];
    }
    public Cell GetCell(Vector2Int position)
    {
        return GetCell(position.x, position.y);
    }
    public bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }
    public bool HasEmptyCells => GetEmptyCells().Count > 0;
    public List<Cell> GetEmptyCells()
    {
        var emptyCells = new List<Cell>();
        foreach (var cell in cells)
        {
            if (cell.IsEmpty)
                emptyCells.Add(cell);
        }
        return emptyCells;
    }
    public int GetChipCount()
    {
        int count = 0;
        foreach (var cell in cells)
        {
            if (!cell.IsEmpty)
                count++;
        }
        return count;
    }

}