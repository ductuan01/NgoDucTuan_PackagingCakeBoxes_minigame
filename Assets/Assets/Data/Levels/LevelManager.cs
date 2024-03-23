using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : SecondMonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance => _instance;

    private readonly string _goHome = "SelectLevel";

    protected override void Awake()
    {
        base.Awake();
        if (LevelManager._instance != null) Debug.LogError("Only 1 LevelManager allow to exist");
        LevelManager._instance = this;
    }

    public void GoHome()
    {
        SceneManager.LoadScene(this._goHome);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(this._goHome);
        }    
    }
}
