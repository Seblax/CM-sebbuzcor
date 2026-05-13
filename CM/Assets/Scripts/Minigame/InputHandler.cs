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

        private void Awake()
        {
            if (inputActions == null)
                inputActions = new PlayerInputActions();
        }

        protected virtual void OnEnable()
        {
            // No llames a OnDisable aquí, es redundante si el ciclo de vida es correcto
            if (inputActions == null)
                inputActions = new PlayerInputActions();

            // SUSCRIPCIONES CON MÉTODOS REALES
            inputActions.Player.Tap.performed += OnTapPerformed;
            inputActions.Player.Touch.performed += OnTouchPerformed;

            // Cambiamos las lambdas por métodos para poder desuscribirlos
            inputActions.Player.Tap.performed += OnTapState;

            inputActions.Player.Enable();

            if (MinigameManager.instance != null)
                MinigameManager.instance.Pause += SetPaused;
        }

        protected virtual void OnDisable()
        {
            InputSystem.DisableAllEnabledActions();

            if (inputActions != null)
            {
                inputActions.Player.Disable();

                // DESUSCRIPCIÓN TOTAL
                inputActions.Player.Tap.performed -= OnTapPerformed;
                inputActions.Player.Touch.performed -= OnTouchPerformed;
                inputActions.Player.Tap.started -= OnTapState;

                inputActions.Dispose();
                inputActions = null;
            }

            if (MinigameManager.instance != null)
                MinigameManager.instance.Pause -= SetPaused;

            ResetInputState();
        }

        // 2. La función limpia
        private void OnTapState(InputAction.CallbackContext ctx)
        {
            if (IsPaused) return;

            isTouching = ctx.ReadValueAsButton();
            Debug.Log($"Touch: {Time.frameCount} \t Touch value: {isTouching}");
        }

        private void ResetInputState()
        {
            isTouching = false;
            tapPos = Vector3.zero;
        }

        private void OnTapPerformed(InputAction.CallbackContext ctx)
        {
            if (IsPaused) return;
            
            TapEvent();
        }

        private void OnTouchPerformed(InputAction.CallbackContext ctx)
        {
            if (IsPaused) return;

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
            if (IsPaused) return;
            
            if (isTouching)
            {
                Vector2 screenPos = inputActions.Player.Touch.ReadValue<Vector2>();
                DragEvent(ScreenToWorld(screenPos));
            }
        }
    }
}