using System;
using System.Collections;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.View.UI
{
    public class LoadingView : CanvasView
    {
        private Coroutine _currentCoroutine;
        [SerializeField] private bool _disableOnSceneLoaded;
        
        [SerializeField] private Image _overlay;
        [SerializeField] private float _fadeInOutSpeed;
        
        [SerializeField, CanBeNull] private TMP_Text _text;
        [SerializeField] private string _textLabel = "Loading...";
        
        [SerializeField] private Color _transparentColor = new Color(0, 0, 0, 0);
        [SerializeField] private Color _fullColor = new Color(0, 0, 0, 1);

        public static event Action OnAnimationEnd;
        
        private void Awake()
        {
            if (_text != null)
            {
                _text.SetText(_textLabel);
            }

            if (_disableOnSceneLoaded)
            {
                void HideCanvas(Scene scene, LoadSceneMode mode)
                {
                    SceneManager.sceneLoaded -= HideCanvas;
                    Hide();
                }

                SceneManager.sceneLoaded += HideCanvas;
            }
        }

        private IEnumerator FadeIn()
        {
            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
            _overlay.color = _transparentColor;
            _thisCanvas.enabled = true;
            yield return null;  // Start from next frame

            var elapsed = 0f;
            while (_overlay.color != _fullColor)
            {
                elapsed += Time.deltaTime;
                _overlay.color = Color.Lerp(_transparentColor, _fullColor,
                    _fadeInOutSpeed * elapsed);
                yield return null;
            }

            if (_text != null) _text.enabled = true;
            OnAnimationEnd?.Invoke();
            _currentCoroutine = null;
        }
        
        private IEnumerator FadeOut()
        {
            if (_text != null) _text.enabled = false;
            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
            _overlay.color = _fullColor;
            yield return null;  // Start from next frame

            var elapsed = 0f;
            while (_overlay.color != _transparentColor)
            {
                elapsed += Time.deltaTime;
                _overlay.color = Color.Lerp(_fullColor, _transparentColor,
                    _fadeInOutSpeed * elapsed);
                yield return null;
            }
            
            OnAnimationEnd?.Invoke();
            _currentCoroutine = null;
            _thisCanvas.enabled = false;
        }
        
        public override void Show()
        {
            _currentCoroutine = StartCoroutine(FadeIn());
        }

        public override void Hide()
        {
            _currentCoroutine = StartCoroutine(FadeOut());
        }
    }
}