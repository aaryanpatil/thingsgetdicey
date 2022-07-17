
using UnityEngine;
using TMPro;
using UnityEditor;

public class Timer : MonoBehaviour
{
    public float timerDuration = 5f;
    public TextMeshProUGUI timerText;
    
    public float currentTime;
    public bool changeRandomness = false;

    AudioManager audioManager;
    
    private void Awake() 
    {
        audioManager = FindObjectOfType<AudioManager>();    
    }
    private void Start() 
    {
        currentTime = timerDuration;
        timerText.text = currentTime.ToString();       
    }
    private void FixedUpdate()
    {
        RunTimer();
    }

    void RunTimer()
    {
        currentTime -= Time.deltaTime;
        if (currentTime >= 0)
        {
            timerText.text = currentTime.ToString("f0");
        }
        else
        {
            audioManager.Play("Change SFX", 0f);
            currentTime = timerDuration;
            
        }
    }
}
