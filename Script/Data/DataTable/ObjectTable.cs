using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object Table", menuName = "SO/Object Table", order = int.MaxValue )]
public class ObjectTable : ScriptableObject
{
    [System.Serializable]
    public struct ObjectData
    {
        public GameObject Object;
        public string[] AllowTile;
        [Range(0, 100)]
        public float SpawnProbability;
    }
    public ObjectData[] m_Object;
}
