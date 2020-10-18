using System;


namespace GameLibrary
{
    public interface IGameEntity
    {
        /// <summary>
        /// Событие смерти сущности
        /// </summary>
        event Action Entity_Deaded;
        /// <summary>
        /// Смерть сущности
        /// </summary>
        void Dead();
        /// <summary>
        /// Определить тип сущности
        /// </summary>
        Entity Type { get; }
    }
}
