﻿using GameLibrary;
using GameLibrary.EntityLibrary;
using System;
using UnityEngine;

public class Alien : GameEntity
{
    [SerializeField] private float _speedMove;
    private Ship player;
    private ShipControl ShipControl { get; set; }

    public override Entity Type => Entity.Alien;

    public override System.Numerics.Vector3 Position 
    {
        get => ShipControl.Movement.Position;
        set
        {
            ShipControl.Movement.SetPosition(value);
            transform.position = ShipControl.Movement.Position.Parse();
        }
    }
    public override System.Numerics.Vector3 Rotation 
    {
        get => ShipControl.Rotation.EulerAngles;
        set
        {
            ShipControl.Rotation.SetRotation(value);
            transform.rotation = Quaternion.Euler(ShipControl.Rotation.EulerAngles.Parse());
            ShipControl.Movement.Direction = transform.TransformDirection(Visualization.GetDirection()).Parse();
        }
    }

    private event Action Ship_Move;
    private event Action<System.Numerics.Vector3, bool> Ship_Rotate;
    public override event Action<IEntity> Entity_Deaded;

    private void Awake()
    {
        InitialShip();
    }

    private void Start()
    {
        ShipControl.Movement.Acceleration = 1;
        player = FindObjectOfType<Ship>();
    }

    private void FixedUpdate()
    {
        transform.position = ShipControl.Movement.Position.Parse();
        transform.rotation = Quaternion.Euler(ShipControl.Rotation.EulerAngles.Parse());
        ShipControl.Movement.Direction = transform.TransformDirection(Visualization.GetDirection()).Parse();
    }

    private void Move()
    {
        Ship_Move();
    }

    private void InitialShip()
    {
        ShipControl = new ShipControl(
            transform.TransformDirection(Visualization.GetDirection()).Parse(),
            ref Ship_Move,
            Visualization.GetRotateVector().Parse(),
            ref Ship_Rotate);
        InitialMovement();
    }

    private void InitialMovement()
    {
        ShipControl.Movement.Speed = _speedMove;
    }

    private void Rotate()
    {
        Ship_Rotate(player.Position,Visualization.is3D);
    }

    public override void Dead()
    {
        Entity_Deaded(this);
        Destroy(gameObject);
    }

    public override void UpdateData()
    {
        Move();
        Rotate();
    }

    public override void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision?.GetComponent<IGameEntity>()?.Dead();
        Entity_Deaded(this);
        Destroy();
    }
    private void OnTriggerEnter(Collider other)
    {
        other?.GetComponent<IGameEntity>()?.Dead();
        Entity_Deaded(this);
        Destroy();
    }
}
