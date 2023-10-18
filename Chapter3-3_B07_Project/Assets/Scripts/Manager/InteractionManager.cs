using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public Action OnShowWindow;
    public Action OnCloseWindow;

	
	public void CallShowWindow()
    {
        OnShowWindow?.Invoke();
    }
    public void CallCloseWindow()
    {
        OnCloseWindow?.Invoke();
    }
}
