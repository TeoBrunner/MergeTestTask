using UnityEngine;

public class Cell
{
    public Vector2Int Position { get; private set; }
    public Chip CurrentChip { get; private set; }
    public bool IsEmpty => CurrentChip == null;
    public Cell(Vector2Int position, Chip chip = null)
    {
        Position = position;
        CurrentChip = chip;
    }
    public Cell(int x, int y, Chip chip = null)
    {
        Position = new Vector2Int(x, y);
        CurrentChip = chip;
    }
    public bool TryOccupy(Chip chip)
    {
        if (CanOccupy(chip))
        {
            Occupy(chip);
            return true;
        }
        return false;
    }
    public bool CanOccupy(Chip chip)
    {
        return IsEmpty;
    }
    private void Occupy(Chip chip)
    {
        CurrentChip = chip;
    }
    public Chip Clear()
    {
        var chip = CurrentChip;
        CurrentChip = null;
        return chip;
    }
}