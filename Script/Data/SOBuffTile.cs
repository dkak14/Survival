using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SOBuffTile", menuName = "SO/EventTile/SOBuffTile", order = int.MaxValue)]
public class SOBuffTile : SOTileEventBase
{
    public string BuffName;
    public float BuffTime;
    public override void TileEvent(ObjectBase objectBase)
    {
        
    }
}
