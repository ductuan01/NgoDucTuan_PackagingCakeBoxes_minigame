using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnExitGuide : BaseButton
{
    protected override void OnClick()
    {
        GuideManager.Instance.ExitGuide();
    }
}
