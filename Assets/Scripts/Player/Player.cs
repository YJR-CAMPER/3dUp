using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerController controller;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        bool sprint = Keyboard.current != null && Keyboard.current.leftShiftKey.isPressed;
        controller.SetRun(sprint);
    }
}
