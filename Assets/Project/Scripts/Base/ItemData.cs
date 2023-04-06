using System;
using NaughtyAttributes;
using UnityEngine;

namespace Base
{
    [Serializable]
    public abstract class ItemData : ScriptableObject
    {
        [field: SerializeField] public int maxStackCount {get; private set; }
        [field: SerializeField, ShowAssetPreview] public Sprite icon { get; private set; }
    }
}