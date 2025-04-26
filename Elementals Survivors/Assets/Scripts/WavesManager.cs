using System.Collections;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Rendering;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private EnemiesManager enemiesManager;
    [SerializeField] private Transform player;
    private int numOfEenemies = 1;
    private float spawnDistanceFromPlayer = 10f;
    private float secondsBetweenSpawns = 3;
    private float secondsDecreaseRate = .1f;
    private void Start()
    {
        StartCoroutine(spawnEnemies());
    }
    IEnumerator spawnEnemies()
    {
        while (true)
        {
           
            GameObject enemy = enemiesManager.allEnemiePrefabs[Random.Range(0, enemiesManager.allEnemiePrefabs.Count)];
           
            for (int j = 0; j < 4; j++)
            {
                // Compute x and y signs using bit manipulation
                float x = ((j & 1) == 0) ? 1f : -1f;
                float y = ((j & 2) == 0) ? 1f : -1f;

                Vector2 direction = new Vector2(x, y).normalized;
                Vector3 spawnPosition = player.position + new Vector3(direction.x, 0, direction.y) * spawnDistanceFromPlayer;
                Collider[] colliders = Physics.OverlapSphere(spawnPosition, enemy.GetComponent<Enemy>().enemyWidth);

                bool groundDetected = false;
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject.name == "Ground")
                        groundDetected = true;
                }
                int thingsDetected = groundDetected ? colliders.Length - 1 : colliders.Length;
                while (thingsDetected > 0)
                {
                    spawnPosition += new Vector3(direction.x, 0, direction.y) * enemy.GetComponent<Enemy>().enemyWidth * 2f;
                    groundDetected = false;
                    colliders = Physics.OverlapSphere(spawnPosition, enemy.GetComponent<Enemy>().enemyWidth);
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        if (colliders[i].gameObject.name == "Ground")
                            groundDetected = true;
                    }
                    thingsDetected = groundDetected ? colliders.Length - 1 : colliders.Length;
                }
                enemiesManager.AddEnemy(enemy, spawnPosition);

            }
            yield return new WaitForSeconds(secondsBetweenSpawns);
            if (secondsBetweenSpawns > 1.5f)
            {
                secondsBetweenSpawns -= secondsDecreaseRate;
            }

        }
        
        

       

        
    }
}
