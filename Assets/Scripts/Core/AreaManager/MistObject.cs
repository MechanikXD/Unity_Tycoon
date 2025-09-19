using System;
using Core.Resource;
using Player.Interactable;
using TMPro;
using UnityEngine;

namespace Core.AreaManager
{
    public class MistObject : MonoBehaviour, ISceneInteractable
    {
        [SerializeField] private BuildingArea _parent;
        [SerializeField] private TMP_Text _cost;

        public static event Action MistDestroyed;

        private void Start()
        {
            _cost.SetText(_parent.Cost + ResourceManager.GoldTextIcon);
        }

        public void PrimaryAction()
        {
            var goldRequired = new ResourceBundle { Gold = _parent.Cost };
            if (ResourceManager.Instance.HasEnoughResources(goldRequired))
            {
                ResourceManager.Instance.Spend(goldRequired);
                MistDestroyed?.Invoke();
                _parent.UnlockArea(out _);
            }
        }

        public void SecondaryAction() { }
    }
}