using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducerPanel : StartUIBase
{
    [SerializeField] RectTransform rectTransform;

    private void Update()
    {
        if (gameObject.active && rectTransform.position.y < 4040)
        {
            float time = Time.deltaTime;
            rectTransform.position += new Vector3(0 , time * 100, 0);
        }
        else
        {
            Close();
            SoundManager.Instance.EffactMusic.SoundStop();
        }
    }

    protected override void Close()
    {
        base.Close();
        rectTransform.position = new Vector2(960, -1920);
        SoundManager.Instance.BackMusic.StartScene();
    }
}
