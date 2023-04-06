using Base;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create Consumable", fileName = "Consumable", order = 0)]
    public class Consumable : ItemData, IWeight
    {
        [field: SerializeField] public float weight { get; private set; }
    }
}

