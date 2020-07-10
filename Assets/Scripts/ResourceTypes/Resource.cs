using System;
using UnityEngine;

namespace ResourceTypes
{
    // [CreateAssetMenu(fileName = "Item", menuName = "Items", order = 0)]
    public abstract class Resource : ScriptableObject
    {
        public Sprite sprite;
        public int scale = 1;

        public abstract String GetLayerName();
    }
}
