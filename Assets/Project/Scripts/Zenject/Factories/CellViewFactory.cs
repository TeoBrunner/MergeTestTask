using UnityEngine;
using Zenject;
public class CellViewFactory : ICellViewFactory
{
    private readonly CellView.Factory factory;

    public CellViewFactory(CellView.Factory factory)
    {
        this.factory = factory;
    }

    public ICellView Create(Cell cell, Transform parent)
    {
        var cellView = factory.Create();
        cellView.transform.SetParent(parent, false);
        cellView.Initialize(cell);
        return cellView;
    }
}