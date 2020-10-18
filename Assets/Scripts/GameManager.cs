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
        system.Factory = new Factory();
    }

    private void Start()
    {
        system.SpawnEntity(
            Entity.Player,
            Vector3.zero.Parse(),
            Quaternion.Euler(Visualization.GetRandomEuler()).Parse(),
            Defeat);
        StartCoroutine(SpawnEnemy());
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
        GameEntity[] entities = FindObjectsOfType<GameEntity>();
        foreach (var entity in entities)
        {
            Destroy(entity.gameObject);
            IGameEntity gameEntity = system.Factory.GetEntity(
                entity.Type, 
                entity.transform.position.Parse(), 
                Quaternion.Euler(Visualization.GetEuler(entity.transform.rotation.eulerAngles)).Parse());
            if (gameEntity.Type == Entity.Player) gameEntity.Entity_Deaded += Defeat;
            else gameEntity.Entity_Deaded += AddScore;
        }
        Cartridge[] cartridges = FindObjectsOfType<Cartridge>();
        foreach(var cartridge in cartridges)
        {
            Destroy(cartridge.gameObject);
        }
    }

    private IEnumerator SpawnEnemy()
    {
        int count = 0;
        while (true)
        {
            system.SpawnEntity(
                Entity.Asteroid,
                new Vector3(9,5,0).Parse(),
                Quaternion.Euler(Visualization.GetRandomEuler()).Parse(),
                AddScore);
            if (++count > 10)
            {
                count = 0;
                system.SpawnEntity(
                    Entity.Alien, 
                    new Vector3(8.8f, 4.8f, 0).Parse(), 
                    Quaternion.Euler(Visualization.GetRandomEuler()).Parse(), 
                    AddScore);
            }
            yield return new WaitForSeconds(frequency);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
