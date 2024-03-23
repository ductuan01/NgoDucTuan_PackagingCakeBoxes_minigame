using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnLevel : BaseButton
{
    protected override void OnClick()
    {
        SelectLevelManager.Instance.LoadLevel(transform.parent.name);
    }
}
