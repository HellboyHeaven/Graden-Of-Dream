using System;
using System.Collections.Generic;
using TMPro;
using UI.Controller;
using UI.Model;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private Button shootButton;
        [SerializeField] private Button addAmmoButton;
        [SerializeField] private Button addItemButton;
        [SerializeField] private Button removeItemButton;
        [SerializeField] private InventorySlotView slotPrefab;
        [SerializeField] private Transform slotsParent;
        [SerializeField] private ItemStackView _stackPrefab;
        [SerializeField] private TextMeshProUGUI unlockedSlotsText;

        private int _unlockedSlots;

        [SerializeField] private InventoryModel _model;
        private InventoryController _controller;

        private List<InventorySlotView> _slotViews = new List<InventorySlotView>();
        private List<ItemStackView> _itemStackViews = new List<ItemStackView>();
    

        private void Awake()
        {
            _controller = new InventoryController (this, _model);
            shootButton.onClick.AddListener(OnShootButtonClick);
            addAmmoButton.onClick.AddListener(OnAddAmmoButtonClick);
            addItemButton.onClick.AddListener(OnAddItemButtonClick);
            removeItemButton.onClick.AddListener(OnRemoveItemButtonClick);
        }

        public void InitializeInventory(InventoryModel model)
        {
            foreach (var slot in  model.ItemSlots)
            {
                InventorySlotView slotView = Instantiate(slotPrefab, slotsParent);
                slotView.Init(slot, this);
                _slotViews.Add(slotView);
                if (!slot.IsEmpty()) AddItemStackView(slot);
            }

            _unlockedSlots = model.unlockedSlotCount;
            UpdateUnlockedSlotsText();
        }

        private void OnDestroy()
        {
            _controller.OnDestroy();
        }


        public void OnSlotClicked (InventorySlot slot) {}


        public void AddItemStackView(InventorySlot slot)
        {
            var slotView = _slotViews.Find(e => e.Model == slot);
            var stackView = Instantiate(_stackPrefab, slotView.transform);
            Debug.Log(slot);
            Debug.Log(stackView);
            stackView.Init(slot.stack);
            _itemStackViews.Add(stackView);
            UpdateItemStackView(stackView.itemStack);
        }

        public void RemoveItemStackView(ItemStack stack)
        {
            var stackView = _itemStackViews.Find(e => e.itemStack == stack);
            Destroy(stackView.gameObject);
        }

        public void UpdateItemStackView(ItemStack stack)
        {
            var stackView = _itemStackViews.Find(e => e.itemStack == stack);
            stackView?.UpdateView();
        }

        public void UpdateUnlockedSlots(InventorySlot slot)
        {
            InventorySlotView slotView = Instantiate(slotPrefab, slotsParent);
            slotView.Init(slot, this);
            _slotViews.Add(slotView);
            _unlockedSlots++;
            UpdateUnlockedSlotsText();
        }
        

        private void UpdateUnlockedSlotsText()
        {
            unlockedSlotsText.text = $"Unlocked Slots: {_unlockedSlots}";
        }

        public void Purchase(int coin) => _controller.UnlockSlot(coin);

        public void OnShootButtonClick() =>  _controller.RemoveAmmo();

        public void OnAddAmmoButtonClick() =>  _controller.AddAmmoStacks();

        public void OnAddItemButtonClick() => _controller.AddItems();

        public void OnRemoveItemButtonClick() => _controller.RemoveRandomItemStack();
    }
}