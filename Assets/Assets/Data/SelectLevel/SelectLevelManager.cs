using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelManager : SecondMonoBehaviour
{
    private static SelectLevelManager _instance;
    public static SelectLevelManager Instance => _instance;

    private readonly string _mainMenu = "MainMenu";

    protected override void Awake()
    {
        base.Awake();
        if (SelectLevelManager._instance != null) Debug.LogError("Only 1 SelectLevelManager allow to exist");
        SelectLevelManager._instance = this;
    }

    public void ExitSelectLevel()
    {
        SceneManager.LoadScene(this._mainMenu);
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}
