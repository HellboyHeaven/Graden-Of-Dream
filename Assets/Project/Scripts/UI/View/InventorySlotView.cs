using TMPro;
using UI.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.View
{
    [RequireComponent(typeof(Button))]
    public class InventorySlotView : MonoBehaviour, IDropHandler
    {
        public InventorySlot Model { get; private set; }

        private InventoryView _inventoryView;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnPointerClick);
        }


        public void Init(InventorySlot model, InventoryView inventoryView)
        {
            Model = model;
            _inventoryView = inventoryView;
            
        }
        

        public void OnPointerClick()
        {
            _inventoryView.OnSlotClicked(Model);
        }

        public void OnDrop(PointerEventData eventData)
        {
            var draggableItem = eventData.pointerDrag.GetComponent<DraggableItem<InventorySlotView>>();
            if (draggableItem is ItemStackView && draggableItem != null)
            {
                var stackView = (draggableItem as ItemStackView);
                draggableItem.ParentAfterDrag.Model.stack = Model.stack;
                Model.stack = stackView.itemStack;
                
                GetComponentInChildren<ItemStackView>().transform.SetParent(draggableItem.ParentAfterDrag.transform);
                draggableItem.ParentAfterDrag = this;
            }
          
            
        }
    }
}