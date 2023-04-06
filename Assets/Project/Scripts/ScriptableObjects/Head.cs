using Base;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create Head", fileName = "Head", order = 0)]
    public class Head : ItemData, IWeight, IDefense
    {
        [field: SerializeField] public float weight { get; private set; }
        [field: SerializeField] public float defense { get; private set; }
    }
}