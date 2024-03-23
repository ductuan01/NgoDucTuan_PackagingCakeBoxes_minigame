using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICompleted : SecondMonoBehaviour
{
    private static UICompleted _instance;
    public static UICompleted Instance => _instance;

    [SerializeField] private Stars _starsLose;
    [SerializeField] private Stars _starsWin;
    [SerializeField] private LevelProfileSO _levelsProfile;

    public bool _isOpen = false;

    protected override void Awake()
    {
        base.Awake();
        if (UICompleted._instance != null) Debug.LogError("Only 1 UICompleted allow to exist");
        UICompleted._instance = this;
        this.Close();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStarsLose();
        this.LoadStarsWin();
        this.LoadLevelsProfile();
    }

    private void LoadStarsLose()
    {
        if (this._starsLose != null) return;
        this._starsLose = transform.Find("StarsLose").GetComponent<Stars>();
        Debug.LogWarning(transform.name + ": LoadStarsLose", gameObject);
    }
    private void LoadStarsWin()
    {
        if (this._starsWin != null) return;
        this._starsWin = transform.Find("StarsWin").GetComponent<Stars>();
        Debug.LogWarning(transform.name + ": LoadStarsWin", gameObject);
    }
    private void LoadLevelsProfile()
    {
        if (this._levelsProfile != null) return;
        this._levelsProfile = Resources.Load<LevelProfileSO>("LevelsProfileSO");
        Debug.LogWarning(transform.name + ": LoadLevelsProfiles", gameObject);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        this._isOpen = true;
        SetStar();
    }
    public void Close()
    {
        gameObject.SetActive(false);
        this._isOpen = false;
    }

    private int GetStarAmount()
    {
        float timeCount = TimeCount.Instance.TimeCnt;
        if (timeCount > 30f)
        {
            return 3;
        }
        else if (timeCount > 15f)
        {
            return 2;
        }
        else return 1;
    }    

    private void SetStar()
    {
        int starAmount = GetStarAmount();
        for(int i = 0; i < 3; i++)
        {
            if (starAmount > i)
            {
                this._starsWin.stars[i].gameObject.SetActive(true);
            }
            else
            {
                this._starsLose.stars[i].gameObject.SetActive(true);
            }
        }
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        foreach(LevelInfo levelInfo in this._levelsProfile.levelInfos)
        {
            if(levelInfo.levelName == currentSceneName)
            {
                if (starAmount < levelInfo.starAmount) return;
                levelInfo.starAmount = starAmount;
            }    
        }    
    }    
}
