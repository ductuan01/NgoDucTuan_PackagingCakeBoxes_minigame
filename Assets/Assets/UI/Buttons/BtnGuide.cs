using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnGuide : BaseButton
{
    protected override void OnClick()
    {
        MainManager.Instance.Guide();
    }
}
