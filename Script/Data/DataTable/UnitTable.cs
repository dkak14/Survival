using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit Table", menuName = "SO/Unit Table", order = int.MaxValue)]
public class UnitTable : ScriptableObject
{
    [System.Serializable]
    public struct UnitData
    {
        public string UnitName;
        public CreatureBase UnitObject;
        public int MaxHP;
        public float Speed;
        public float ActionSpeed;
        public int Damage;
        public int Armor;
    }
    public UnitData[] m_Unit;
}
