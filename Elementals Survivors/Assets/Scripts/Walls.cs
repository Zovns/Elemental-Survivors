using UnityEngine;

public class Walls : MonoBehaviour
{
    [SerializeField] private float forceLasts = 1f;
    [SerializeField] private float onHitForce;


    private void OnCollisionEnter(Collision collision)
    {
       
        PlayerController playerController = collision.collider.GetComponentInParent<PlayerController>();
        Debug.Log(playerController);
        if (playerController != null)
        {
            Debug.Log("adding force");
            playerController.addForce((collision.collider.transform.position - collision.contacts[0].point).normalized * onHitForce, forceLasts);
        }
        
    }
}
