using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducerPanel : StartSceneBase
{
    [SerializeField] RectTransform rectTransform;

    private void Update()
    {
        if (gameObject.active && rectTransform.position.y < 3000)
        {
            float time = Time.deltaTime;
            rectTransform.position += new Vector3(0 , time * 100, 0);
            Debug.Log(rectTransform.position.y);
        }
        else
        {
            Close();
        }
    }

    protected override void Close()
    {
        base.Close();
        rectTransform.position = new Vector2(960, -1920);
    }
}
