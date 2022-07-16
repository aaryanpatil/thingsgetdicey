
using UnityEngine;
using TMPro;
using UnityEditor;

public class Timer : MonoBehaviour
{
    public float timerDuration = 5f;
    public TextMeshProUGUI timerText;
    
    public float currentTime;
    public bool changeRandomness = false;
    

    private void Start() 
    {
        currentTime = timerDuration;
        timerText.text = currentTime.ToString();       
    }
    private void FixedUpdate()
    {
        RunTimer();
    }

    public bool RunTimer()
    {
        currentTime -= Time.deltaTime;
        if (currentTime >= 0)
        {
            timerText.text = currentTime.ToString("f0");
            return false;
        }
        else
        {
            currentTime = timerDuration;
            changeRandomness = true;
            return changeRandomness;
        }
    }
}
