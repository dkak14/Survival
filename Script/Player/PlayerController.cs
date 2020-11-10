using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class PlayerController : CreatureBase
{
    bool isObjectCollision;
    public static bool isMoving;
    [SerializeField] JoystickValue Value;

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        isMoving = true;
    }

    protected override void Move()
    {
        Vector2 dir = Value.JoyTouch;
        MoveDirc = dir;
        float TileSpeed = OnTile.TileSpeed;
        Vector2 Movedir = (dir * TileSpeed * Speed * Time.deltaTime);
        //transform.position = new Vector2(transform.position.x + dir.x, transform.position.y + dir.y);
        Rigid.MovePosition(Rigid.position + Movedir);
        Render.sortingOrder = -(int)Rigid.position.y;
    }

    void PlayerSightDirSet(SightDir dir)
    {
        switch (dir)
        {
            case SightDir.Up:
                SightDirc = SightDir.Up;
                break;
            case SightDir.Down:
                SightDirc = SightDir.Down;
                break;
            case SightDir.Left:
                SightDirc = SightDir.Left;
                break;
            case SightDir.Right:
                SightDirc = SightDir.Right;
                break;
            default:
                break;
        }
        
    }
}
