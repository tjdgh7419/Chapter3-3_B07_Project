using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUIBase : UIBase
{
    protected override void Close()
    {
        base.Close();
        UIManager.Instance.OpenUI<ButtonsPanel>();
    }
}
