using GameLibrary;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameSystem))]
class GameManager : MonoBehaviour
{
    private GameSystem system;
    [SerializeField]private float frequency;
    public GameObject defeatPanel;
    public Text score;

    private void Awake()
    {
        system = GetComponent<GameSystem>();
    }

    private void Start()
    {
        system.SpawnEntity(Entity.Player,Vector3.zero,Quaternion.Euler(Visualization.GetEuler()),Defeat);
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
            GameEntity gameEntity = system.Factory.GetEntity(entity.type, entity.transform.position, Quaternion.Euler(Visualization.GetEuler()));
            if (gameEntity.type == Entity.Player) gameEntity.Entity_Deaded += Defeat;
            else gameEntity.Entity_Deaded += AddScore;
        }
    }


    private IEnumerator SpawnEnemy()
    {
        int count = 0;
        while (true)
        {
            system.SpawnEntity(Entity.Asteroid,new Vector3(9,5,0),Quaternion.Euler(Visualization.GetEuler()),AddScore);
            if (++count > 10)
            {
                count = 0;
                system.SpawnEntity(Entity.Alien, new Vector3(9, 5, 0), Quaternion.Euler(Visualization.GetEuler()), AddScore);
            }
            yield return new WaitForSeconds(frequency);
        }
    }
}
