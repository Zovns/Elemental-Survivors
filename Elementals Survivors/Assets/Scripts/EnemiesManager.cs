using UnityEngine;
using System.Collections.Generic;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private int maxEnemies = 200;
    [SerializeField] private Transform player;
    [SerializeField] public List<GameObject> allEnemiePrefabs = new List<GameObject>();
    /*[SerializeField] private GameObject[] Wave1Enemies;
   [SerializeField] private GameObject[] Wave2Enemies;
   [SerializeField] private GameObject[] wave3Eenemies;
   [SerializeField] private int wavesLength;
   [SerializeField] private TimeManager timeManager;*/
    private List<GameObject> enemiesInScene = new List<GameObject>();
    public void AddEnemy(GameObject enemy,Vector3 pos)
    {
        if (enemiesInScene.Count >= maxEnemies)
        {
            Debug.Log("Max enemies reached");
            return;
        }
        if (allEnemiePrefabs.Contains(enemy))
        {
            GameObject newEnemy = Instantiate(enemy, pos, Quaternion.Euler(0, 0, 0));
            newEnemy.GetComponent<Enemy>().target = player;
            enemiesInScene.Add(newEnemy);
        }
       
    }
    public void RemoveEnemy(GameObject enemy)
    {
        enemiesInScene.Remove(enemy);
        Destroy(enemy);
    }
    public int CountEnemies()
    {
        return enemiesInScene.Count;
    }

    public GameObject GetClosetEnemyTo(Vector3 pos)
    {
        if (enemiesInScene.Count == 0)
        {
            Debug.Log(enemiesInScene.Count);
            return null;
        }
        GameObject closestEnemy = enemiesInScene[0];
        float closestDistance = (enemiesInScene[0].transform.position - pos).magnitude;
        foreach(GameObject enemy in enemiesInScene)
        {
            float distance = (enemy.transform.position - pos).magnitude;
            if ( distance < closestDistance )
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }
        return closestEnemy;
    }
}
 