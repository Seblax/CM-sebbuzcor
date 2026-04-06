using Player;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigame
{
    public class InputManager : Singleton<InputManager>
    {
        public Action touchEvent;
        public PlayerInputActions inputActions;

        public Action TapActions;
        public Action<Vector3> TouchActions;
        public Action DragActions;

        void Awake()
        {
            inputActions = new PlayerInputActions();

            inputActions.Player.Touch.performed += x => Touch(Touchscreen.current.primaryTouch.position.ReadValue());
            inputActions.Player.Tap.performed += x => Tap();
        }

        void OnEnable()
        {
            inputActions.Enable();
        }

        void OnDisable()
        {
            inputActions.Disable();
        }

        private void Tap()
        {
            TapActions?.Invoke();
        }

        private void Touch(Vector2 touch)
        {
            Vector3 screenPos = new Vector3(touch.x, touch.y, Camera.main.nearClipPlane);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);

            TouchActions?.Invoke(worldPosition);
        }
    }
}