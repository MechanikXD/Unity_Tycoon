using Player.Interactable;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private InteractionTrigger _interaction;
        [SerializeField] private LayerMask _groundMask;

        [SerializeField] private float _moveSpeed = 10f;

        // Left, Top, Right, Bottom
        private Vector4 _moveBounds;

        [SerializeField] private float _scrollSpeed = 3f;
        [SerializeField] private Vector2 _scrollBounds = new Vector2(-10f, 10f);

        [SerializeField] private float _distToScreenEdge = 10f;

        private Vector2 _moveVector;
        private float _scrollValue;
        private Vector2 _mousePosition;

        private void Awake()
        {
            Initialize();
            UpdateInteractionTriggerPosition();
        }

        private void Update()
        {
            UpdatePosition();
        }

        public void OnMove(InputValue value)
        {
            _moveVector = value.Get<Vector2>();
        }

        public void OnMouseMove(InputValue delta)
        {
            _mousePosition = Input.mousePosition;
            UpdateInteractionTriggerPosition();
        }

        public void OnMouseScroll(InputValue delta)
        {
            var scrollDelta = delta.Get<float>() * (_scrollSpeed * Time.deltaTime);
            UpdateScroll(scrollDelta);
        }

        public void OnPrimaryAction() => _interaction.PrimaryAction();
        
        public void OnSecondaryAction() => _interaction.SecondaryAction();

        private void Initialize()
        {
            // TODO: Placeholders
            _moveBounds = new Vector4(-50, 50, 50, -50);

            _mousePosition = Input.mousePosition;
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void UpdatePosition()
        {
            var currentPosition = transform.position;

            if (_moveVector != Vector2.zero)
            {
                currentPosition.x += _moveVector.x * _moveSpeed * Time.deltaTime;
                currentPosition.z += _moveVector.y * _moveSpeed * Time.deltaTime;
            }
            else
            {
                // Right
                if (_mousePosition.x >= Screen.width - _distToScreenEdge &&
                    currentPosition.x <= _moveBounds.z)
                    currentPosition.x += _moveSpeed * Time.deltaTime;

                // Left
                if (_mousePosition.x <= _distToScreenEdge && currentPosition.x >= _moveBounds.x)
                    currentPosition.x -= _moveSpeed * Time.deltaTime;

                // Top
                if (_mousePosition.y >= Screen.height - _distToScreenEdge &&
                    currentPosition.z <= _moveBounds.y)
                    currentPosition.z += _moveSpeed * Time.deltaTime;

                // Bottom
                if (_mousePosition.y <= _distToScreenEdge && currentPosition.z >= _moveBounds.w)
                    currentPosition.z -= _moveSpeed * Time.deltaTime;
            }

            transform.position = currentPosition;
        }

        private void UpdateInteractionTriggerPosition()
        {
            var ray = _camera.ScreenPointToRay(_mousePosition);

            if (Physics.Raycast(ray, out var hit, _groundMask))
            {
                _interaction.transform.position = hit.point;
            }
        }

        private void UpdateScroll(float scrollDelta)
        {
            _scrollValue += scrollDelta;
            if (_scrollValue <= _scrollBounds.x)
            {
                _scrollValue = _scrollBounds.x;
                return;
            }

            if (_scrollValue >= _scrollBounds.y)
            {
                _scrollValue = _scrollBounds.y;
                return;
            }

            var myTransform = transform;
            var currentPosition = myTransform.position;

            currentPosition += scrollDelta * _camera.transform.forward;
            myTransform.position = currentPosition;
        }
    }
}