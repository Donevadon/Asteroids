using GameLibrary;
using GameLibrary.ShotSystem;
using UnityEngine;

public class Bullet : Cartridge
{
    [SerializeField] private float _speed;
    private ObjectMovement movement = new ObjectMovement();

    public override Sprite Image { get; }

    private void Start()
    {
        Destroy(gameObject,1);
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
        collision.GetComponent<IGameEntity>().Dead();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IGameEntity>()?.Dead();
        Destroy(gameObject);
    }
}
