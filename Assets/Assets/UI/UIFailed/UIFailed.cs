using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFailed : SecondMonoBehaviour
{
    private static UIFailed _instance;
    public static UIFailed Instance => _instance;

    public bool _isOpen = false;

    protected override void Awake()
    {
        base.Awake();
        if (UIFailed._instance != null) Debug.LogError("Only 1 UIFailed allow to exist");
        UIFailed._instance = this;
        this.Close();
    }

    public void Open()
    {
        gameObject.SetActive(true);
        this._isOpen = true;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        this._isOpen = false;
    }
}
