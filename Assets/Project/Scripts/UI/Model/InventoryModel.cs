using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Base;
using Extensions;
using NaughtyAttributes;
using ScriptableObjects;
using UI.View;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.Model
{
    [Serializable]
    public class InventoryModel : ISaveData
    {
        public event Action<InventorySlot> OnStackCreated;
        public event Action<ItemStack> OnStackDestroyed; 
        public event Action<ItemStack> OnStackChanged;

        [SerializeField, Expandable] private ItemRegistry itemRegistry;
    
    
        [SerializeField] private int maxSlotCount = 30;
        [field: SerializeField] public int unlockedSlotCount { get; private set; } = 15;
        [field: SerializeField] public int unlockPrice { get; private set; } = 30;

        private int _slotCount;
    

        [SerializeField]
        private List<InventorySlot> _itemSlots;
        public IReadOnlyList<InventorySlot> ItemSlots => _itemSlots;
    
        public InventoryModel()
        {
            _slotCount = unlockedSlotCount;
            _itemSlots = new List<InventorySlot>();
            for (int i = 0; i < _slotCount; i++)
                _itemSlots.Add(new InventorySlot());
        }

        public void Init()
        {
            foreach (var slot in ItemSlots)
            {
                slot.OnItemStackDestroyed += (stack) => OnStackDestroyed?.Invoke(stack);
                slot.OnItemStackCreated += (inventorySlot) => OnStackCreated?.Invoke(inventorySlot);
                slot.OnItemStackChanged += (stack) => OnStackChanged?.Invoke(stack);
            }
        }
    
        public InventorySlot UnlockSlot(int coin)
        {
            if (unlockedSlotCount < maxSlotCount && coin >= unlockPrice)
            {
                unlockedSlotCount++;
                _slotCount++;
                InventorySlot slot = CreateSlot();
                _itemSlots.Add(slot);
                return slot;
            }
            return null;
        }

        public void RemoveAmmo()
        {
            Consumable consumable = GetRandomItem<Consumable>();
            var slot = GetSlotWithItem(consumable);
            if (slot is null) return;
            slot.RemoveItem();
        }

        public void AddAmmoStacks()
        {
            List<Consumable> consumables = GetItems<Consumable>();

            foreach (var consumable in consumables)
            {
                InventorySlot slot = GetEmptySlot();
                if(slot is null) return;
                slot.AddItem(consumable, consumable.maxStackCount);
            }
        }

        public void AddItems()
        {
            ItemData[] itemsToAdd = new ItemData[]
            {
                GetRandomItem<Weapon>(),
                GetRandomItem<Head>(),
                GetRandomItem<Torso>()
            };


            foreach (var item in itemsToAdd)
            {
                InventorySlot slot = GetSlotForItem(item);
                if (slot != null)
                {
                    slot.AddItem(item);
                }
            }
            
        }

        public void RemoveRandomItemStack()
        {
             InventorySlot slot = ItemSlots.Where(s => !s.IsEmpty()).ToList().GetRandom();
            if (slot == null)
            {
                Debug.LogError("No more item");
                return;
            }
            slot.Clear();
        }




        private List<T> GetItems<T>() where T : ItemData
            => itemRegistry.Items.Select(i => i as T).Where(i => i is not null).ToList();

        private T GetRandomItem<T>() where T : ItemData =>
            GetItems<T>().GetRandom();
        
        private InventorySlot GetEmptySlot()
        {
            foreach (var slot in ItemSlots)
            {
                if (slot.IsEmpty()) return slot;
            }
            return null;
        }
        

        private InventorySlot GetSlotForItem(ItemData item)
        {
            if (item == null) return null;
            foreach (var slot in ItemSlots)
            {
                if (slot.CanAddItem(item)) return slot;
            }
            return null;
        }

        private InventorySlot GetSlotWithItem(ItemData item)
        {
            if (item == null) return null;
            foreach (var slot in ItemSlots)
            {
                if (slot.HasItem(item)) return slot;
            }
            return null;
        }

        private InventorySlot CreateSlot()
        {
            var slot = new InventorySlot();
            slot.OnItemStackDestroyed += (stack) => OnStackDestroyed?.Invoke(stack);
            slot.OnItemStackCreated += (inventorySlot) => OnStackCreated?.Invoke(inventorySlot);
            slot.OnItemStackChanged += (stack) => OnStackChanged?.Invoke(stack);
            return slot;
        }

        string ISaveData.path => "InventoryData.json";
        
    }
}
