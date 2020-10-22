using GameLibrary;
using GameLibrary.EntityLibrary;
using System;
using UnityEngine;

public class Asteroid : GameEntity
{
    [SerializeField] private float _speed;
    protected ObjectMovement Movement { get; set; }
    protected ObjectRotation RotationObject { get; set; }
    public override Entity Type => Entity.Asteroid;
    public override System.Numerics.Vector3 Position 
    {
        get => Movement.Position;
        set => Movement.SetPosition(value);
    }
    public override System.Numerics.Vector3 Rotation 
    {
        get => RotationObject.EulerAngles;
        set 
        { 
            RotationObject.SetRotation(value);
            transform.rotation = Quaternion.Euler(RotationObject.EulerAngles.Parse());
            Movement.Direction = transform.TransformDirection(Visualization.GetDirection()).Parse();
        } 
    }

    public event Action Object_Move;
    public override event Action<IEntity> Entity_Deaded;
    private void Awake()
    {
        Initial();
    }
    private void FixedUpdate()
    {
        transform.position = Movement.Position.Parse();
    }
    private void Initial()
    {
        RotationObject = new ObjectRotation();
        Movement = new ObjectMovement(ref Object_Move);
        Movement.Speed = _speed;
        Movement.Acceleration = 1;
    }
    private void CreateDebris()
    {
        IEntity debris;
        for (int i = 0; i < UnityEngine.Random.Range(2, 5); i++)
        {
            debris = GameSystem.GetInstance().Spawner.SpawnEntity(Entity.Debris, transform.position.Parse(), Visualization.GetRandomEuler().Parse(),null);
            debris.Entity_Deaded += Entity_Deaded;
        }
    }
    public override void Dead()
    {
        CreateDebris();
        Entity_Deaded(this);
        Destroy(gameObject);
    }
    public override void UpdateData()
    {
        Object_Move();
    }

    public override void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision?.GetComponent<IGameEntity>()?.Dead();
    }
    private void OnTriggerEnter(Collider other)
    {
        other?.GetComponent<IGameEntity>()?.Dead();
    }

}

