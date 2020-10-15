using GameLibrary;
using System;
using UnityEngine;

/// <summary>
/// Корабль игрока
/// </summary>
[RequireComponent(typeof(ShipControl))]
public class Ship : GameEntity
{
    private ShipControl shipControl;

    public override event Action Entity_Deaded;

    private void Awake()
    {
        shipControl = GetComponent<ShipControl>();
    }

    private void Start()
    {
        type = Entity.Player;
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
        Shoot();
    }

    private void Move()
    {
        shipControl.MoveAcceleration = Input.GetAxis("Vertical");
        shipControl.Move(Visualization.GetDirection());
    }
    private void Rotate()
    {
        shipControl.RotateAcceleration = Input.GetAxis("Horizontal");
        shipControl.Rotate(Visualization.GetRotateVector());
    }
    private void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            shipControl.Shoot(Weapons.BulletWearon, Visualization.GetDirection());
        }
        if (Input.GetMouseButton(1))
        {
            shipControl.Shoot(Weapons.LazerWeapon, Visualization.GetDirection());
        }
    }
    public override void Dead()
    {
        Entity_Deaded();
        Destroy(gameObject);
    }
}
