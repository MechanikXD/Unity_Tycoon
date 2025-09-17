using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    [RequireComponent(typeof(EventTrigger))]
    public abstract class UIInteractable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, 
        IPointerClickHandler
    {
        public abstract void OnPointerEnter(PointerEventData eventData);

        public abstract void OnPointerExit(PointerEventData eventData);

        public abstract void OnPointerClick(PointerEventData eventData);
    }
}