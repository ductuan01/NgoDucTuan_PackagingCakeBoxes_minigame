using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLevel : SecondMonoBehaviour
{
    [SerializeField] private Stars _starsLose;
    public Stars StarsLose => _starsLose;

    [SerializeField] private Stars _starsWin;
    public Stars StarsWin => _starsWin;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStarsLose();
        this.LoadStarsWin();
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
}
