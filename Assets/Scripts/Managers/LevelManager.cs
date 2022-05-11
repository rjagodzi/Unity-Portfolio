using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] float timeToLoad = 2f;

    private bool gameIsPaused;

    public int levelToGoTo_1, levelToGoTo_2;
    public LevelExit levelExit_1, levelExit_2;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResumeGame();
        }
    }

    public IEnumerator LoadingNextLevel(int nextLevel)
    {
        AudioManager.instance.PlaySFX(14);

        Time.timeScale = 0.5f;

        UIManager.instance.Fading();

        yield return new WaitForSecondsRealtime(timeToLoad);

        Time.timeScale = 1f;

        SceneManager.LoadScene(nextLevel);
    }

    public void LevelPicker()
    {

        levelToGoTo_1 = SceneManager.GetActiveScene().buildIndex;
        levelToGoTo_2 = SceneManager.GetActiveScene().buildIndex;

        while (levelToGoTo_1 == SceneManager.GetActiveScene().buildIndex)
        {
            int rand = Random.Range(1, SceneManager.sceneCountInBuildSettings - 1);
            print("The random number1 is: " + rand);
            levelToGoTo_1 = rand;
        }

        while(levelToGoTo_2 == levelToGoTo_1 || levelToGoTo_2 == SceneManager.GetActiveScene().buildIndex)
        {
            int rand = Random.Range(1, SceneManager.sceneCountInBuildSettings - 1);
            print("The random number2 is: " + rand);
            levelToGoTo_2 = rand;
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseResumeGame()
    {
        if (!gameIsPaused)
        {
            UIManager.instance.TurnPauseMenuOnOff(true);
            gameIsPaused = true;

            Time.timeScale = 0f;
        }
        else
        {

            UIManager.instance.TurnPauseMenuOnOff(false);
            gameIsPaused = false;

            Time.timeScale = 1f;
        }
    }

    public bool IsGamePaused()
    {
        return gameIsPaused;
    }

}
