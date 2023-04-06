using System;
using Base;
using UnityEngine;

namespace UI.Model
{
   [Serializable]
    public class InventorySlot
    {
        public event Action<InventorySlot> OnItemStackCreated;
        public event Action<ItemStack> OnItemStackDestroyed;
        public event Action<ItemStack> OnItemStackChanged;
        
      
        public ItemStack stack;
        
        
        public bool IsEmpty() => stack == null || stack.data == null;

        public bool CanAddItem(ItemData item)
        {
            if (IsEmpty()) return true;
            if(HasItem(item))return stack.CanAddItem();
            return false;
        }
        
        public bool HasItem(ItemData data)
        {
            if (IsEmpty()) return false;
            return stack.HasItem(data);
        }

        public void AddItem(ItemData item, int count = 1)
        {
            if (IsEmpty())
            {
                stack = new ItemStack(item, count);
                OnItemStackCreated?.Invoke(this);
            }
            else
            {
                stack.AddItem(count);
                OnItemStackChanged?.Invoke(stack);
            }
        }

        public void RemoveItem(int count = 1)
        {
            if(IsEmpty()) return;
            stack.RemoveItem(count, out bool toRemoveStack);
            OnItemStackChanged?.Invoke(stack);
            if (toRemoveStack) Clear();
        }

        public void Clear()
        {
            OnItemStackDestroyed?.Invoke(stack);
            stack = null;
        }

    }
}