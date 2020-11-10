using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Craft Table", menuName = "SO/Craft Table", order = int.MaxValue)]
public class CraftTable : ScriptableObject
{
    [System.Serializable]
    public struct Material
    {
        public int MaterialItemID;
        public int NeedNum;
    }
    [System.Serializable]
    public struct CraftData
    {
        public int CraftItemID;
        public Material[] Materials;
    }
    public CraftData[] m_Craft;
}
