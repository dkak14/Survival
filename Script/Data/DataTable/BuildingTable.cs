using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building Table", menuName = "SO/Building Table", order = int.MaxValue)]
public class BuildingTable : ScriptableObject
{
    [System.Serializable]
    public struct Meterial
    {
        public int MeterialItemID;
        public int ItemNum;
    }
    [System.Serializable]
    public struct BulidingData
    {
        public string BuildingName;
        public BuildingBase BuildingObject;
        public Meterial[] BuildingMeterial;
        public bool isConstruct;
    }
    public BulidingData[] m_Building;
}
