using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CellView : MonoBehaviour, ICellView
{
    [Header("UI Components")]
    [SerializeField] private Image background;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightColor = Color.yellow;

    private Cell cellData;
    private IChipView currentChipView;
    private RectTransform rectTransform;

    public Cell CellData => cellData;
    public IChipView CurrentChipView => currentChipView;
    public Vector2Int GridPosition => cellData.Position;
    public Vector3 WorldPosition => rectTransform.position;
    public RectTransform RectTransform => rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        SetHighlight(false);
    }

    public void Initialize(Cell cell)
    {
        cellData = cell;
        
        gameObject.name = $"Cell_{cell.Position.x}_{cell.Position.y}";
    }

    public bool CanAcceptChip(Chip chip)
    {

        if (cellData.IsEmpty) return true;

        return cellData.CurrentChip.CanMergeWith(chip);
    }

    public void SetChip(IChipView chipView)
    {
        currentChipView = chipView;
        
        if (chipView != null)
        {
            chipView.RectTransform.SetParent(rectTransform);
            chipView.RectTransform.anchoredPosition = Vector2.zero;
        }
    }

    public IChipView RemoveChip()
    {
        var chip = currentChipView;
        currentChipView = null;
        return chip;
    }

    public void SetHighlight(bool highlighted)
    {
        if (background != null)
        {
            background.color = highlighted ? highlightColor : normalColor;
        }
    }

    public class Factory : PlaceholderFactory<CellView> { }
}