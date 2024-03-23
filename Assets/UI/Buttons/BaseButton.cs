using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : SecondMonoBehaviour
{
    [Header("Base Button")]
    [SerializeField] private Button _button;

    protected override void Start()
    {
        base.Start();
        this.AddOnClickEvent();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButton();
    }

    private void LoadButton()
    {
        if (this._button != null) return;
        this._button = transform.GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadButton", gameObject);
    }

    private void AddOnClickEvent()
    {
        this._button.onClick.AddListener(this.OnClick);
    }

    protected abstract void OnClick();
}
