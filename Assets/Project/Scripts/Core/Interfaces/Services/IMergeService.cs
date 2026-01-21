using System;

public interface IMergeService
{
    bool CanMerge(Chip chip1, Chip chip2);
    bool TryMerge(Cell sourceCell, Cell targetCell, Action<Cell, Chip> onMerged = null);
}