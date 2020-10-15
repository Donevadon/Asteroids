using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLibrary
{
    public class GameSystem : MonoBehaviour
    {
        public static GameSystem gameSystem;
        public EntityFactory Factory { get; private set; }
        public int Score { get; private set; }
        private GameSystem()
        {

        }
        private void Awake()
        {
            gameSystem = this;
            Factory = GetComponent<EntityFactory>();
        }

        public void SpawnEntity(Entity entity, Vector3 position,Quaternion quaternion, Action DeadHandler)
        {
            GameEntity gameEntity = Factory.GetEntity(entity, position, quaternion);
            gameEntity.Entity_Deaded += DeadHandler;
        }

        public static GameSystem GetInstance()
        {
            if (gameSystem is null) throw new Exception("GameManager not created");
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

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
