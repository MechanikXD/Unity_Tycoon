using UnityEngine;

namespace Core.Items
{
    public abstract class ItemActionBase : ScriptableObject
    {
        public abstract void ItemOwned();
        protected virtual void Enabled() {}
        protected virtual void Disabled() {}

        private void OnEnable()
        {
            Enabled();
        }

        private void OnDisable()
        {
            Disabled();
        }
    }
}