using GameLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEntity : MonoBehaviour, IGameEntity
{
    public abstract Entity Type { get; }

    public abstract event Action Entity_Deaded;

    public abstract void Dead();
}
