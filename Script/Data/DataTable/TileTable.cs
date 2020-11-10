using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Tile Table", menuName = "SO/Tile Table", order = int.MaxValue)]
public class TileTable : ScriptableObject
{
    [System.Serializable]
    public struct TileData
    {
        public string TileKind;
        public List<TileBase> tiles;
        public float TileSpeed;
        [SerializeField]
        bool isSoil;
        public bool IsSoil { get { return isSoil; } }
        [SerializeField]
        bool isBuilding;
        public bool IsBuilding { get { return isBuilding; } }
        public SOTileEventBase SO_TileEvent;
    }
    public TileData[] m_Tile;
}
