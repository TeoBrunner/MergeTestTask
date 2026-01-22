using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Scene")]
    [SerializeField] private BoardView boardView;
    [SerializeField] private Canvas mainCanvas;

    [Header("Prefabs")]
    [SerializeField] private ChipView chipViewPrefab;
    [SerializeField] private CellView cellViewPrefab;
    [SerializeField] private FloatingMessageView messagePrefab;

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

        Container.Bind<IBoardView>()
        .To<BoardView>()
        .FromComponentInHierarchy()
        .AsSingle();

        Container.Bind<ChipTypeDatabase>()
            .FromInstance(chipTypeDatabase)
            .AsSingle();

        Container.Bind<ISpawnService>()
            .To<SpawnService>()
            .AsSingle();

        Container.BindInterfacesAndSelfTo<GameController>()
            .AsSingle();

        Container.BindFactory<FloatingMessageView, FloatingMessageView.Factory>()
        .FromComponentInNewPrefab(messagePrefab);

        Container.Bind<IMessageService>()
        .To<FloatingMessageService>()
        .AsSingle()
        .WithArguments(mainCanvas);
    }
}