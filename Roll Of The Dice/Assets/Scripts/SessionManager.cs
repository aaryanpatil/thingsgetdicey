using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SessionManager : MonoBehaviour
{
    [SerializeField] float waitTime = 1.2f;
    [SerializeField] TextMeshProUGUI gravityValue;
    [SerializeField] TextMeshProUGUI speedValue;
    [SerializeField] TextMeshProUGUI wallStickValue;
    [SerializeField] Color32 highColor;
    [SerializeField] Color32 normalColor;
    [SerializeField] Color32 lowColor;
    [SerializeField] Color32 reverseColor;

    PlayerMovement player;

    private void Awake() 
    {
        player = FindObjectOfType<PlayerMovement>();    
    }

    private void Update() 
    {
        UpdateValues();    
    }

    void UpdateValues()
    {
        UpdateGravity(); 
        UpdateSpeed();
        UpdateWallStick();
        
    }

    void UpdateWallStick()
    {
        wallStickValue.overrideColorTags = true;
        if (player.wallStick == "true")
        {
            wallStickValue.color = highColor;
        }
        else
        {
            wallStickValue.color = normalColor;
        }
        wallStickValue.text = player.wallStick;
    }

    void UpdateGravity()
    {
        gravityValue.overrideColorTags = true;
        if (player.currentGravity == "high")
        {
            gravityValue.color = highColor;
        }
        else if (player.currentGravity == "low")
        {
            gravityValue.color = lowColor;
        }
        else if (player.currentGravity == "reverse")
        {
            gravityValue.color = reverseColor;
        }
        else 
        {
            
            gravityValue.color = normalColor;
        }
        gravityValue.text = player.currentGravity;
    }

    void UpdateSpeed()
    {
        speedValue.overrideColorTags = true;
        if (player.runSpeedScale == "high")
        {
            speedValue.color = highColor;
        }
        else if (player.runSpeedScale == "low")
        {
            speedValue.color = lowColor;
        }
        else 
        {       
            speedValue.color = normalColor;
        }
        speedValue.text = player.runSpeedScale;
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        
    }

    public void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(ReloadLevelAfterDelay(currentSceneIndex));
        
    }

    IEnumerator ReloadLevelAfterDelay(int currentSceneIndex)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        SceneManager.LoadScene(currentSceneIndex);
    }


}
