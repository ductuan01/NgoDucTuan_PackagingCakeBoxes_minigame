using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCount : SecondMonoBehaviour
{
    private static TimeCount _instance;
    public static TimeCount Instance => _instance;

    [SerializeField] private TextMeshProUGUI _timeText;

    private float _timeCount = 46f;
    public float TimeCnt => _timeCount;

    public bool IsWin { get; set; }

    protected override void Awake()
    {
        base.Awake();
        if (TimeCount._instance != null) Debug.LogError("Only 1 TimeCount allow to exist");
        TimeCount._instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTimeText();
    }

    private void LoadTimeText()
    {
        if (this._timeText != null) return;
        this._timeText = transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadTimeText", gameObject);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        while (_timeCount > 0 && !IsWin)
        {
            yield return new WaitForSeconds(1f);
            _timeCount -= 1f;
            if(_timeCount < 10) _timeText.SetText("00:0" + _timeCount.ToString());
            else _timeText.SetText("00:" + _timeCount.ToString());
        }
        if(!IsWin) UIFailed.Instance.Open();
    }    
}
