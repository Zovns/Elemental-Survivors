using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public WeaponsManager weaponsManager;
    public Vector3 direction;
    public EnemiesManager enemiesManager;
    public ElementSO element;
    [SerializeField] private GameObject[] bulletPrefabsVisiuals;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float lifetime = 100f;
    void Start()
    {
        element = weaponsManager.GetActiveElement();
        InstantiateBulletVisiual();
        Invoke("LifeTimeEnded", lifetime);
    }

    void InstantiateBulletVisiual()
    {
        
        foreach (GameObject bulletPrefab in bulletPrefabsVisiuals)
        {
            if(bulletPrefab.name == element.elementName)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform);
                bullet.transform.localPosition = Vector3.zero;
            }
          
        }
    }

    private void LifeTimeEnded()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.forward = direction.normalized;
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy && enemy.element.weakAgainst == element)
        {
            enemiesManager.RemoveEnemy(collision.gameObject);
            Destroy(gameObject);
   
           
        }
    
    }
}
