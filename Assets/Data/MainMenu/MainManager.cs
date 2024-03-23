using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : SecondMonoBehaviour
{
    private static MainManager _instance;
    public static MainManager Instance => _instance;

    private readonly string _selectLevel = "SelectLevel";

    private readonly string _guide = "Guide";

    protected override void Awake()
    {
        base.Awake();
        if (MainManager._instance != null) Debug.LogError("Only 1 MainManager allow to exist");
        MainManager._instance = this;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(this._selectLevel);
    }

    public void Guide()
    {
        SceneManager.LoadScene(this._guide);
    }
}
