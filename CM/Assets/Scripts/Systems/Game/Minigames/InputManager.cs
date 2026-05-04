using Player;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Minigame
{
    public class InputManager : Singleton<InputManager>
    {
        public PlayerInputActions inputActions;

        public Action TapActions;
        public Action<Vector3> TouchActions;
        public Action<Vector3> DragActions;

        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);

            inputActions = new PlayerInputActions();

            inputActions.Player.Tap.performed += x => Drag(Touchscreen.current.primaryTouch.position.ReadValue());
            inputActions.Player.Touch.performed += x => Drag(Touchscreen.current.primaryTouch.position.ReadValue());

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

        private void Drag(Vector2 drag)
        {
            float distanceToScene = Mathf.Abs(Camera.main.transform.position.z);

            Vector3 screenPos = new Vector3(drag.x, drag.y, distanceToScene);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);

            worldPosition.z = 0f;

            DragActions?.Invoke(worldPosition);
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