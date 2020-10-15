using GameLibrary;
using System;
using UnityEngine;

[RequireComponent(typeof(ObjectMovement))]
public class Asteroid : GameEntity
{
    protected ObjectMovement movement;
    public override event Action Entity_Deaded;

    private void Awake()
    {
        movement = GetComponent<ObjectMovement>();
    }
    private void Start()
    {
        type = Entity.Asteroid;
        movement.Acceleration = 1;
    }

    private void FixedUpdate()
    {
        movement.Move(Visualization.GetDirection());
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

    private void CreateDebris()
    {
        GameEntity debris;
        for (int i = 0; i < UnityEngine.Random.Range(2, 5); i++)
        {
            debris = GameSystem.GetInstance().Factory.GetEntity(Entity.Debris, transform.position, Quaternion.Euler(Visualization.GetEuler()));
            debris.Entity_Deaded += Entity_Deaded;
        }
    }

    public override void Dead()
    {
        CreateDebris();
        Entity_Deaded();
        Destroy(gameObject);
    }
}

