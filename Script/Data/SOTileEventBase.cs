using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class SOTileEventBase: ScriptableObject, ITileEvent
{
    public abstract void TileEvent(ObjectBase objectBase);
}
