using GameLibrary;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class GameManager : MonoBehaviour
{
    [SerializeField]private float frequency;
    private GameSystem system;
    public GameObject defeatPanel;
    public Text score;

    private void Awake()
    {
        system = GameSystem.GetInstance();
    }

    private void Start()
    {
        system.Spawner.SpawnEntity(
            Entity.Player,
            Vector3.zero.Parse(),
            Visualization.GetRandomEuler().Parse(),
            (x) => Defeat());
        StartCoroutine(SpawnEnemy());
    }

    private void OnApplicationQuit()
    {
        system.InvokeCloseProgram();
    }

    private void Defeat()
    {
        defeatPanel.SetActive(true);
    }

    private void AddScore()
    {
        system.AddScore(1);
        score.text = "Score : " + system.Score.ToString();
    }

    public void ReloadEntity()
    {
        system.ReloadEntity((entity) =>
        {
            IEntity gameEntity = system.Spawner.SpawnEntity(
                entity.Type,
                entity.Position,
                Visualization.GetEuler(entity.Rotation.Parse()).Parse(),
                null);
            if (gameEntity.Type == Entity.Player) gameEntity.Entity_Deaded += (x) => Defeat();
            else gameEntity.Entity_Deaded += (x) => AddScore();
        });
    }

    private IEnumerator SpawnEnemy()
    {
        int count = 0;
        while (true)
        {
            system.Spawner.SpawnEntity(
                Entity.Asteroid,
                new Vector3(9,5,0).Parse(),
                Visualization.GetRandomEuler().Parse(),
                (x)=> AddScore());
            if (++count > 10)
            {
                count = 0;
                system.Spawner.SpawnEntity(
                    Entity.Alien, 
                    new Vector3(8.8f, 4.8f, 0).Parse(), 
                    Visualization.GetRandomEuler().Parse(), 
                    (x) => AddScore());
            }
            yield return new WaitForSeconds(frequency);
        }
    }
    /// <summary>
    /// Перезапуск уровня
    /// </summary>
    public void Restart()
    {
        system.Restart(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }
}
