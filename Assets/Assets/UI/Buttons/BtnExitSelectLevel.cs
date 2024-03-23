using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnExitSelectLevel : BaseButton
{
    protected override void OnClick()
    {
        SelectLevelManager.Instance.ExitSelectLevel();
    }
}
