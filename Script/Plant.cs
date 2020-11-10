using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
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

    SpriteRenderer Renderer;
    void Start()
    {
        if(PlantName != null)
        {
            SetPlant(PlantName);
        }
    }

    
    void Update()
    {
        
    }
    public void SetPlant(string _PlantName)
    {
        var PlantData = GameManager.Instance.Get.GetPlantData(_PlantName);
        PlantName = PlantData.PlantName;
        gameObject.name = PlantName;
        PlantSprite = PlantData.PlantSprite;
        CultivationPeriod = PlantData.CultivationPeriod;
        MaxHarvestNum = PlantData.MaxHarvestNum;
        MinHarvestNum = PlantData.MinHarvestNum;
        HarvestCrops = PlantData.HarvestCrops;
        ProlificPlant = PlantData.ProlificPlant;
        Renderer = GetComponent<SpriteRenderer>();
        Renderer.sprite = PlantSprite[0];
    }
}
