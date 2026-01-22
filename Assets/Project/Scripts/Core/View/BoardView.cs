using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoardView : MonoBehaviour, IBoardView
{
    [Header("Settings")]
    [SerializeField] private float cellSpacing = 100f;

    [Header("Prefabs")]
    [SerializeField] private RectTransform cellsParent; 
    [SerializeField] private RectTransform chipsParent; 

    private ICellViewFactory cellFactory;
    private IChipViewFactory chipFactory;

    private Board boardData;
    private Dictionary<Cell, ICellView> cellMap = new Dictionary<Cell, ICellView>();

    public Board BoardData => boardData;

    [Inject]
    public void Construct(ICellViewFactory cellFactory, IChipViewFactory chipFactory)
    {
        this.cellFactory = cellFactory;
        this.chipFactory = chipFactory;
    }

    public void Initialize(Board board)
    {
        this.boardData = board;
        ClearBoard();

        for (int y = 0; y < board.Height; y++)
        {
            for (int x = 0; x < board.Width; x++)
            {
                Cell cell = board.GetCell(x, y);
                CreateCellView(cell);
            }
        }
    }

    private void CreateCellView(Cell cell)
    {
        var cellView = cellFactory.Create(cell, cellsParent);

        Vector2 position = new Vector2(
            (cell.Position.x - 1) * cellSpacing,
            (cell.Position.y - 1) * cellSpacing
        );
        cellView.RectTransform.anchoredPosition = position;

        cellMap.Add(cell, cellView);
    }

    public ICellView GetCellView(Cell cellData)
    {
        return cellMap.TryGetValue(cellData, out var view) ? view : null;
    }

    public ICellView GetCellView(int x, int y)
    {
        var cell = boardData.GetCell(x, y);
        return cell != null ? GetCellView(cell) : null;
    }

    public ICellView GetCellView(Vector2Int position)
    {
        return GetCellView(position.x, position.y);
    }

    public IChipView CreateChipView(Chip chip)
    {
        return chipFactory.Create(chip, chipsParent);
    }

    public void DestroyChipView(IChipView chipView)
    {
        if (chipView != null && chipView is MonoBehaviour mb)
        {
            Destroy(mb.gameObject);
        }
    }

    public void RefreshAll()
    {
        foreach (var pair in cellMap)
        {

        }
    }

    private void ClearBoard()
    {
        foreach (Transform child in cellsParent) Destroy(child.gameObject);
        foreach (Transform child in chipsParent) Destroy(child.gameObject);
        cellMap.Clear();
    }
}