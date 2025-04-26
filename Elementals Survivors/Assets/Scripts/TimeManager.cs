using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float _startTime;
    private bool _isRunning;

    private void Start()
    {
        StartTimer();
    }

    // Call this to start/reset the timer
    public void StartTimer()
    {
        _startTime = Time.time;
        _isRunning = true;
    }

    // Returns raw seconds elapsed
    public float GetSecondsElapsed()
    {
        return _isRunning ? Time.time - _startTime : 0f;
    }

    // Returns formatted time string 
    public string GetFormattedTime()
    {
        int totalSeconds = Mathf.FloorToInt(GetSecondsElapsed());
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        return $"{minutes}:{seconds:D2}"; // Formats seconds with leading zero
    }

    // Optional: Pause/resume functionality
    public void PauseTimer() => _isRunning = false;
    public void ResumeTimer() => _startTime = Time.time - GetSecondsElapsed();
}