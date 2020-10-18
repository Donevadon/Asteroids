using GameLibrary;
using System;
using UnityEngine;

public class Asteroid : GameEntity
{
    [SerializeField] private float _speed;
    protected ObjectMovement movement = new ObjectMovement();

    public override Entity Type => Entity.Asteroid;

    public override event Action Entity_Deaded;

    private void Start()
    {
        movement.Acceleration = 1;
    }

    private void FixedUpdate()
    {
        transform.position = movement.Move(
            transform.position.Parse(),
            transform.TransformDirection(Visualization.GetDirection() * _speed * Time.deltaTime).Parse())
            .Parse();
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

    private void CreateDebris()
    {
        IGameEntity debris;
        for (int i = 0; i < UnityEngine.Random.Range(2, 5); i++)
        {
            debris = GameSystem.GetInstance().Factory.GetEntity(Entity.Debris, transform.position.Parse(), Quaternion.Euler(Visualization.GetRandomEuler()).Parse());
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

