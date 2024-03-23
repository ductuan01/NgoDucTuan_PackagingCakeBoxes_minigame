using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnGoHome : BaseButton
{
    protected override void OnClick()
    {
        LevelManager.Instance.GoHome();
    }
}
