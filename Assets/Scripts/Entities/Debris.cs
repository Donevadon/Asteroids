using GameLibrary;
using System;

public class Debris : Asteroid
{
    public override event Action<IEntity> Entity_Deaded;
    public override float RadiusCollider => 0.117f;
    public override Entity Type => Entity.Debris;
    private void Start()
    {
        Movement.Acceleration = 1;
    }

    public override void Dead()
    {
        Entity_Deaded(this);
        Destroy();
    }
}

