using System;
using System.Numerics;

namespace GameLibrary
{
    internal class CreatorEntity : ISpawnEntity
    {
        private IEntityFactory Factory { get;}
        private IDataUpdater Updater { get; }

        internal CreatorEntity(IDataUpdater updater)
        {
            Updater = updater;
            Factory = new Factory();
        }

        public IEntity SpawnEntity(Entity entity, Vector3 position, Vector3 euler, Action<IEntity> DeadHandler)
        {
            IEntity gameEntity = Factory.GetEntity(entity);
            gameEntity.Position = position;
            gameEntity.Rotation = euler;
            gameEntity.Entity_Deaded += Updater.Remove;
            gameEntity.Entity_Deaded += DeadHandler;
            Updater.Add(gameEntity);
            return gameEntity;
        }
    }
}
