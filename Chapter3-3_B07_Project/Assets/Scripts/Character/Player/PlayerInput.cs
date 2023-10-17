using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputSystem InputActions { get; private set; }
    public PlayerInputSystem.PlayerActions PlayerActions { get; private set; }

    private void Awake()
    {
        InputActions = new PlayerInputSystem();
        PlayerActions = InputActions.Player;
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable(); 
    }
}
