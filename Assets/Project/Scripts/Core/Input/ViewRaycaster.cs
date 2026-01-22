using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ViewRaycaster : IViewRaycaster
{
    private readonly GraphicRaycaster graphicRaycaster;
    private readonly EventSystem eventSystem;
    public ViewRaycaster(Canvas canvas)
    {
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        eventSystem = EventSystem.current;
    }
    public ICellView GetCellAtScreenPosition(Vector2 screenPosition)
    {

        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = screenPosition;

        var results = new List<RaycastResult>();

        graphicRaycaster.Raycast(pointerEventData, results);
        foreach (var result in results)
        {
            var cellView = result.gameObject.GetComponent<ICellView>();
            if (cellView != null)
            {
                return cellView;
            }
        }

        return null;
    }
}