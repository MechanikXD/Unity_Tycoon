using UI;
using UI.HUD.View;
using UnityEngine;

namespace Core.Building.Test
{
    public class TestBuilding : Building
    {
        public override void Destroy()
        {
            base.Destroy();
            UIManager.Instance.ExitHudCanvas<BuildInteractionView>();
            Destroy(gameObject);
        }
    }
}