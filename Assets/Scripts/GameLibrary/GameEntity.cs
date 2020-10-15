using System;
using UnityEngine;

namespace GameLibrary
{
    public abstract class GameEntity : MonoBehaviour
    {
        /// <summary>
        /// Событие смерти сущности
        /// </summary>
        public abstract event Action Entity_Deaded;
        /// <summary>
        /// Смерть сущности
        /// </summary>
        public abstract void Dead();
        /// <summary>
        /// Определить тип сущности
        /// </summary>
        public Entity type;
    }
}
