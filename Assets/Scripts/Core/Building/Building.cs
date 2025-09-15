using System.Collections.Generic;
using Player.Interactable;
using UI;
using UI.HUD.View;
using UnityEngine;

namespace Core.Building
{
    public abstract class Building : MonoBehaviour, ISceneInteractable
    {
        [SerializeField] private float _halfHeight;
        private static LayerMask _groundLayer;
        private HashSet<int> _objectsInRange;
        public bool CanBePlaced { get; private set; }

        public float HalfHeight => _halfHeight;

        public abstract void OnBuild();
        public abstract void OnRepositionStart();
        public abstract void OnRepositionEnd();
        public abstract void OnRemove();
        public abstract void OnUpgrade();

        private void Awake()
        {
            _objectsInRange = new HashSet<int>();
            CanBePlaced = true;
            _groundLayer = LayerMask.NameToLayer("Ground");
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer != _groundLayer)
            {
                _objectsInRange.Add(other.gameObject.GetInstanceID());
                CanBePlaced = false;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            _objectsInRange.Remove(other.gameObject.GetInstanceID());
            if (_objectsInRange.Count == 0)
            {
                CanBePlaced = true;
            }
        }

        public virtual void PrimaryAction()
        {
            BuildInteractionView.SetBuilding(this);
            UIManager.Instance.EnterHUDCanvas<BuildInteractionView>();
        }

        public virtual void SecondaryAction() {}
    }
}