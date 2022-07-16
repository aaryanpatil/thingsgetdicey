using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{
    [SerializeField] float waitTime = 1.2f;
    public void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevelAfterDelay(currentSceneIndex));
        
    }

    IEnumerator LoadLevelAfterDelay(int currentSceneIndex)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        SceneManager.LoadScene(currentSceneIndex);
    }
}
