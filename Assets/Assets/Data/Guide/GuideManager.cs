using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuideManager : SecondMonoBehaviour
{
    private static GuideManager _instance;
    public static GuideManager Instance => _instance;

    private readonly string _mainMenu = "MainMenu";

    protected override void Awake()
    {
        base.Awake();
        if (GuideManager._instance != null) Debug.LogError("Only 1 GuideManager allow to exist");
        GuideManager._instance = this;
    }

    public void ExitGuide()
    {
        SceneManager.LoadScene(this._mainMenu);
    }
}
