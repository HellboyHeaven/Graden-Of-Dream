using System;
using System.Collections.Generic;
using Base;
using Extensions;
using ScriptableObjects;
using UI.Model;
using UI.View;
using UnityEngine;

namespace UI.Controller
{
    public class InventoryController
    { 
        private InventoryModel _model;
        private InventoryView _view;
    

        public InventoryController (InventoryView view, InventoryModel model)
        {
            _model = model;
            _view = view;

            if (_model.TryLoadData(out InventoryModel inventoryModel))
                _model = inventoryModel;

            _view.InitializeInventory(_model);
            

            for (int i = 0; i < _model.ItemSlots.Count; i++)
            {
                var slot = _model.ItemSlots[i];
                if(slot.IsEmpty()) continue;
                _view.UpdateItemStackView(slot.stack);
            }
            
            _model.Init();

            _model.OnStackChanged +=  OnStackChanged;
            _model.OnStackCreated += AddItemStackView;
            _model.OnStackDestroyed += RemoveItemStackView;
        }

        private void OnStackChanged(ItemStack stack)
        {
            _view.UpdateItemStackView(stack);
        }

        public void UnlockSlot(int coin)
        {
            InventorySlot slot = _model.UnlockSlot(coin);
            if(slot is null) return;
            _view.UpdateUnlockedSlots(slot);
        }


        private void AddItemStackView(InventorySlot slot) => _view.AddItemStackView(slot);
        private void RemoveItemStackView(ItemStack stack) => _view.RemoveItemStackView(stack);

        public void RemoveAmmo() => _model.RemoveAmmo();

        public void AddAmmoStacks() => _model.AddAmmoStacks();

        public void AddItems() =>  _model.AddItems();

        public void RemoveRandomItemStack() => _model.RemoveRandomItemStack();

        public void OnDestroy()
        {
            _model.SaveData();
        }
    }
}