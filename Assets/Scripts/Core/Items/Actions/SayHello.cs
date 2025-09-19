using UnityEngine;

namespace Core.Items.Actions
{
    [CreateAssetMenu(fileName = "Say Hello", menuName = "ScriptableObjects/Item Actions/Say Hello")]
    public class SayHello : ItemActionBase
    {
        public override void ItemOwned()
        {
            Debug.Log("Hello World");
        }
    }
}