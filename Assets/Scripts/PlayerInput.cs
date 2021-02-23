using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public UnityAction KeyRightPressed;
    public UnityAction KeyLeftPressed;
    public UnityAction KeyJumptPressed;
    public UnityAction KeyDownPressed;
    public UnityAction KeyMoveUnpressed;


    private void Update()
    {
        KeyCheck(KeyCode.A, Input.GetKey, KeyLeftPressed);
        KeyCheck(KeyCode.D, Input.GetKey, KeyRightPressed);
        KeyCheck(KeyCode.Space, Input.GetKeyDown, KeyJumptPressed);
        KeyCheck(KeyCode.S, Input.GetKey, KeyDownPressed);
        KeysCheck(new KeyCode[] { KeyCode.A, KeyCode.D }, Input.GetKeyUp, KeyMoveUnpressed);
    }

    private void KeyCheck(KeyCode keyCode, Func<KeyCode, bool> getKey, UnityAction action)
    {
        if (getKey(keyCode))
            action?.Invoke();
    }
    private void KeysCheck(KeyCode[] keyCode, Func<KeyCode, bool> getKey, UnityAction action)
    {
        foreach (var key in keyCode)
        {
            KeyCheck(key, getKey, action);
        }
    }
}
