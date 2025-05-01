using System.Collections;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.Rendering;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float orbitRadius = .5f;
    [SerializeField] private float orbitSpeed = 50f;
    [SerializeField] public ElementSO weaponElement;   
    private float lastHitSeconds = 0;
    private float attackCooldown = 1;
    [SerializeField] public Transform player;
    [SerializeField] public EnemiesManager enemiesManager;
    private bool canSpin = true;
    private float attackRange = 7f;
    private float angle = 0f;
   IEnumerator useItem()
    {
       
        Vector3 startPosition = transform.position;

        float timePassed = 0;
        GameObject closestEnemyToPlayer = enemiesManager.GetClosetEnemyTo(player.transform.position);
        if (closestEnemyToPlayer == null)
            yield break;

        canSpin = false;
        Vector3 targetPosition = closestEnemyToPlayer.transform.position;
        float distance = (player.transform.position - targetPosition).magnitude;

        float timeToHit = distance/speed;

        while (timePassed < timeToHit)
        {
            if (closestEnemyToPlayer == null)
            {
                lastHitSeconds = 0;
                canSpin = true;
                yield break;
            }
            float progress = timePassed / timeToHit;
            targetPosition = closestEnemyToPlayer.transform.position;
            Vector3 currentVector3 = Vector3.Slerp(startPosition, targetPosition, progress);
            transform.position = currentVector3;
            timePassed += Time.deltaTime;

            yield return null;

        }

        if (closestEnemyToPlayer == null)
        {
            lastHitSeconds = 0;
            canSpin = true;
            yield break;
        }
        transform.position = closestEnemyToPlayer.transform.position;
        Vector3 enemyPos = transform.position;
        if (weaponElement == closestEnemyToPlayer.GetComponent<Enemy>().element.weakAgainst)
        {
            enemiesManager.RemoveEnemy(closestEnemyToPlayer);
        }
       /* else
        {
            closestEnemyToPlayer.GetComponent<Enemy>().TakeDamage(weaponElement.damage);
        }*/
        
        timePassed = 0;
        while (timePassed < timeToHit)
        {
            float progress = timePassed / timeToHit;
            Vector3 currentVector3 = Vector3.Slerp(enemyPos,spinPosition(), progress);
            transform.position = currentVector3;
            timePassed += Time.deltaTime;

            yield return null;
        }

        lastHitSeconds = 0;
        canSpin = true;
       
    }
    private Vector3 spinPosition()
    {
        angle += orbitSpeed * Time.deltaTime;
        float radians = angle * Mathf.Deg2Rad;

        Vector3 centerPos = player.position;
        float x = centerPos.x + Mathf.Cos(radians) * orbitRadius;
        float z = centerPos.z + Mathf.Sin(radians) * orbitRadius;

        return new Vector3(x, transform.position.y, z);
    }
    private void Spin()
    {
      
        transform.position = spinPosition();
        
    }

   
    private void Update()
    {
        if (player == null) return;

        GameObject closestEnemy = enemiesManager.GetClosetEnemyTo(player.transform.position);
        
        if (canSpin)
        {
            if (closestEnemy == null || (player.transform.position - closestEnemy.transform.position).magnitude > attackRange || lastHitSeconds < attackCooldown)
            {
                Spin();
                lastHitSeconds += Time.deltaTime;
            }
            else
            {
                StartCoroutine(useItem());
            }
          
        }
       
    }
}
