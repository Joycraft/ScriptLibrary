﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Dialog : MonoBehaviour
{
    InputManager inputManager;
    InputPad inputPad;

    [SerializeField]
    ButtonBase[] buttons;
    int index;

    public void SetAction(UnityAction cancelAction, UnityAction submitAction)
    {
        buttons[0].OnClick += cancelAction;
        buttons[1].OnClick += submitAction;
    }
    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance();
        inputPad = inputManager.CreateInputPad();
        CustomEventSystem.Instance().Enter(buttons[0].gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (inputPad.IsTrigger(InputType.Button.KeyLeft) || inputPad.IsTrigger(InputType.Button.KeyRight))
        {
            index++;
            if(index > buttons.Length - 1) { index = 0; }
            CustomEventSystem.Instance().Enter(buttons[index].gameObject);
        }
        else if (inputPad.IsTrigger(InputType.Button.Right))
        {
            CustomEventSystem.Instance().Click();
        }
        else if (inputPad.IsTrigger(InputType.Button.Down))
        {
            buttons[0].OnClick?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(inputManager != null)
        {
            inputManager.Remove(inputPad);
        }
    }
}