using System;
using UI;
using UI.View.HUD;
using UI.View.UI;

namespace Core.Building.Types
{
    public class CraftStation : Building
    {
        private readonly Action _openUI = () => UIManager.Instance.EnterUICanvas<CraftView>();

        public override void Build()
        {
            base.Build();
            UIManager.Instance.GetHUDCanvas<MainButtonsView>().CraftButton.interactable = true;
        }

        protected override void BeforeDestroy()
        {
            UIManager.Instance.GetHUDCanvas<MainButtonsView>().CraftButton.interactable = false;
        }

        public override void PrimaryAction()
        {
            BuildInteractionView.EnableSpecialInteractionButton(_openUI);
            base.PrimaryAction();
        }
    }
}