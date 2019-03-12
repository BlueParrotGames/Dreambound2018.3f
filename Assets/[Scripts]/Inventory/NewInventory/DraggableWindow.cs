using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWindow : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Vector2 offset;

    public void OnPointerDown(PointerEventData eventData)
    {
        offset = eventData.position - (Vector2)transform.position;
        transform.position = eventData.position - offset;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position - offset;
    }
}
