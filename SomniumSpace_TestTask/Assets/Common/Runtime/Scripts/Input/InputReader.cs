using System;
using Common.Services;
using Common.Services.Tick;
using Common.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputActions;

namespace Common.Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Input/InputReader")]
    public class InputReader:ScriptableObject, IPlayerActions
    {
        private TickService _tickService;
        private InputActions _inputActions;
        public Vector2 MovementInput { get; set; }
        public Vector2 RotationInput { get; set; }

        public void Init() {
            _tickService = ServiceLocator.Get<TickService>();
            if (_inputActions == null) {
                _inputActions = new InputActions();
                _inputActions.Player.SetCallbacks(this);
            }
            _inputActions.Enable();
            
            _tickService.AddTickAction(OnTick);
            _tickService.AddLateTickAction(OnLateTick);
        }
        
        public void DeInit() {
            MovementInput = Vector2.zero;
            RotationInput = Vector2.zero;

            _tickService.RemoveTickAction(OnTick);
            _tickService.RemoveLateTickAction(OnLateTick);
        }

        public InputData FirstAction  = new() {Id = InputId.FIRST_ACTION};
        public InputData SwipeLeft = new() {Id = InputId.SWIPE_LEFT};
        public InputData SwipeRight = new() {Id = InputId.SWIPE_RIGHT};
        public InputData SwipeUp = new() {Id = InputId.SWIPE_UP};
        public InputData SwipeDown = new() {Id = InputId.SWIPE_DOWN};

        private bool _isResetRequired;

        private void OnTick()
        {
        }

        private void OnLateTick()
        {
            if (!_isResetRequired) return;

            FirstAction.IsStart = false;
            FirstAction.IsCancelled = false;
            
            SwipeRight.IsStart = false;
            SwipeRight.IsCancelled = false;
            
            SwipeLeft.IsStart = false;
            SwipeLeft.IsCancelled = false;
            
            SwipeUp.IsStart = false;
            SwipeUp.IsCancelled = false;
            
            SwipeDown.IsStart = false;
            SwipeDown.IsCancelled = false;
            
            _isResetRequired = false;
        }

        private Vector2 _startMousePos;

        private void OnStartFirstAction(InputAction.CallbackContext obj)
        {
            
        }

        private void MouseTick(float time)
        {
            var mousePos = Mouse.current.position.ReadValue();
            var dif = mousePos-_startMousePos;

            if (dif.sqrMagnitude > 1f)
            {
                if (Mathf.Abs(dif.x) > Mathf.Abs(dif.y))
                    if (dif.x > 0)
                    {
                        SwipeRight.IsStart = true;
                        SwipeRight.CallUpdate();
                    }
                    else
                    {
                        SwipeLeft.IsStart = true;
                        SwipeLeft.CallUpdate();
                    }
                else
                    if (dif.y > 0)
                    {
                        SwipeUp.IsStart = true;
                        SwipeUp.CallUpdate();
                    }
                    else
                    {
                        SwipeDown.IsStart = true;
                        SwipeDown.CallUpdate();
                    }

                _isResetRequired = true;
                _tickService.RemoveTickIndexAction(MouseTick);
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    MovementInput = context.ReadValue<Vector2>();
                    break;
                case InputActionPhase.Canceled:
                    MovementInput = Vector2.zero;
                    break;
            }
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    RotationInput = context.ReadValue<Vector2>();
                    break;
                case InputActionPhase.Canceled:
                    RotationInput = Vector2.zero;
                    break;
            }
        }

        public void OnJoystickLook(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            /*switch (context.phase)
            {
                case InputActionPhase.Started:
                    break;
                case InputActionPhase.Performed:
                    break;
                case InputActionPhase.Canceled:
                    break;
            }*/
        }

        public void OnJump(InputAction.CallbackContext context)
        {
        }

        public void OnFirstAction(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    FirstAction.IsStart = true;
                    FirstAction.IsPressed = true;
                    
                    _tickService.AddTickIndexAction(0, MouseTick);

                    _startMousePos = Mouse.current.position.ReadValue();
            
                    _isResetRequired = true;
                    break;
                case InputActionPhase.Performed:
                    break;
                case InputActionPhase.Canceled:
                    FirstAction.IsPressed = false;
                    FirstAction.IsCancelled = true;
                    _tickService.RemoveTickIndexAction(MouseTick);

                    _isResetRequired = true;
                    break;
            }
        }

        public void OnScroll(InputAction.CallbackContext context)
        {
        }

        public void OnEscape(InputAction.CallbackContext context)
        {
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
        }
    }

    public struct InputData
    {
        public InputId Id;
        public event Action UpdateEvent;
        public bool IsStart;
        public bool IsPressed;
        public bool IsCancelled;

        public void CallUpdate() => UpdateEvent.Call();
    }

    public enum InputId
    {
        NONE,
        LEFT,
        UP,
        RIGHT,
        DOWN,
        FIRST_ACTION,
        SECOND_ACTION,
        SWIPE_LEFT,
        SWIPE_UP,
        SWIPE_RIGHT,
        SWIPE_DOWN,
    }
}