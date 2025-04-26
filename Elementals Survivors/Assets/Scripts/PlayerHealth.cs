using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float forceLasts = 1f;
    [SerializeField] private float onHitForce;

    private void OnCollisionEnter(Collision collision)
    {
      
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            playerController.addForce(enemy.movingDirection.normalized * onHitForce, forceLasts);
            if (health > 0)
            {
                
                health--;
                Debug.Log(health);
            }
            else
            {
                Debug.Log("Player already dead");
            }



        }
    }
    
}
