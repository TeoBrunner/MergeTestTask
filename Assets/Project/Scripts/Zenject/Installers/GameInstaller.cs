using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Prefabs")]
    [SerializeField] private ChipView chipViewPrefab;

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

        Container.Bind<ChipTypeDatabase>()
            .FromInstance(chipTypeDatabase)
            .AsSingle();
    }
}