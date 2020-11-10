using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkBench : BuildingBase, IInteraction
{
    [Header("조합 아이템")]
    public CraftTable Combinetable;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            //Interaction();
        }
    }
    public void Interaction()
    {
        UIManager.Instance.OpenCraftPage(Combinetable);
    }
}
