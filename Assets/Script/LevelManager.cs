using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    [SerializeField] EnemySpawner enemySpawner;

 

void OnEnable()
{
    // Only run this logic in the gameplay scene
    if (SceneManager.GetActiveScene().name == "SampleScene") // Replace "SampleScene" with your gameplay scene name
    {
        if (enemySpawner == null)
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }

        if (enemySpawner != null)
        {
            Subscribe();
        }
        else
        {
            Debug.LogError("EnemySpawner is not assigned or found in the scene!");
        }
    }
}


void OnDisable()
{
    if (enemySpawner != null)
    {
        Unsubscribe();
    }
}


    void Subscribe()
{
    if (enemySpawner != null)
    {
        enemySpawner.OnAllWavesCompleted += HandleAllWavesCompleted;
    }
    else
    {
        Debug.LogError("EnemySpawner is not assigned in the LevelManager!");
    }
}


    void Unsubscribe()
    {
        if (enemySpawner != null)
        {
            enemySpawner.OnAllWavesCompleted -= HandleAllWavesCompleted;
        }
    }

    void HandleAllWavesCompleted()
    {
        LoadGameOver();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        // Use Invoke to delay the scene loading
        Invoke(nameof(LoadEndScreen), sceneLoadDelay);
    }

    void LoadEndScreen()
    {
        
        SceneManager.LoadScene("EndScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

