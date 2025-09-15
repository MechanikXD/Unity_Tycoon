using UnityEngine;

namespace UI
{
    public abstract class CanvasView : MonoBehaviour
    {
        [SerializeField] private bool _isActiveOnStart = false;
        [SerializeField] protected Canvas _thisCanvas;

        public bool IsActiveOnStart => _isActiveOnStart;

        public abstract void Show();
        public abstract void Hide();
    }
}