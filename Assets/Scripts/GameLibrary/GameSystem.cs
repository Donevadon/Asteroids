using System;
using System.Numerics;

namespace GameLibrary
{
    public class GameSystem
    {
        private static GameSystem gameSystem;
        public IEntityFactory Factory { get; set; }
        public int Score { get; private set; }
        private GameSystem()
        {

        }
        public void SpawnEntity(Entity entity, Vector3 position,Quaternion quaternion, Action DeadHandler)
        {
            IGameEntity gameEntity = gameSystem.Factory.GetEntity(entity, position, quaternion);
            gameEntity.Entity_Deaded += DeadHandler;
        }

        public static GameSystem GetInstance()
        {
            if (gameSystem is null) gameSystem = new GameSystem();
            return gameSystem;
        }

        public void AddScore(int score)
        {
            Score += score;
        }

        public void ResetScore()
        {
            Score = 0;
        }
    }
}
