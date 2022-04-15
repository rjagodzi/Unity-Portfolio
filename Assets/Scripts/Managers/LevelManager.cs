using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] float timeToLoad = 2f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public IEnumerator LoadingNextLevel(string nextLevel)
    {
        yield return new WaitForSeconds(timeToLoad);

        SceneManager.LoadScene(nextLevel);
    }
}
