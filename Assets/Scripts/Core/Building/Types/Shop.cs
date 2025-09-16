using System;
using UI.HUD.View;
using UnityEngine;

namespace Core.Building.Types
{
    // Each station must be it's own Type for BuildingManager to record.
    public class Shop : Building
    {
        private Action OpenUI = () => { Debug.Log("Hello World!"); };

        public override void PrimaryAction()
        {
            BuildInteractionView.EnableSpecialInteractionButton(OpenUI);
            base.PrimaryAction();
        }
    }
}