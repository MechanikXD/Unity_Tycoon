using System;
using UI.HUD.View;

namespace Core.Building.Types
{
    // Each station must be it's own Type for BuildingManager to record.
    public class CraftStation : Building
    {
        private Action OpenUI = () => { };

        public override void PrimaryAction()
        {
            BuildInteractionView.EnableSpecialInteractionButton(OpenUI);
            base.PrimaryAction();
        }
    }
}