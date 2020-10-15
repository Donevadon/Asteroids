using UnityEngine;

namespace GameLibrary
{
    /// <summary>
    /// Проиводит игровые сущности
    /// </summary>
    public abstract class EntityFactory : MonoBehaviour
    {
        /// <summary>
        /// Инстантировать сущность и вернуть
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="position"></param>
        /// <param name="quaternion"></param>
        /// <returns></returns>
        public abstract GameEntity GetEntity(Entity entity, Vector2 position, Quaternion quaternion);
    }
}
