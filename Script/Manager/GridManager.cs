using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Linq;
public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField]
    Tilemap tileMap;
    [SerializeField]
    GameObject GridPool;
    [SerializeField]
    GameObject GridImage;
    [SerializeField]
    Queue<GameObject> Q_GridPool;
    [SerializeField]
    List<GameObject> ActiveGrids;
    [SerializeField]
    Color AllowGirdColor;
    [SerializeField]
    Color UnAllowGirdColor;
    [SerializeField]
    GameObject BuildingBluePrint;
    SpriteRenderer BluePrintRender;
    TileInform MouseTile;
    void Start()
    {
        Instance = this;
        Q_GridPool = new Queue<GameObject>();
        for (int i = 0; i < GridPool.transform.childCount; i++)
        {
            Q_GridPool.Enqueue(GridPool.transform.GetChild(i).gameObject);
        }
        BluePrintRender = BuildingBluePrint.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    Vector2 BuildingPos = new Vector2();
        //    Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    if (MapData.Instance.MoveTile(MousePos, ref MouseTile))
        //    {
        //        if (DrawBluePrint(Building2, MousePos, BuildingBase.Direction.Front, ref BuildingPos))
        //            Instantiate(Building2, BuildingPos, Building.transform.rotation);
        //    }
        //}
        //if (Input.GetMouseButton(0))
        //{
        //    Vector2 BuildingPos = new Vector2();
        //    Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    if (MapData.Instance.MoveTile(MousePos, ref MouseTile))
        //    {
        //        DeleteAllGrid();
        //        if (DrawBluePrint(Building, MousePos, BuildingBase.Direction.Left, ref BuildingPos))
        //        {
        //            Building.BuildingDirection = BuildingBase.Direction.Right;
        //            // 그리드 생성
        //            Instantiate(Building, BuildingPos, Building.transform.rotation);

        //        }
        //    }
        //}
    }
    public Vector2 GetBuildingPos(BuildingBase ObjectData, Vector2 Pos)
    {
        float HalfX = ObjectData.Size.x / 2;
        float HalfY = ObjectData.Size.y / 2;
        TileInform ClickTile = MapData.Instance.GetTileInform(Pos);
        Vector2 BuildingPos = new Vector2(ClickTile.transform.position.x + -(HalfX) + 0.5f, ClickTile.transform.position.y - (HalfY) + 0.5f);
        return BuildingPos;
    }
    /// <summary>얻고자하는 건물의 청사진을 그립니다. BuildingPos를 매개변수로 청사진 건물의 위치를 얻습니다.</summary>
    public bool DrawBluePrint(BuildingBase ObjectData, Vector2 Pos, BuildingBase.Direction Dirc, ref Vector2 BuildingPos)
    {
        ObjectData.BuildingDirection = Dirc;
        bool IsCounstruct;
        IsCounstruct = true;
        DeleteGrid();
        ActiveGrids = new List<GameObject>();

        // 오브젝트 풀링 안에 있는 오브젝트가 생성할 그리드보다 적을경우
        while (Q_GridPool.Count < ObjectData.Size.x * ObjectData.Size.y)
        {
            GameObject grid = Instantiate(GridImage);
            grid.transform.parent = GridPool.transform;
            Q_GridPool.Enqueue(grid);
            grid.SetActive(false);
        }
        // 건물 청사진 생성
        TileInform ClickTile = MapData.Instance.GetTileInform(Pos);
        Vector2 BluePrintPos = GetBuildingPos(ObjectData, ClickTile.transform.position);
        BuildingBluePrint.transform.position = BluePrintPos;
        BluePrintRender.sprite = ObjectData.GetComponent<SpriteRenderer>().sprite;
        BuildingBluePrint.transform.rotation = Quaternion.Euler(0, 0, (float)Dirc);
        BuildingBluePrint.SetActive(true);

        BuildingPos = BluePrintPos;
        // 그리드 생성
        TileInform[] UnderTiles = ObjectData.SearchUnderTile(BluePrintPos);
        foreach (TileInform Tile in UnderTiles)
        {
            GameObject grid = Q_GridPool.Dequeue();
            grid.SetActive(true);
            grid.transform.position = Tile.transform.position;
            ActiveGrids.Add(grid);
            if (Tile.Construted == false && Tile.isBuilding == true)
            {
                grid.gameObject.GetComponent<SpriteRenderer>().color = AllowGirdColor;
            }
            else
            {
                grid.gameObject.GetComponent<SpriteRenderer>().color = UnAllowGirdColor;
                IsCounstruct = false;
            }
        }
        return IsCounstruct;
    }
    // 그리드 그리기
    public void DrawGrid(int x, int y, Vector2 Pos)
    {
        DeleteGrid();
        Vector3Int gridPos = tileMap.WorldToCell(Pos);
        ActiveGrids = new List<GameObject>();
        // 오브젝트 풀링 안에 있는 오브젝트가 생성할 그리드보다 적을경우
        while (Q_GridPool.Count < x * y)
        {
            GameObject grid = Instantiate(GridImage);
            grid.transform.parent = GridPool.transform;
            Q_GridPool.Enqueue(grid);
            grid.SetActive(false);
        }
        for (int X = 0; X < x; X++)
        {
            for (int Y = 0; Y < y; Y++)
            {
                GameObject grid = Q_GridPool.Dequeue();
                grid.SetActive(true);
                grid.transform.position = new Vector2(gridPos.x + X + 0.5f, gridPos.y + Y + 0.5f);
                ActiveGrids.Add(grid);
            }
        }
    }
    public void DeleteGrid()
    {
        foreach (GameObject grid in ActiveGrids)
        {
            Q_GridPool.Enqueue(grid);
            grid.SetActive(false);
        }
        BuildingBluePrint.SetActive(false);
    }
    public void DeleteAllGrid()
    {
        for (int i = 0; i < Q_GridPool.Count; i++)
        {
            GameObject Grid = Q_GridPool.Dequeue();
            Grid.SetActive(false);
            Q_GridPool.Enqueue(Grid);
        }
    }

}
