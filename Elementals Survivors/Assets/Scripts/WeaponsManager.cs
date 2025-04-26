using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] fireWeapons;
    [SerializeField] private GameObject[] WaterWeapons;
    [SerializeField] private GameObject[] GrassWeaons;
    [SerializeField] private GameObject[] electricityWeaons;
    [SerializeField] private GameObject[] earthWeaons;
    [SerializeField] private GameObject activeWeapon;
    [SerializeField] private Transform player;
    [SerializeField] private EnemiesManager enemiesManager;
    private int fireLevel = 0;
    private int waterLevel = 0;
    private int grassLevel = 0;
    private int electricityLevel = 0;
    private int earthLevel = 0;

    private void Start()
    {
        // Initialize with the first weapon
        if (fireWeapons.Length > 0)
        {
           EquipGun("Fire", 0);
        }
    }
    private void UnEquipGun()
    {
        if (activeWeapon == null) return;
        Destroy(activeWeapon);
        activeWeapon = null;
    }

    private void EquipGun(string element,int index)
    {
        UnEquipGun();
        if (element == "Fire" && index < fireWeapons.Length)
        {
            activeWeapon = fireWeapons[index];
            activeWeapon = Instantiate(activeWeapon);
            
        }
        else if (element == "Grass" && index < GrassWeaons.Length)
        {
            activeWeapon = GrassWeaons[index];
            activeWeapon = Instantiate(activeWeapon);
        }
        else if (element == "Water" && index < WaterWeapons.Length)
        {
            activeWeapon = WaterWeapons[index];
            activeWeapon = Instantiate(activeWeapon);
        }
        else if (element == "Electricity" && index < electricityWeaons.Length)
        {
            activeWeapon = electricityWeaons[index];
            activeWeapon = Instantiate(activeWeapon);
        }
        else if (element == "Earth" && index < earthWeaons.Length)
        {
            activeWeapon = earthWeaons[index];
            activeWeapon = Instantiate(activeWeapon);
        }
        if (activeWeapon != null)
        {
            activeWeapon.GetComponent<Weapon>().player = player;
            activeWeapon.GetComponent<Weapon>().enemiesManager = enemiesManager;
        }
    }
    private void EquipNextElement()
    {
        if (activeWeapon == null)
        {
            EquipGun("Fire", fireLevel);
            return;
        }
        string element = activeWeapon.GetComponent<Weapon>().weaponElement.strongAgainst.elementName;
        if (element == "Fire")
        {
            EquipGun(element, fireLevel);
        }
        else if (element == "Water")
        {
            EquipGun(element, waterLevel);
        }
        else if (element == "Grass")
        {
            EquipGun(element, grassLevel);
        }
        else if (element == "Electricity")
        {
            EquipGun(element, electricityLevel);
        }
        else if (element == "Earth")
        {
            EquipGun(element, earthLevel);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            EquipNextElement();
        }
    }
}
