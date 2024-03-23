using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnResetLevel : BaseButton
{
    protected override void OnClick()
    {
        LevelManager.Instance.ResetLevel();
    }
}
