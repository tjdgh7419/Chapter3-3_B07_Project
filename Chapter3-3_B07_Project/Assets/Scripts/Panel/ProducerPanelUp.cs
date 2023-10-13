using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducerPanelUp : MonoBehaviour
{
    private RectTransform _rectTransform;
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.anchoredPosition = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (_rectTransform.anchoredPosition.y < 4920)
        {
            float time = Time.deltaTime;
            _rectTransform.anchoredPosition += new Vector2(0, time * 100);
        }
    }
}
