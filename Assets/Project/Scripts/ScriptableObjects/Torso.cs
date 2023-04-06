using Base;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create Torso", fileName = "Torso", order = 0)]
    public class Torso : ItemData, IWeight, IDefense
    {
        [field: SerializeField] public float weight { get; private set; }
        [field: SerializeField] public float defense { get; private set; }
    }
}