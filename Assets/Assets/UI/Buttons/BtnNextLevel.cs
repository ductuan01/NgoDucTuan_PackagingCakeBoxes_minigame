using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnNextLevel : BaseButton
{
    protected override void OnClick()
    {
        LevelManager.Instance.NextLevel();
    }
}
