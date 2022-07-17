using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] float loadDelay = 2.5f;
    Animator animator;

    private void Awake() 
    {
        FindObjectOfType<SceneTransition>();    
    }
    public void LoadStartLevel()
    {
        FindObjectOfType<SceneTransition>().MakeTransition();
        StartCoroutine(LoadLevel1());
    }

    IEnumerator LoadLevel1()
    {
        yield return new WaitForSecondsRealtime(loadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

