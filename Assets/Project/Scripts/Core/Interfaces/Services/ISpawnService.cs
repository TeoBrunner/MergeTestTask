using System;

public interface ISpawnService
{
    bool TrySpawn(Board board, Action<Cell, Chip> onSpawned = null);
}