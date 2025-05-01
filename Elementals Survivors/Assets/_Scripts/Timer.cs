using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private int time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerText = gameObject.GetComponent<TextMeshProUGUI>();
        StartCoroutine(CountSeconds());
    }

    public string FormatTime(int seconds)
    {
        int minutes = seconds / 60;
        seconds = seconds % 60;
        string formattedTime = string.Format("{0:D2}:{1:D2}", minutes, seconds);
        return formattedTime;
    }
    void UpdateText()
    {
        timerText.text = FormatTime(time);
    }
    // Update is called once per frame
    IEnumerator CountSeconds()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            time++;
            UpdateText();
        }
        
    }
}
