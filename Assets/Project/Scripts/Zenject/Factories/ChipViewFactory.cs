using UnityEngine;
using Zenject;

/// <summary>
/// Фабрика для создания ChipView через Zenject
/// </summary>
public class ChipViewFactory : IChipViewFactory
{
    private readonly ChipView.Factory factory;

    public ChipViewFactory(ChipView.Factory factory)
    {
        this.factory = factory;
    }

    public IChipView Create(Chip chip, Transform parent)
    {
        var chipView = factory.Create();
        chipView.transform.SetParent(parent, false);
        chipView.Initialize(chip);
        return chipView;
    }
}