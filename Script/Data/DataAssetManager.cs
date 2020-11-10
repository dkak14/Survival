using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataAssetManager : MonoBehaviour
{
    PlantTable AssetPlantTable;
    public Dictionary<string, PlantTable.PlantData> PlantDic;
    private void Init(PlantTable table)
    {
        AssetPlantTable = table;
        PlantDic = new Dictionary<string, PlantTable.PlantData>();
        if(AssetPlantTable == null)
        {
            Debug.LogError("You missed DataTable");
            return;
        }
        foreach(var _Plant in AssetPlantTable.m_Plant)
        {
            PlantDic.Add(_Plant.PlantName, _Plant);
        }
    }
    public PlantTable.PlantData GetPlantData(string PlantName)
    {
        return PlantDic[PlantName];
    }
}
