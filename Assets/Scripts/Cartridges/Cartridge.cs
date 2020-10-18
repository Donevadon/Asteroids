using GameLibrary.ShotSystem;
using UnityEngine;

public abstract class Cartridge : MonoBehaviour,ICartridge
{
    public abstract Sprite Image { get; }
}
