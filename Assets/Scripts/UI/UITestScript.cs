using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UITestScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, 
        IPointerClickHandler
    {
        public void OnPointerEnter(PointerEventData eventData) { }
        
        public void OnPointerExit(PointerEventData eventData) { }
        
        public void OnPointerClick(PointerEventData eventData) { }
    }
}