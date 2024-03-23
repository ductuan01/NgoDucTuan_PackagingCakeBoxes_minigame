using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : SecondMonoBehaviour
{
    [SerializeField] private List<Transform> _stars;
    public List<Transform> stars => _stars;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStarsLose();
    }

    private void LoadStarsLose()
    {
        if (this._stars.Count > 1) return;
        foreach(Transform star in this.transform)
        {
            this._stars.Add(star);
            star.gameObject.SetActive(false);
        }    
        Debug.LogWarning(transform.name + ": LoadStarsLose", gameObject);
    }
}
