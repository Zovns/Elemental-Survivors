using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject heartsContainer;
    [SerializeField] private Sprite emptyHeartSprite;
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private int health = 3;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float forceLasts = 1f;
    [SerializeField] private float onHitForce;
    [SerializeField] private float damageCooldown = 1f;
    private bool canDamage = true;
    

    private void ResetDamage()
    {
        canDamage = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
      
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            if (!canDamage)
                return;
            playerController.addForce(enemy.movingDirection.normalized * onHitForce, forceLasts);
            if (health > 0)
            {
                heartsContainer.transform.Find("Heart " + health).GetComponent<Image>().sprite = emptyHeartSprite;
                health--;
                canDamage = false;
                if (health == 0)
                {
                    Time.timeScale = 0;
                    
                }
                Invoke("ResetDamage", damageCooldown);
            }
            



        }
    }
    
}
