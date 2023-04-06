using System;
using Base;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class ItemStack
{
    [field: SerializeField] public ItemData data { get; private set; }
    
    [field: SerializeField] public int count { get; private set; }

    private int _maxCount;

    public ItemStack( [NotNull] ItemData data, int count = 1)
    {
        this.data = data;
        this.count = count;
        _maxCount = data.maxStackCount;
    }

    public void Init(ItemStackView view)
    {
        
    }
    
    public bool CanAddItem()
    {
        if ( count < data.maxStackCount)
            return true;

        return false;
    }

    public void AddItem(int count)
    {
        this.count = Math.Clamp(this.count + count, 0, _maxCount);
    }


    public void RemoveItem(int count, out bool toRemoveStack)
    {
        toRemoveStack = false;
        this.count =  Math.Clamp( this.count - count, 0, _maxCount);
        if (this.count == 0) 
            toRemoveStack = true;
    }

    public bool HasItem([NotNull] ItemData item) => data == item;
}
