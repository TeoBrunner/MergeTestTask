using UnityEngine;
public interface IChipViewFactory
{
    IChipView Create(Chip chip, Transform parent);
}