using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreferencesPanel : StartUIBase
{
    protected override void Close()
    {
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            base.Close();
        }
        if (SceneManager.GetActiveScene().name == "UI_DEV_Scene")
        {
            gameObject.SetActive(false);
            UIManager.Instance.OpenUI<PausePanel>();
        }
    }
}
