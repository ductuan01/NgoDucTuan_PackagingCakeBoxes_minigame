using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCtrl : SecondMonoBehaviour
{
    [SerializeField] private LevelProfileSO _levelProfile;
    public LevelProfileSO LevelProfile => _levelProfile;

    [SerializeField] private Transform _lockLevel;
    [SerializeField] private BtnLevel _btnLevel;
    [SerializeField] private StarLevel _starLevel;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.UnLock();
        this.SetStar();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLevelsProfile();
        this.LoadLockLevel();
        this.LoadBtnLevel();
        this.LoadStarLevel();
    }

    private void LoadLevelsProfile()
    {
        if (this._levelProfile != null) return;
        this._levelProfile = Resources.Load<LevelProfileSO>("LevelsProfileSO");
        Debug.LogWarning(transform.name + ": LoadLevelsProfiles", gameObject);
    }

    private void LoadLockLevel()
    {
        if (this._lockLevel != null) return;
        this._lockLevel = transform.Find("LockLevel").transform;
        Debug.LogWarning(transform.name + ": LoadLockLevel", gameObject);
    }

    private void LoadBtnLevel()
    {
        if (this._btnLevel != null) return;
        this._btnLevel = transform.GetComponentInChildren<BtnLevel>();
        Debug.LogWarning(transform.name + ": LoadBtnLevel", gameObject);
    }

    private void LoadStarLevel()
    {
        if (this._starLevel != null) return;
        this._starLevel = transform.GetComponentInChildren<StarLevel>();
        Debug.LogWarning(transform.name + ": LoadStarLevel", gameObject);
    }

    private void UnLock()
    {
        string currentSceneName = transform.name;

        foreach (LevelInfo levelInfo in this._levelProfile.levelInfos)
        {
            if(levelInfo.levelName == currentSceneName)
            {
                int indexPreviousLevel = this._levelProfile.levelInfos.IndexOf(levelInfo) - 1;
                if (indexPreviousLevel < 0)
                {
                    this._btnLevel.gameObject.SetActive(true);
                    this._lockLevel.gameObject.SetActive(false);
                    continue;
                }
                if (_levelProfile.levelInfos[indexPreviousLevel].starAmount > 0)
                {
                    this._btnLevel.gameObject.SetActive(true);
                    this._lockLevel.gameObject.SetActive(false);
                }
                if (_levelProfile.levelInfos[indexPreviousLevel].starAmount <= 0)
                {
                    this._btnLevel.gameObject.SetActive(false);
                    this._lockLevel.gameObject.SetActive(true);
                }
            }    
        }    
    }
    private void SetStar()
    {
        string currentSceneName = transform.name;

        foreach (LevelInfo levelInfo in this._levelProfile.levelInfos)
        {
            if (levelInfo.levelName == currentSceneName)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (levelInfo.starAmount > i)
                    {
                        this._starLevel.StarsWin.stars[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        this._starLevel.StarsLose.stars[i].gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
