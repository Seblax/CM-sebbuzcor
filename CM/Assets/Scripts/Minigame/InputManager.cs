using Player;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigame
{
    public class InputManager : Singleton<InputManager>
    {
        //public PlayerInputActions inputActions;

        //public Action TapActions;
        //public Action<Vector3> TouchActions;
        //public Action<Vector3> DragActions;

        //protected void Awake()
        //{
        //    if (instance != this) return;

        //    if (inputActions == null)
        //        inputActions = new PlayerInputActions();
        //}

        //private void OnEnable()
        //{
        //    if (inputActions == null) inputActions = new PlayerInputActions();

        //    inputActions.Player.Tap.performed += OnTapPerformed;
        //    inputActions.Player.Touch.performed += OnTouchPerformed;
        //    inputActions.Player.Enable();
        //}

        //private void OnDisable()
        //{
        //    if (inputActions != null)
        //    {
        //        inputActions.Player.Tap.performed -= OnTapPerformed;
        //        inputActions.Player.Touch.performed -= OnTouchPerformed;
        //        inputActions.Player.Disable();
        //    }
        //}

        //private void OnTapPerformed(InputAction.CallbackContext ctx)
        //{
        //    Debug.LogWarning("Tap Performed");
        //    SafeInvoke(TapActions);
        //}

        //private void OnTouchPerformed(InputAction.CallbackContext ctx)
        //{
        //    if (ctx.valueType == typeof(Vector2))
        //    {
        //        Debug.LogWarning("Touch Performed");
        //        Vector2 pos = ctx.ReadValue<Vector2>();
        //        Vector3 worldPos = ScreenToWorld(pos);
        //        SafeInvoke(TouchActions, worldPos);
        //        SafeInvoke(DragActions, worldPos);
        //    }
        //}

        //private Vector3 ScreenToWorld(Vector2 screenPos)
        //{
        //    float dist = Mathf.Abs(Camera.main.transform.position.z);
        //    Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, dist));
        //    worldPos.z = 0f;
        //    return worldPos;
        //}

        //// Sistema de invocación segura para evitar que un suscriptor muerto rompa todo
        //private void SafeInvoke(Action action)
        //{
        //    if (action == null) return;
        //    foreach (Action del in action.GetInvocationList())
        //    {
        //        try
        //        {
        //            if (del.Target != null && !del.Target.Equals(null)) del();
        //        }
        //        catch (Exception e) { Debug.LogError($"Error en Tap: {e.Message}"); }
        //    }
        //}

        //private void SafeInvoke<T>(Action<T> action, T value)
        //{
        //    if (action == null) return;
        //    foreach (Action<T> del in action.GetInvocationList())
        //    {
        //        try
        //        {
        //            if (del.Target != null && !del.Target.Equals(null)) del(value);
        //        }
        //        catch (Exception e) { Debug.LogError($"Error en Input: {e.Message}"); }
        //    }
        //}
    }
}