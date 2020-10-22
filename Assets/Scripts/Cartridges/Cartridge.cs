using GameLibrary;
using GameLibrary.EntityLibrary;
using System;
using UnityEngine;

public abstract class Cartridge : MonoBehaviour,ICartridge
{
    public abstract Sprite Image { get; }
    public abstract Entity Type { get; }
    public abstract System.Numerics.Vector3 Position { get; set; }
    public abstract System.Numerics.Vector3 Rotation { get; set; }

    public abstract event Action<IEntity> Entity_Deaded;

    public abstract void Destroy();

    public abstract void UpdateData();
}
