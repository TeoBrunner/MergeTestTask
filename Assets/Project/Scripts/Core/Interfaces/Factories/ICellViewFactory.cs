using UnityEngine;
public interface ICellViewFactory
{
    ICellView Create(Cell cell, Transform parent);
}
