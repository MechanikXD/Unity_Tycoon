using UI;
using UI.HUD.View;
using UnityEngine;

namespace Core.Building.Test
{
    public class TestBuilding : Building
    {
        private int _buildingLevel;
        
        public override void OnBuild() 
        {
            _buildingLevel = 0;
        }

        public override void OnRepositionStart() { }

        public override void OnRepositionEnd() { }

        public override void OnRemove()
        {
            UIManager.Instance.ExitHudCanvas<BuildInteractionView>();
            Destroy(gameObject);
        }

        public override void OnUpgrade()
        {
            _buildingLevel++;
            Debug.Log($"Building was upgraded to {_buildingLevel} level!");
        }
    }
}