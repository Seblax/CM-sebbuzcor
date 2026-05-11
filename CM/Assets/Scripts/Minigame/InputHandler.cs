using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigame
{
    public class InputHandler : MonoBehaviour, IPausable
    {
        public PlayerInputActions inputActions;

        private Vector3 tapPos;

        [SerializeField] bool paused = true;
        public bool IsPaused { get => paused; }
        public Vector3 TapPosition { get => tapPos; }

        
        private bool isTouching = false;

        protected void Awake()
        {
            if (inputActions == null)
                inputActions = new PlayerInputActions();
        }

        protected virtual void OnEnable()
        {
            if (inputActions == null) inputActions = new PlayerInputActions();

            inputActions.Player.Tap.performed += OnTapPerformed;
            inputActions.Player.Touch.performed += OnTouchPerformed;
            inputActions.Player.Enable();

            inputActions.Player.Touch.started += ctx => isTouching = true;
            inputActions.Player.Touch.canceled += ctx => isTouching = false;

            if (MinigameManager.instance != null)
                MinigameManager.instance.Pause += SetPaused;
        }

        protected virtual void OnDisable()
        {
            if (inputActions != null)
            {
                inputActions.Player.Tap.performed -= OnTapPerformed;
                inputActions.Player.Touch.performed -= OnTouchPerformed;
                inputActions.Player.Disable();
            }

            if (MinigameManager.instance != null)
                MinigameManager.instance.Pause -= SetPaused;
        }

        private void OnTapPerformed(InputAction.CallbackContext ctx)
        {
            TapEvent();
        }

        private void OnTouchPerformed(InputAction.CallbackContext ctx)
        {
            if (ctx.valueType == typeof(Vector2))
            {
                Vector2 pos = ctx.ReadValue<Vector2>();
                Vector3 worldPos = ScreenToWorld(pos);
                DragEvent(worldPos);
            }
        }

        private Vector3 ScreenToWorld(Vector2 screenPos)
        {
            float dist = Mathf.Abs(Camera.main.transform.position.z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, dist));
            worldPos.z = 0f;
            return worldPos;
        }

        public virtual void TapEvent()
        {
            throw new System.NotImplementedException();
        }

        public virtual void DragEvent(Vector3 worldPos)
        {
            throw new System.NotImplementedException();
        }

        public virtual void SetPaused(bool isPaused)
        {
            paused = isPaused;
        }

        void Update()
        {
            if (isTouching)
            {
                // Aquí pones lo que quieres que pase MIENTRAS mantiene pulsado
                // Por ejemplo, seguir la posición del dedo
                Vector2 screenPos = inputActions.Player.Touch.ReadValue<Vector2>();
                DragEvent(ScreenToWorld(screenPos));
            }
        }
    }
}