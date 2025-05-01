using System.Collections;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Rendering;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private EnemiesManager enemiesManager;
    [SerializeField] private Transform player;
    [SerializeField] private int ENEMIES_TO_SWITCH_ELEMENT;
    private int enemiesToSwitchElement;
    [SerializeField] private ElementSO currentSpawningElement;
    private int numOfEenemies = 1;
    private float spawnDistanceFromPlayer = 15f;
    private float secondsBetweenSpawns = 3;
    private float secondsDecreaseRate = .1f;
    private void Start()
    {
        enemiesToSwitchElement = ENEMIES_TO_SWITCH_ELEMENT;
        StartCoroutine(spawnEnemies());
    }
    IEnumerator spawnEnemies()
    {
        while (true)
        {

           
            for (int j = 0; j < 4; j++)
            {
                GameObject chosenEnemy = null;
                foreach (GameObject enemy in enemiesManager.allEnemiePrefabs)
                {
                    if (enemy.GetComponent<Enemy>().element == currentSpawningElement)
                    {
                        chosenEnemy = enemy;
                        enemiesToSwitchElement--;
                        if (enemiesToSwitchElement == 0)
                        {
                            enemiesToSwitchElement = ENEMIES_TO_SWITCH_ELEMENT;
                            SwitchSpawnsElement();
                        }
                        break;
                    }
                }
                // Compute x and y signs using bit manipulation
                float x = ((j & 1) == 0) ? 1f : -1f;
                float y = ((j & 2) == 0) ? 1f : -1f;

                Vector2 direction = new Vector2(x, y).normalized;
                Vector3 spawnPosition = player.position + new Vector3(direction.x, 0, direction.y) * spawnDistanceFromPlayer;
                Collider[] colliders = Physics.OverlapSphere(spawnPosition, chosenEnemy.GetComponent<Enemy>().enemyWidth);

                bool groundDetected = false;
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject.name == "Ground")
                        groundDetected = true;
                }
                int thingsDetected = groundDetected ? colliders.Length - 1 : colliders.Length;
                while (thingsDetected > 0)
                {
                    spawnPosition += new Vector3(direction.x, 0, direction.y) * chosenEnemy.GetComponent<Enemy>().enemyWidth * 2f;
                    groundDetected = false;
                    colliders = Physics.OverlapSphere(spawnPosition, chosenEnemy.GetComponent<Enemy>().enemyWidth);
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        if (colliders[i].gameObject.name == "Ground")
                            groundDetected = true;
                    }
                    thingsDetected = groundDetected ? colliders.Length - 1 : colliders.Length;
                }
                enemiesManager.AddEnemy(chosenEnemy, spawnPosition);

            }
            yield return new WaitForSeconds(secondsBetweenSpawns);
            if (secondsBetweenSpawns > 1.5f)
            {
                secondsBetweenSpawns -= secondsDecreaseRate;
            }

        }
        
        

       

        
    }

    void SwitchSpawnsElement()
    {
        currentSpawningElement = currentSpawningElement.strongAgainst;
    }
}
