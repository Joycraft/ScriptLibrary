﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Dialog : MonoBehaviour
{
    InputManager inputManager;
    InputPad inputPad;

    [SerializeField]
    ButtonBase button;
    int index;

    public void SetAction(UnityAction submitAction)
    {
        button.OnClick += submitAction;
    }
    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance();
        inputPad = inputManager.CreateInputPad();
        CustomEventSystem.Instance().Enter(button.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (inputPad.IsTrigger(InputType.Button.Right))
        {
            CustomEventSystem.Instance().Click();
        }
        else if (inputPad.IsTrigger(InputType.Button.Down))
        {
            button.OnClick?.Invoke();
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
