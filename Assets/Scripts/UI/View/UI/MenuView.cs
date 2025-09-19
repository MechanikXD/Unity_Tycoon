using Core.DataSave;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.View.UI
{
    public class MenuView : CanvasView
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _exitButton;

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(UIManager.Instance.ExitLastCanvas);
            _settingsButton.onClick.AddListener(EnterSettings);
            _menuButton.onClick.AddListener(EnterMainMenu);
            _exitButton.onClick.AddListener(ExitApplication);
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(UIManager.Instance.ExitLastCanvas);
            _settingsButton.onClick.RemoveListener(EnterSettings);
            _menuButton.onClick.RemoveListener(EnterMainMenu);
            _exitButton.onClick.RemoveListener(ExitApplication);
        }

        private void EnterMainMenu()
        {
            void LoadScene()
            {
                LoadingView.OnAnimationEnd -= LoadScene;
                SceneManager.LoadScene("MainMenu");
            }

            LoadingView.OnAnimationEnd += LoadScene;
            UIManager.Instance.EnterUICanvas<LoadingView>();
        }

        private void EnterSettings()
        {
            // SaveManager.SaveToFile();
            UIManager.Instance.EnterUICanvas<SettingsView>();
        }

        private void ExitApplication()
        {
            SaveManager.SaveToFile();
            Application.Quit();
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