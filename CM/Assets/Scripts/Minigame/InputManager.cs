//using Player;
//using System;
//using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.EnhancedTouch;

//namespace Minigame
//{
//    public class InputManager : Singleton<InputManager>
//    {
//        public PlayerInputActions inputActions;

//        public Action TapActions;
//        public Action<Vector3> TouchActions;
//        public Action<Vector3> DragActions;

//        void Awake()
//        {
//            if (instance != null && instance != this)
//            {
//                Destroy(gameObject);
//                return;
//            }

//            DontDestroyOnLoad(gameObject);

//            inputActions = new PlayerInputActions();

//            //inputActions.Player.Touch.performed += x => Drag(Touchscreen.current.primaryTouch.position.ReadValue());
//            //inputActions.Player.Tap.performed += x => Drag(Touchscreen.current.primaryTouch.position.ReadValue());

//            //inputActions.Player.Tap.performed += x => Tap();


//            inputActions.Player.Tap.performed += ctx =>
//            {
//                Debug.LogWarning("Se ta pulsando la pantalla");
//                Tap();
//            };

//            inputActions.Player.Touch.performed += ctx =>
//            {
//                Debug.LogWarning("Se ta tocando la pantalla");

//                Vector2 pos = ctx.ReadValue<Vector2>();
//                Touch(pos);
//                Drag(pos);
//            };
//        }

//        void OnEnable()
//        {
//            inputActions.Enable();
//        }

//        private void Drag(Vector2 drag)
//        {
//            if (DragActions == null) return;

//            float distanceToScene = Mathf.Abs(Camera.main.transform.position.z);

//            Vector3 screenPos = new Vector3(drag.x, drag.y, distanceToScene);
//            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);

//            worldPosition.z = 0f;

//            DragActions?.Invoke(worldPosition);

//            Delegate[] subscribers = DragActions.GetInvocationList();

//            foreach (Delegate subscriber in subscribers)
//            {
//                try
//                {
//                    if (subscriber.Target == null || subscriber.Target.Equals(null))
//                    {
//                        DragActions -= (Action<Vector3>)subscriber;
//                        continue;
//                    }

//                    subscriber.DynamicInvoke();
//                }
//                catch (Exception e)
//                {
//                    // Si un bloque falla, nos dirá CUÁL es, pero no detendrá el sistema
//                    Debug.LogError($"Error en suscriptor de Drag: {subscriber.Method.Name}. Error: {e.Message}");
//                }
//            }
//        }

//        private void Tap()
//        {
//            if (TapActions == null) return;

//            // Obtenemos todos los suscriptores
//            Delegate[] subscribers = TapActions.GetInvocationList();

//            foreach (Delegate subscriber in subscribers)
//            {
//                try
//                {

//                    if (subscriber.Target == null || subscriber.Target.Equals(null))
//                    {
//                        // Limpieza automática de suscriptores muertos
//                        TapActions -= (Action)subscriber;
//                        continue;
//                    }

//                    // Invocamos uno por uno de forma segura
//                    subscriber.DynamicInvoke();
//                }
//                catch (Exception e)
//                {
//                    // Si un bloque falla, nos dirá CUÁL es, pero no detendrá el sistema
//                    Debug.LogError($"Error en suscriptor de Tap: {subscriber.Method.Name}. Error: {e.Message}");
//                }
//            }
//        }

//        private void Touch(Vector2 touch)
//        {
//            if (TouchActions == null) return;

//            float distanceToScene = Mathf.Abs(Camera.main.transform.position.z);

//            Vector3 screenPos = new Vector3(touch.x, touch.y, distanceToScene);
//            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);

//            worldPosition.z = 0f;

//            TouchActions?.Invoke(worldPosition);

//            Delegate[] subscribers = TouchActions.GetInvocationList();

//            foreach (Delegate subscriber in subscribers)
//            {
//                try
//                {
//                    if (subscriber.Target == null || subscriber.Target.Equals(null))
//                    {
//                        TouchActions -= (Action<Vector3>)subscriber;
//                        continue;
//                    }

//                    subscriber.DynamicInvoke();
//                }
//                catch (Exception e)
//                {
//                    // Si un bloque falla, nos dirá CUÁL es, pero no detendrá el sistema
//                    Debug.LogError($"Error en suscriptor de Touch: {subscriber.Method.Name}. Error: {e.Message}");
//                }
//            }
//        }

//        public void Update()
//        {
//            Debug.LogWarning($"Tap: {TapActions} \n Touch: {TouchActions} \n  Tap: {DragActions} \n  ");
//        }
//    }
//}

using Player;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

            if (inputActions == null)
            {
                inputActions = new PlayerInputActions();
            }

            inputActions.Player.Tap.performed -= OnTapPerformed;
            inputActions.Player.Touch.performed -= OnTouchPerformed;

            inputActions.Player.Tap.performed += OnTapPerformed;
            inputActions.Player.Touch.performed += OnTouchPerformed;

            inputActions.Player.Enable();
        }

        private void OnEnable()
        {
            inputActions?.Enable();
        }

        private void OnDisable()
        {
            inputActions?.Disable();
        }

        private void OnTapPerformed(InputAction.CallbackContext ctx)
        {
            Debug.LogWarning("Tap Performed");
            SafeInvoke(TapActions);
        }

        private void OnTouchPerformed(InputAction.CallbackContext ctx)
        {
            if (ctx.valueType == typeof(Vector2))
            {
                Debug.LogWarning("Touch Performed");
                Vector2 pos = ctx.ReadValue<Vector2>();
                Vector3 worldPos = ScreenToWorld(pos);
                SafeInvoke(TouchActions, worldPos);
                SafeInvoke(DragActions, worldPos);
            }
        }

        private Vector3 ScreenToWorld(Vector2 screenPos)
        {
            float dist = Mathf.Abs(Camera.main.transform.position.z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, dist));
            worldPos.z = 0f;
            return worldPos;
        }

        // Sistema de invocación segura para evitar que un suscriptor muerto rompa todo
        private void SafeInvoke(Action action)
        {
            if (action == null) return;
            foreach (Action del in action.GetInvocationList())
            {
                try
                {
                    if (del.Target != null && !del.Target.Equals(null)) del();
                }
                catch (Exception e) { Debug.LogError($"Error en Tap: {e.Message}"); }
            }
        }

        private void SafeInvoke<T>(Action<T> action, T value)
        {
            if (action == null) return;
            foreach (Action<T> del in action.GetInvocationList())
            {
                try
                {
                    if (del.Target != null && !del.Target.Equals(null)) del(value);
                }
                catch (Exception e) { Debug.LogError($"Error en Input: {e.Message}"); }
            }
        }
    }
}