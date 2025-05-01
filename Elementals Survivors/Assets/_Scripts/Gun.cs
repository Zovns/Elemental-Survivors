using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private PlayerUpgrades playerUpgrades;
    [SerializeField] GameObject player;
    [SerializeField] WeaponsManager weaponsManager;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] EnemiesManager enemiesManager;

    [SerializeField] private bool canShoot = true;

    private void shoot(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.Find("Muz").position,Quaternion.Euler(90,0,0));
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.weaponsManager = weaponsManager;
        bulletScript.direction = direction;
        bulletScript.enemiesManager = enemiesManager;
    }

    private void ResetCanShoot()
    {
        canShoot = true;
    }

    private void Update()
    {
        // Works on PC and Mobile:
        bool isPressed = false;
        Vector2 screenPosition = Vector2.zero;

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Mouse.current.leftButton.isPressed)
        {
            isPressed = true;
            screenPosition = Mouse.current.position.ReadValue(); ;
        }
#elif UNITY_ANDROID || UNITY_IOS
       if (Touchscreen.current.touches.Count > 0)
        {
            // Get the first touch
            var touch = Touchscreen.current.touches[0];

            // Check if the touch phase is 'Began' (touch started)
            if (touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
            {
                isPressed = true;
                screenPosition = touch.position.ReadValue();
            }
        }
#endif

        if (isPressed && canShoot)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
               

                Vector3 hitPointPos = new Vector3(hit.point.x, player.transform.position.y, hit.point.z);
                Vector3 direction = hitPointPos - player.transform.position;
                player.transform.LookAt(hitPointPos);

                canShoot = false;
                shoot(direction);
                Invoke(nameof(ResetCanShoot), playerUpgrades.fireRateLevels[playerUpgrades.fireRateLevel - 1]);
            }
        }
    }
}
