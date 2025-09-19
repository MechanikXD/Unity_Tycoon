using Core.DataSave;
using UI.Data;
using UI.View.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainMenuView
{
    public class MainMenuView : CanvasView
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(LoadGame);
            _newGameButton.onClick.AddListener(LoadNewGame);
            _settingsButton.onClick.AddListener(OpenSettings);
            _exitButton.onClick.AddListener(ExitApplication);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(LoadGame);
            _newGameButton.onClick.RemoveListener(LoadNewGame);
            _settingsButton.onClick.RemoveListener(OpenSettings);
            _exitButton.onClick.RemoveListener(ExitApplication);
        }

        private void LoadGame()
        {
            void LoadScene()
            {
                LoadingView.OnAnimationEnd -= LoadScene;
                SceneManager.LoadScene("GameScene");
            }

            LoadingView.OnAnimationEnd += LoadScene;
            UIManager.Instance.EnterUICanvas<LoadingView>();
        }

        private void LoadNewGame()
        {
            SaveManager.ClearSaveData();
            var itemData = Resources.Load("Item DataSet") as ItemDataSet;
            if (itemData != null)
            {
                foreach (var item in itemData.DataSet)
                {
                    item.IsOwned = false;
                }
            }

            LoadGame();
        }

        private void OpenSettings()
        {
            UIManager.Instance.EnterUICanvas<SettingsView>();
        }

        private void ExitApplication()
        {
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