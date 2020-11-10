using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject Inventory;
    public GameObject InventoryPage;
    public GameObject CraftPage;
    public UICraft uiCraft;

    private void Start()
    {
        Instance = this;
    }
    public void OpenCraftPage(CraftTable craftData)
    {
        Inventory.SetActive(true);
        InventoryPage.SetActive(false);
        CraftPage.SetActive(true);
        uiCraft.CraftItemList(craftData);
    }
}
