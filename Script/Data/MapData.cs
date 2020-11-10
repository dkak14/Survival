using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapData : MonoBehaviour
{
    public static MapData Instance;
    public Dictionary<string, TileTable.TileData> TileDic;
    [SerializeField]
    TileTable tileTable;
    [SerializeField]
    Tilemap tileMap;
    private void Awake()
    {
        Instance = this;
        TileDic = new Dictionary<string, TileTable.TileData>();
        for (int i = 0; i < tileTable.m_Tile.Length; i++)
        {
            string forderName = tileTable.m_Tile[i].TileKind;
            TileBase[] tiles = Resources.LoadAll<TileBase>("TileMap/Tile/" + forderName);

            for (int k = 0; k < tiles.Length; k++)
            {
                TileDic.Add(tiles[k].name, tileTable.m_Tile[i]);
                tileTable.m_Tile[i].tiles.Add(tiles[k]);
            }
        }
    }

    public bool MoveTile(Vector2 Pos, ref TileInform OnTile)
    {
        RaycastHit2D ray = Physics2D.Raycast(Pos, Vector2.down, 1, 1 << LayerMask.NameToLayer("Tile"));
        if (ray)
        {
            // 처음생성 오류 방지
            if (OnTile == null)
            {
                OnTile = ray.collider.GetComponent<TileInform>();
                return true;
            }
            // 타일이 같으면 취소
            if (ray.collider.gameObject == OnTile.gameObject)
            {
                return false;
            }
            // 타일 이동시
            OnTile = ray.collider.GetComponent<TileInform>();
            return true;
        }
        else
        {
            Debug.Log("타일이 존재하지 않음" + "좌표 : " + Pos);
            return false;
        }
    }
    public TileInform GetTileInform(Vector2 Pos)
    {
        RaycastHit2D ray = Physics2D.Raycast(Pos, Vector2.down, 1, 1 << LayerMask.NameToLayer("Tile"));
        TileInform tile;
        if (ray)
        {
            tile = ray.collider.GetComponent<TileInform>();
        }
        else
        {
            Debug.Log("타일이 존재하지 않음" + "좌표 : " + Pos);
            return null;
        }
        return tile;
    }
    public Vector3 GetTilePos(Vector2 Pos)
    {
        RaycastHit2D ray = Physics2D.Raycast(Pos, Vector2.down, 1, 1 << LayerMask.NameToLayer("Tile"));
        Vector3 gridPos = ray.transform.position;
        return gridPos;
    }
}
