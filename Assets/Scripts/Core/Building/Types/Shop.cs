using System;
using UI;
using UI.View.HUD;
using UI.View.UI;
using UnityEngine;

namespace Core.Building.Types
{
    public class Shop : Building
    {
        private readonly Action _openUI = () => UIManager.Instance.EnterUICanvas<ShopView>();

        public override void Build()
        {
            base.Build();
            UIManager.Instance.GetHUDCanvas<MainButtonsView>().ShopButton.interactable = true;
        }

        protected override void BeforeDestroy()
        {
            UIManager.Instance.GetHUDCanvas<MainButtonsView>().ShopButton.interactable = false;
        }
        
        public override void PrimaryAction()
        {
            BuildInteractionView.EnableSpecialInteractionButton(_openUI);
            base.PrimaryAction();
        }
    }
}