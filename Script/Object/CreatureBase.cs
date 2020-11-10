using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureBase : ObjectBase
{
    [SerializeField] int hp;
    public int Hp { get { return hp; } set { hp = value; } }
    [SerializeField]  float speed;
    public float Speed { get { return speed; } set { speed = value; } }
    [SerializeField]  float actionSpeed;
    public float ActionSpeed { get { return actionSpeed; } set { actionSpeed = value; } }
    [SerializeField]  int damage;
    public int Damage { get { return damage; } set { damage = value; } }
    [SerializeField]  int armor;
    public int Armor { get { return armor; } set { armor = value; } }
    [SerializeField] Vector2 sightVector;
    public Vector2 SightVector { get { return sightVector; } }
    [SerializeField] SightDir sightDirc;
    public SightDir SightDirc
    {
        get { return sightDirc; }
        set {
            sightDirc = value;
            if (sightDirc == SightDir.Up)
                sightVector = new Vector2(0, 1);
            else if (sightDirc == SightDir.Down)
                sightVector = new Vector2(0, -1);
            else if (sightDirc == SightDir.Left)
                sightVector = new Vector2(-1, 0);
            else if (sightDirc == SightDir.Right)
                sightVector = new Vector2(1, 0);
        }
    }
    public enum SightDir
    {
        Up, Down, Left, Right
    }
    protected Animator Anim;
    public TileInform OnTile;
    protected int ver, hor;
    protected Vector2 MoveDirc;
    protected override void Awake()
    {
        base.Awake();
        Anim = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
    }
    protected virtual void FixedUpdate()
    {
        MoveOtherTile();
        DirectionSet(MoveDirc);
    }
    protected virtual void DirectionSet(Vector2 Dirc)
    {
        Move();
        Vector2 RigidDIrc = Dirc.normalized;
        float Angle = Mathf.Atan2(RigidDIrc.y, RigidDIrc.x) * Mathf.Rad2Deg;
        if (45 <= Angle && Angle < 135)
        {
            SightDirc = SightDir.Up;
            ver = 1;
            hor = 0;
        }
        else if (135 <= Angle || Angle < -135)
        {
            SightDirc = SightDir.Left;
            ver = 0;
            hor = -1;
        }
        else if (-135 <= Angle && Angle < -45)
        {
            SightDirc = SightDir.Down;
            ver = -1;
            hor = 0;
        }
        else if (-45 <= Angle && Angle < 45)
        {
            SightDirc = SightDir.Right;
            ver = 0;
            hor = 1;
        }
        if (Angle == 0)
        {
            ver = 0;
            hor = 0;
        }
        else
            transform.rotation = Quaternion.Euler(0, 0, Angle);
    }
    protected void MoveOtherTile()
    {
        TileInform nowTile = OnTile;
        MapData.Instance.MoveTile(transform.position, ref OnTile);
        if (OnTile != null)
        {
            if (OnTile != nowTile)
            {
                if (OnTile.SO_TileEvent != null)
                    OnTile.SO_TileEvent.TileEvent(gameObject.GetComponent<ObjectBase>());
            }
        }
    }
    protected abstract void Move();
}
