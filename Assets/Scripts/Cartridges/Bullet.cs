using GameLibrary;
using GameLibrary.ShotSystem;
using UnityEngine;

[RequireComponent(typeof(ObjectMovement))]
public class Bullet : Cartridge
{
    private ObjectMovement movement;

    public override Sprite Image { get; }

    private void Awake()
    {
        movement = GetComponent<ObjectMovement>();
    }

    private void Start()
    {
        Destroy(gameObject,1);
        movement.Acceleration = 1;
    }

    private void FixedUpdate()
    {
        movement.Move(Visualization.GetDirection());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<GameEntity>().Dead();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<GameEntity>()?.Dead();
        Destroy(gameObject);
    }
}
