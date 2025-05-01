using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelBar;
    [SerializeField] private GameObject levelText;
    private Bar levelBarBar;
    //index + 2 is the level and the value is the required xp to reach that level
    [SerializeField] private float[] xpToLevelUp;
    private float levelUpPercentage;
    //for every xp prefab you collect you get xpPrefab * xpMulti
    [SerializeField] private int xpMulti;
    private int level;
    private int xpPoints;

    private int Level
    {
        get { return level; }
        set {
            level = value;
            OnLevelChanged();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        level = 1;
        xpPoints = 0;
        levelBarBar = levelBar.GetComponent<Bar>();
    }
    void Start()
    {
       
    }
    public void AddXpPoints()
    {
        int maxLevel = xpToLevelUp.Length + 1;
        if (Level == maxLevel)
        {
            return;
        }
       
        xpPoints += xpMulti;
        
        if (xpPoints >= xpToLevelUp[Level - 1])
        {
            Level++;
            if (Level == maxLevel)
            {
                levelUpPercentage = 1;
                levelBarBar.Size = levelUpPercentage;
                return;
            }
            xpPoints = 0;
        }
        levelUpPercentage = xpPoints / xpToLevelUp[Level - 1];
        levelBarBar.Size = levelUpPercentage;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Level ++;
        }
    }

    void OnLevelChanged()
    {
        //load upgrades
          
        levelText.GetComponent<TextMeshProUGUI>().text = "Level " + Level;

    }
}
