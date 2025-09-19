using System;
using Core.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View.UI
{
    public class SettingsView : CanvasView
    {
        private Action _eventUnSubscriber;
        [SerializeField] private Button _exitButton;

        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;
        
        private void OnEnable()
        {
            void SetMusicVolume(float value) => AudioManager.Instance.SetMusicVolume(value);
            void SetSfxVolume(float value) => AudioManager.Instance.SetSfxVolume(value);

            _musicSlider.onValueChanged.AddListener(SetMusicVolume);
            _sfxSlider.onValueChanged.AddListener(SetSfxVolume);
            _exitButton.onClick.AddListener(UIManager.Instance.ExitLastCanvas);

            _eventUnSubscriber = () =>
            {
                _musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
                _sfxSlider.onValueChanged.RemoveListener(SetSfxVolume);
            };
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(UIManager.Instance.ExitLastCanvas);
            _eventUnSubscriber();
        }
        
        public override void Show()
        {
            _thisCanvas.enabled = true;
        }

        public override void Hide()
        {
            _thisCanvas.enabled = false;
        }
    }
}