using GameLibrary;
using System;
using UnityEngine;

public class Alien : GameEntity
{
    [SerializeField] private float _speedMove;
    private Ship player;
    private ShipControl shipControl = new ShipControl();

    public override Entity Type => Entity.Alien;

    public override event Action Entity_Deaded;

    private void Awake()
    {
        player = FindObjectOfType<Ship>();
    }

    private void Start()
    {
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
        transform.position = shipControl.Move(
            transform.position.Parse(),
            transform.TransformDirection(Visualization.GetDirection() * _speedMove * Time.deltaTime).Parse())
            .Parse();
    }
    private void Rotate()
    {
        transform.rotation = Quaternion.Euler(
            shipControl.RotateForTarget(
                transform.position.Parse(),
                player.transform.position.Parse(),
                -Visualization.GetRotateVector().Parse(),
                Visualization.is3D).Parse());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<IGameEntity>()?.Dead();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IGameEntity>()?.Dead();
        Destroy(gameObject);
    }

    public override void Dead()
    {
        Entity_Deaded();
        Destroy(gameObject);
    }
}
