
using UnityEngine;
using TMPro;


public class Timer : MonoBehaviour
{
    [SerializeField] float timerDuration = 5f;
    float currentTime;
    
    [SerializeField] TextMeshProUGUI timerText;

    private void Start() 
    {
        currentTime = timerDuration;
        timerText.text = currentTime.ToString();       
    }
    private void FixedUpdate() 
    {
        currentTime -= Time.fixedDeltaTime;
        if(currentTime >= 0)
        {
            currentTime = Mathf.Abs(currentTime);
            timerText.text = currentTime.ToString("f0");
        }
        else
        {
            currentTime = timerDuration;
        }
        
    }
}
