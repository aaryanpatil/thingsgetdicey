using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{

    [SerializeField] float loadLevelDelay = 2f;
    private bool sfxPlayed = false;
    AudioManager audioManager;

    private void Awake()
    {
       audioManager = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player") )
        {
            FindObjectOfType<PlayerMovement>().enabled = false;
            audioManager.Play("Level Complete", 0f);
            sfxPlayed = true;
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(loadLevelDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene("Credits");
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
