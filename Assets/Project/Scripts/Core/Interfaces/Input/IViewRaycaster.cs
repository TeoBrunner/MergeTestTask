using UnityEngine;
public interface IViewRaycaster
{
    ICellView GetCellAtScreenPosition(Vector2 screenPosition);
}