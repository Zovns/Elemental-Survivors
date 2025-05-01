using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class XP : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float speed = .0005f;
    [SerializeField] private GameObject magnetingXpObject;
    public XpPrefabsManager xpPrefabsManager;
    private PlayerUpgrades playerUpgrades;

    private bool moving = false;
    

    IEnumerator Initialize()
    {
        if (player == null)
        {
            yield return new WaitUntil(() => player != null);
        }
      
        playerUpgrades = player.gameObject.GetComponent<PlayerUpgrades>();
    }
    void Start()
    {
        StartCoroutine(Initialize());

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;

        if (!moving)
        {
            float distance = Vector3.Distance(transform.position, player.position);
           
            if (distance < playerUpgrades.magnetDistanceLevels[playerUpgrades.magnetLevel -1])
            {
                Debug.Log("Distance is : " + distance + " And magnet power is " + playerUpgrades.magnetDistanceLevels[playerUpgrades.magnetLevel - 1]);
                moving = true;
                StartCoroutine(MoveXpToPlayer());
            }
        }
       
    }

    IEnumerator MoveXpToPlayer()
    {
       
        magnetingXpObject.GetComponent<MeshRenderer>().enabled = true;
        Vector3 startPosition = transform.position;

        float timePassed = 0;


        Vector3 targetPosition = player.position + new Vector3(0,3,0);
        float distance = (transform.position - targetPosition).magnitude;

        float timeToHit = distance / speed;

        while (timePassed < timeToHit)
        {
            
            float progress = timePassed / timeToHit;
            targetPosition = player.position;
            Vector3 currentVector3 = Vector3.Slerp(startPosition, targetPosition, progress);
            transform.position = currentVector3;
            timePassed += Time.deltaTime;

            yield return null;

        }


        transform.position = player.position;

        xpPrefabsManager.RemoveXp(gameObject);



    }
}
