﻿// ==============================
// !@ biref 使用されるコントローラ
// !@ author Takayoshi Ogawa
// !@ since 2017/05/12
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamepad : MonoBehaviour
{
    /// <summary>
    /// ボタンの入力情報 
    /// </summary>
    ///
    /// UnityのInputを使用して設定されたButtonNameが
    /// "押された瞬間","押されているとき","離れた瞬間"を
    /// それぞれ取得できるように更新処理が行われている
    ///
    public struct Button
    {
        public bool down;
        public bool trigger;
        public bool relese; 

        public void GetButton(string buttonName)
        {
            down = Input.GetButton(buttonName);
            trigger = Input.GetButtonDown(buttonName);
            relese = Input.GetButtonUp(buttonName);
        }
    }

    [HideInInspector] public Vector2 leftStick;
    [HideInInspector] public Vector2 rightStick;
    [HideInInspector] public float triggerButton;
    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;

    [HideInInspector] public Button buttonA;
    [HideInInspector] public Button buttonB;
    [HideInInspector] public Button buttonX;
    [HideInInspector] public Button buttonY;
    [HideInInspector] public Button leftButton;
    [HideInInspector] public Button rightButton;

    [HideInInspector] public Button backButton;
    [HideInInspector] public Button startButton;
    [HideInInspector] public Button leftStickPress;
    [HideInInspector] public Button rightStickPress;

    void Update() {

        leftStick.x = Input.GetAxis("Horizontal");
        leftStick.y = Input.GetAxis("Vertical");

        rightStick.x = Input.GetAxis("Picth");
        rightStick.y = Input.GetAxis("Yaw");
        triggerButton = Input.GetAxis("Roll");

        buttonA.GetButton("Fire1");
        buttonB.GetButton("Fire2");
        buttonX.GetButton("Fire3");
        buttonY.GetButton("Jump");

        leftButton.GetButton("LeftShoulder");
        rightButton.GetButton("RightShoulder");
        backButton.GetButton("BackButton");
        startButton.GetButton("StartButton");
        leftStickPress.GetButton("LeftStickPress");
        rightStickPress.GetButton("RightStickPress");

        horizontal = Input.GetAxis("HorizontalKey");
        vertical = Input.GetAxis("VerticalKey");
    }
}
