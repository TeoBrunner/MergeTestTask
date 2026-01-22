using System;
using UnityEngine;

public class MergeService : IMergeService
{
    public bool CanMerge(Chip chip1, Chip chip2)
    {
        if (chip1 == null || chip2 == null)
            return false;

        return chip1.CanMergeWith(chip2);
    }

    public bool TryMerge(Cell sourceCell, Cell targetCell, Action<Cell, Chip> onMerged = null)
    {
        if (sourceCell == null || targetCell == null)
            return false;

        if (sourceCell.IsEmpty || targetCell.IsEmpty)
            return false;
        
        if (sourceCell == targetCell)
            return false;

        var sourceChip = sourceCell.CurrentChip;
        var targetChip = targetCell.CurrentChip;

        if (!CanMerge(sourceChip, targetChip))
            return false;

        var mergedChip = sourceChip.CreateMergedChip();

        sourceCell.Clear();
        targetCell.Clear();

        targetCell.TryOccupy(mergedChip);

        onMerged?.Invoke(targetCell, mergedChip);
        return true;
    }
}