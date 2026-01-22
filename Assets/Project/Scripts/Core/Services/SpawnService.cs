using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnService : ISpawnService
{
    private readonly ChipTypeDatabase chipDatabase;

    public SpawnService(ChipTypeDatabase chipDatabase)
    {
        this.chipDatabase = chipDatabase;
    }

    public bool TrySpawn(Board board, Action<Cell, Chip> onSpawned = null)
    {
        var emptyCells = board.GetEmptyCells();

        if (emptyCells.Count == 0)
        {
            Debug.Log("There are no empty cells");
            return false;
        }

        Cell targetCell = emptyCells[UnityEngine.Random.Range(0, emptyCells.Count)];
        ChipType randomType = chipDatabase.GetRandom();

        Chip newChip = new Chip(randomType, 1);

        if (targetCell.TryOccupy(newChip))
        {
            onSpawned?.Invoke(targetCell, newChip);
            return true;
        }

        return false;
    }
}