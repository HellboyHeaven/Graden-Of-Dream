using System.Collections.Generic;
using Base;
using NaughtyAttributes;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ItemRegistry ", menuName = "ItemRegistry", order = 0)]
    public class ItemRegistry : ScriptableObject
    {
        [SerializeField, Expandable] private List<ItemData> items;

        public IReadOnlyList<ItemData> Items => items;
    }
}