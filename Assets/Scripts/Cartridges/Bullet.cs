using GameLibrary;
using GameLibrary.EntityLibrary;
using System;
using UnityEngine;

public class Bullet : Cartridge
{
    [SerializeField] private float _speed;
    private ObjectMovement Movement { get; set; }
    private ObjectRotation RotationObject { get; set; }

    private event Action Object_Move;
    public override event Action<IEntity> Entity_Deaded;
    private float deadTime;

    public override Sprite Image { get; }

    public override Entity Type => Entity.Bullet;

    public override System.Numerics.Vector3 Position { get => Movement.Position; set => Movement.SetPosition(value); }
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
    private void Timer(float time)
    {
        if (deadTime >= 1)
        {
            Entity_Deaded(this);
            GameSystem.Context.Send(InvokeDestroy,null);
        }
        else deadTime += time;
    }

    private void InvokeDestroy(object o)
    {
        Destroy(gameObject);
    }

    public override void UpdateData()
    {
        Object_Move();
        Timer(0.03f);
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
