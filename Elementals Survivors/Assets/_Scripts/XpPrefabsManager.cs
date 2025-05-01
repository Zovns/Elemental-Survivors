using UnityEngine;
using System.Collections.Generic;

public class XpPrefabsManager : MonoBehaviour
{
  
    [SerializeField] private Transform player;
    [SerializeField] private GameObject xpPrefab;
    [SerializeField] private LevelManager levelManager;
   
    private List<GameObject> xpPrefabsInScene = new List<GameObject>();
    public void AddXp(Vector3 pos)
    {
        GameObject newXp = Instantiate(xpPrefab, pos, Quaternion.identity);
        XP xp = newXp.GetComponent<XP>();
        xp.player = player;
        xp.xpPrefabsManager = this;
        
        xpPrefabsInScene.Add(newXp);
    }
    public void RemoveXp(GameObject xpPrefab)
    {
        xpPrefabsInScene.Remove(xpPrefab);
        Destroy(xpPrefab);
        levelManager.AddXpPoints();
    }
    public int CountXpPrefabs()
    {
        return xpPrefabsInScene.Count;
    }

    
}
 