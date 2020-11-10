using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Plant Table", menuName = "SO/Plant Table", order = int.MaxValue)]
public class PlantTable : ScriptableObject
{
    [System.Serializable]
    public struct PlantData
    {
        public string PlantName;
        public Sprite[] PlantSprite;
        public float CultivationPeriod;
        public float GlowSpeed;
        public int MaxHarvestNum;
        public int MinHarvestNum;
        public GameObject HarvestCrops;
        public bool ProlificPlant; // 다작식물
        public bool isHarvest;
    }
    public PlantData[] m_Plant;
}
