using ClumsyWizard.Utilities;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : Persistant<InputManager>
{
    public Action<Vector2> OnPointerDown;
    public Vector2 InputAxis { get; private set; }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            OnPointerDown?.Invoke(mouseWorldPosition);
        }

        InputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    protected override void CleanUpStaticData()
    {
        OnPointerDown = null;
    }
}