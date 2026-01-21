public interface IGameController
{
    void Initialize();
    void OnSpawnButtonClicked();
    bool TryMoveChip(Cell fromCell, Cell toCell);
}