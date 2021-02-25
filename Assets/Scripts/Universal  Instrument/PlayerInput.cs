using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public UnityAction KeyRightPressed;
    public UnityAction KeyLeftPressed;
    public UnityAction KeyJumptPressed;
    public UnityAction KeysMoveUnpressed;


    private void Update()
    {
        KeyCheck(KeyCode.A, Input.GetKey, KeyLeftPressed);
        KeyCheck(KeyCode.D, Input.GetKey, KeyRightPressed);
        KeyCheck(KeyCode.Space, Input.GetKeyDown, KeyJumptPressed);

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                KeysMoveUnpressed?.Invoke();
    }

    private void KeyCheck(KeyCode keyCode, Func<KeyCode, bool> getKey, UnityAction action)
    {
        if (getKey(keyCode))
            action?.Invoke();
    }
}
