using Base;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create Weapon", fileName = "Weapon", order = 0)]
    public class Weapon : ItemData, IWeight, IDamage
    {
        [field: SerializeField] public float weight { get; private set; }
        [field: SerializeField] public float damage { get; private set; }
    }
}