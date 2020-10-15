using System;

public class Debris : Asteroid
{
    public override event Action Entity_Deaded;
    private void Start()
    {
        type = GameLibrary.Entity.Debris;
        movement.Acceleration = 1;
    }

    public override void Dead()
    {
        Entity_Deaded();
        Destroy(gameObject);
    }
}

