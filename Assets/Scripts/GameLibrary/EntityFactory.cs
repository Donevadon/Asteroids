
using System.Numerics;

namespace GameLibrary
{
    /// <summary>
    /// Проиводит игровые сущности
    /// </summary>
    public interface IEntityFactory
    {
        /// <summary>
        /// Инстантировать сущность и вернуть
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="position"></param>
        /// <param name="quaternion"></param>
        /// <returns></returns>
        IGameEntity GetEntity(Entity entity, Vector3 position, Quaternion quaternion);
    }
}
