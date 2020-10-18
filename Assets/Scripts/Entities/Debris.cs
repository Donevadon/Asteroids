using GameLibrary;
using System;

public class Debris : Asteroid
{
    public override event Action Entity_Deaded;
    public override Entity Type => Entity.Debris;
    private void Start()
    {
        movement.Acceleration = 1;
    }

    public override void Dead()
    {
        Entity_Deaded();
        Destroy(gameObject);
    }
}

