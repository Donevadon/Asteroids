using GameLibrary;
using System;
using UnityEngine;

[RequireComponent(typeof(ShipControl))]
public class Alien : GameEntity
{
    private Ship player;
    private ShipControl shipControl;
    public override event Action Entity_Deaded;

    private void Awake()
    {
        shipControl = GetComponent<ShipControl>();
        player = FindObjectOfType<Ship>();
    }

    private void Start()
    {
        type = Entity.Alien;
        shipControl.MoveAcceleration = 1;
    }

    private void FixedUpdate()
    {
        Move();
        try
        {
            Rotate();
        }
        catch
        {

        }
    }

    private void Move()
    {
        shipControl.Move(Visualization.GetDirection());
    }
    private void Rotate()
    {
        shipControl.RotateForTarget(player.transform.position,-Visualization.GetRotateVector(),Visualization.is3D);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<GameEntity>()?.Dead();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<GameEntity>()?.Dead();
        Destroy(gameObject);
    }

    public override void Dead()
    {
        Entity_Deaded();
        Destroy(gameObject);
    }
}
