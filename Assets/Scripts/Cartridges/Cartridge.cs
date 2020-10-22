using GameLibrary;
using GameLibrary.EntityLibrary;
using System;
using UnityEngine;

public abstract class Cartridge : MonoBehaviour,ICartridge
{
    protected float deadTime;
    public abstract Sprite Image { get; }
    public abstract Entity Type { get; }
    public abstract System.Numerics.Vector3 Position { get; set; }
    public abstract System.Numerics.Vector3 Rotation { get; set; }

    public virtual float RadiusCollider { get; }

    public abstract event Action<IEntity> Entity_Deaded;
    protected abstract void Timer(float time);

    public abstract void Destroy();

    public virtual void OnCollision(IEntity foundEntity)
    {
        
    }

    public abstract void UpdateData();
}
