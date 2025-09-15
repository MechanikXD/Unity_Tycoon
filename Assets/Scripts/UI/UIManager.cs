using System;
using System.Collections.Generic;
using Core.Behaviour.SingletonBehaviour;
using UnityEngine;

namespace UI {
    public class UIManager : SingletonBase<UIManager> {
        private Dictionary<Type, CanvasView> _uiCanvases;
        private Dictionary<Type, CanvasView> _hudCanvases;

        [SerializeField] private CanvasView[] _sceneUiCanvases;
        [SerializeField] private CanvasView[] _sceneHudCanvases;

        private Stack<CanvasView> _uiStack;
        private Action _unsubscribeAction;

        protected override void Awake() {
            ToSingleton(false);
            Initialize();
            SortCanvases();
            DisableCanvases();
        }

        private void OnDisable() => _unsubscribeAction();

        public void EnterUICanvas<T>() where T : CanvasView {
            if (_uiStack.Count > 0) _uiStack.Peek().Hide();
            var canvas = GetUICanvas<T>();
            _uiStack.Push(canvas);
            canvas.Show();
        }

        public void EnterHUDCanvas<T>() where T : CanvasView {
            GetHUDCanvas<T>().Show();
        }

        public void ExitLastCanvas() {
            if (_uiStack.Count > 0) _uiStack.Pop().Hide();

            else _uiStack.Peek().Show();
        }

        public void ExitHudCanvas<T>() where T : CanvasView {
            if (_hudCanvases.TryGetValue(typeof(T), out var hud)) hud.Hide();
        }

        private T GetUICanvas<T>() where T : CanvasView => (T)_uiCanvases[typeof(T)];
        private T GetHUDCanvas<T>() where T : CanvasView => (T)_hudCanvases[typeof(T)];

        private void SortCanvases() {
            foreach (var hudCanvas in _sceneHudCanvases) {
                _hudCanvases.Add(hudCanvas.GetType(), hudCanvas);
            }
            
            foreach (var uiCanvas in _sceneUiCanvases) {
                _uiCanvases.Add(uiCanvas.GetType(), uiCanvas);
            }
        }
        
        private void Initialize() {
            _hudCanvases = new Dictionary<Type, CanvasView>();
            _uiCanvases = new Dictionary<Type, CanvasView>();
            _uiStack = new Stack<CanvasView>();
        }
        
        private void DisableCanvases() {
            foreach (var uiCanvas in _uiCanvases.Values) {
                // Safe exit from canvas (disables only canvas, not gameObject)
                if (!uiCanvas.gameObject.activeInHierarchy) {
                    uiCanvas.gameObject.SetActive(true);
                }
                if (!uiCanvas.IsActiveOnStart) uiCanvas.Hide();
            }
            
            foreach (var hudCanvas in _hudCanvases.Values) {
                // Safe exit from canvas (disables only canvas, not gameObject)
                if (!hudCanvas.gameObject.activeInHierarchy) {
                    hudCanvas.gameObject.SetActive(true);
                }
                if (!hudCanvas.IsActiveOnStart) hudCanvas.Hide();
            }
        }
    }
}