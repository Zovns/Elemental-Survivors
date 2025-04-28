using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public EnemiesManager enemiesManager;
    [SerializeField] private float speed = 15f;
    [SerializeField] private float lifetime = 10f;
    void Start()
    {
        Invoke("LifeTimeEnded", lifetime);
    }

    private void LifeTimeEnded()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemiesManager.RemoveEnemy(collision.gameObject);
            // Handle collision with enemy
   
           
        }
    
    }
}
