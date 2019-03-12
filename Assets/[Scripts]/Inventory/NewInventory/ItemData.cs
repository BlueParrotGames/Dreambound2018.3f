using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public int amount;
    public int slotIndex;

    private Transform originParent;
    private Inventory inventory;
    private Tooltip tooltip;
    private Vector2 offset;
    private CanvasGroup group;

    public Image iconRenderer;
    public Image rarityRenderer;

    void Start()
    {
        group = GetComponent<CanvasGroup>();
        inventory = GameObject.Find("Scene").GetComponent<Inventory>();
        tooltip = GameObject.Find("Canvas").GetComponent<Tooltip>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            originParent = transform.parent;
            transform.SetParent(transform.parent.parent);
            group.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            transform.position = eventData.position - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(inventory.slots[slotIndex].transform);
        transform.position = inventory.slots[slotIndex].transform.position;
        group.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offset = eventData.position - (Vector2)transform.position;
        transform.position = eventData.position - offset;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Active(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }
}
