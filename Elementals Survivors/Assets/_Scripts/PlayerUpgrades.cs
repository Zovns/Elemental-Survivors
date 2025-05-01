using System.Collections;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgrades : MonoBehaviour
{
    //level 2 power is at index 1
    public float[] magnetDistanceLevels;
    public float[] fireRateLevels ;
    public int magnetLevel = 1;
    public int fireRateLevel = 1;
    private float openSpeed = .02f;
    [SerializeField] private GameObject upgradesFrame;
    [SerializeField] private GameObject magnetFrame;
    [SerializeField] private GameObject fireRateFrame;
    [SerializeField] private Vector3 openedFrameSize = new Vector3(1.3f, 1.3f, 1.3f);
    [SerializeField] private Vector3 closedFrameSize = new Vector3(.1f, .1f, .1f);
    void Start()
    {
        InitMagnetFrame();
        InitFireRateFrame();
    }

    private void InitMagnetFrame()
    {
        void InitMangetFrameVisiuals()
        {
            if (magnetLevel < magnetDistanceLevels.Length)
            {
                magnetFrame.transform.Find("Level").GetComponent<TextMeshProUGUI>().text = "Level " + (magnetLevel + 1);
                float difference = magnetDistanceLevels[magnetLevel - 1] / magnetDistanceLevels[magnetLevel];
                difference *= 100;
                magnetFrame.transform.Find("Stat1Number").GetComponent<TextMeshProUGUI>().text = " +"+difference.ToString("0") + "%";

            }
            else
            {
                magnetFrame.transform.Find("Level").GetComponent<TextMeshProUGUI>().text = "Level MAX";
                magnetFrame.transform.Find("Stat1Number").GetComponent<TextMeshProUGUI>().text = magnetDistanceLevels[magnetLevel - 1].ToString("0") + "m";
            }

        }

        InitMangetFrameVisiuals();

        void OnPickButtonClicked()
        {
            if (magnetLevel < magnetDistanceLevels.Length)
            {
                magnetLevel++;
                InitMangetFrameVisiuals();
            }
            StartCoroutine(CloseUpgradesFrame());
        }
        magnetFrame.transform.Find("Pick").GetComponent<Button>().onClick.AddListener(OnPickButtonClicked);
       

    }

    private void InitFireRateFrame()
    {
        void InitFireRateFrameVisiuals()
        {
            if (fireRateLevel < fireRateLevels.Length)
            {
                fireRateFrame.transform.Find("Level").GetComponent<TextMeshProUGUI>().text = "Level " + (fireRateLevel + 1);
                float difference =  fireRateLevels[fireRateLevel] / fireRateLevels[fireRateLevel - 1];
                difference *= 100;
                fireRateFrame.transform.Find("Stat1Number").GetComponent<TextMeshProUGUI>().text = " +" + difference.ToString("0") + "%";

            }
            else
            {
                fireRateFrame.transform.Find("Level").GetComponent<TextMeshProUGUI>().text = "Level MAX";
                fireRateFrame.transform.Find("Stat1Number").GetComponent<TextMeshProUGUI>().text = fireRateLevels[fireRateLevel - 1].ToString();
            }

        }

        InitFireRateFrameVisiuals();

        void OnPickButtonClicked()
        {
            if (fireRateLevel < fireRateLevels.Length)
            {
                fireRateLevel++;
                InitFireRateFrameVisiuals();
            }
            StartCoroutine(CloseUpgradesFrame());
        }
        fireRateFrame.transform.Find("Pick").GetComponent<Button>().onClick.AddListener(OnPickButtonClicked);


    }
    public IEnumerator OpenUpgradesFrame()
    {
       
        Time.timeScale = 0;
        upgradesFrame.SetActive(true);
        upgradesFrame.transform.localScale = closedFrameSize;
        while (upgradesFrame.transform.localScale.x < openedFrameSize.x)
        {
            upgradesFrame.transform.localScale += new Vector3(openSpeed, openSpeed, openSpeed);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator CloseUpgradesFrame()
    {
       
        
        while (upgradesFrame.transform.localScale.x > closedFrameSize.x)
        {
            upgradesFrame.transform.localScale -= new Vector3(openSpeed, openSpeed, openSpeed);
            yield return new WaitForEndOfFrame();
        }
        upgradesFrame.SetActive(false);
        Time.timeScale = 1;
    }
}
