using System.Collections;
using UI;
using UI.View.UI;
using UnityEngine;

namespace Core.DataSave
{
    public class DataSaver : MonoBehaviour
    {
        [SerializeField] private int _saveDelay = 60;
        private bool _coroutineToken;
        private Coroutine _dataSaveCoroutine;

        private void Start()
        {
            SaveManager.LoadFromFile();
            _coroutineToken = true;
            _dataSaveCoroutine = StartCoroutine(SaveGameOnDelay());
            UIManager.Instance.GetUICanvas<InventoryView>().ActivateOwnedItems();
        }

        private IEnumerator SaveGameOnDelay()
        {
            while (_coroutineToken)
            {
                yield return new WaitForSeconds(_saveDelay);
                SaveManager.SaveToFile();
            }
        }

        private void OnDisable()
        {
            StopCoroutine(_dataSaveCoroutine);
        }
    }
}