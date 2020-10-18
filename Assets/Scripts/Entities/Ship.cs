using GameLibrary;
using GameLibrary.ShotSystem;
using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

/// <summary>
/// Корабль игрока
/// </summary>
public class Ship : GameEntity
{
    [SerializeField] private float _speedMove;
    [SerializeField] private float _speedRotate;
    private ShipControl shipControl = new ShipControl();

    public override Entity Type { get;} = Entity.Player;

    public override event Action Entity_Deaded;

    private void Awake()
    {
        InitialGuns();
    }

    private void InitialGuns()
    {
        shipControl.GunManager.ShipGuns = new Weapon[]
            {
                new WeaponComponent(
                    new CartridgeLoader(),
                    Weapons.BulletWearon,
                    0.3f),
                new WeaponComponent(
                    new CartridgeLoader(),
                    Weapons.LazerWeapon,
                    2,
                    0.2f,
                    2)
            };
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
        Shoot();
        shipControl.GunsRechargeAndAddCartridge(Time.deltaTime);
    }

    private void Move()
    {
        shipControl.MoveAcceleration = Input.GetAxis("Vertical");
        transform.position = shipControl.Move(
            transform.position.Parse(),
            transform.TransformDirection(Visualization.GetDirection() *_speedMove* Time.deltaTime).Parse())
            .Parse();
    }
    private void Rotate()
    {
        shipControl.RotateAcceleration = Input.GetAxis("Horizontal");
        transform.Rotate(
            shipControl.GetRotateDirection(
                (Visualization.GetRotateVector() * _speedRotate)
                .Parse())
            .Parse());
    }
    private void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            shipControl.Shoot(
                Weapons.BulletWearon,
                transform.position.Parse(), 
                transform.rotation.eulerAngles.Parse());
        }
        if (Input.GetMouseButton(1))
        {
            shipControl.Shoot(
                Weapons.LazerWeapon,
                transform.position.Parse(), 
                transform.rotation.eulerAngles.Parse());
        }
    }
    public override void Dead()
    {
        Entity_Deaded();
        Destroy(gameObject);
    }
}
