using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShipPrefab;

    [SerializeField]
    private GameObject _asteroidPrefab;

    [SerializeField]
    private GameObject[] powerups;

    private GameManager _gameManager;

    // Use this for initialization
    private void Start ()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        StartCoroutines();
	}

    // Use this for initialization
    public void StartCoroutines()
    {
        StartCoroutine(EnemySpawnRoutine());
        //StartCoroutine(AsteroidSpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
	}
	
	//create a coroutine to spawn the Enemy every 5 seconds
    IEnumerator EnemySpawnRoutine()
    {
        while(_gameManager.gameOver == false)
        {
            float randomX = Random.Range(-7f, 7f);
            Instantiate(_enemyShipPrefab, new Vector3(randomX, 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
	
	//create a coroutine to spawn the Asteroid every 5 seconds
    IEnumerator AsteroidSpawnRoutine()
    {
        while(_gameManager.gameOver == false)
        {
            float randomX = Random.Range(-7f, 7f);
            Instantiate(_asteroidPrefab, new Vector3(randomX, 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(25.0f);
        }
    }

    IEnumerator PowerUpSpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            int randomPowerup = Random.Range(0, 3);
            float randomX = Random.Range(-7f, 7f);
            Instantiate(powerups[randomPowerup], new Vector3(randomX, 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
}
