using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class DraggableItem<T> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler where T : MonoBehaviour, IDropHandler
{
    [HideInInspector] public T ParentAfterDrag;
    private Image _image;


    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ParentAfterDrag = GetComponentInParent<T>();
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(ParentAfterDrag.transform);
        _image.raycastTarget = true;
    }
}
