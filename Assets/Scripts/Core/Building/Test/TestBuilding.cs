using UnityEngine;

namespace Core.Building.Test
{
    public class TestBuilding : Building
    {
        public override void PrimaryAction()
        {
            Debug.Log($"Primary action performed on {gameObject.name}");
        }

        public override void SecondaryAction()
        {
            Debug.Log($"Secondary action performed on {gameObject.name}");
        }
    }
}