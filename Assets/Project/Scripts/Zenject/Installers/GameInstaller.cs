using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Prefabs")]
    [SerializeField] private ChipView chipViewPrefab;
    [SerializeField] private CellView cellViewPrefab;

    [Header("Config")]
    [SerializeField] private ChipTypeDatabase chipTypeDatabase;
    public override void InstallBindings()
    {
        Container.BindFactory<ChipView, ChipView.Factory>()
            .FromComponentInNewPrefab(chipViewPrefab)
            .AsSingle();

        Container.Bind<IChipViewFactory>()
            .To<ChipViewFactory>()
            .AsSingle();

        Container.BindFactory<CellView, CellView.Factory>()
        .FromComponentInNewPrefab(cellViewPrefab)
        .AsSingle();

        Container.Bind<ICellViewFactory>()
            .To<CellViewFactory>()
            .AsSingle();

        Container.Bind<ChipTypeDatabase>()
            .FromInstance(chipTypeDatabase)
            .AsSingle();
    }
}