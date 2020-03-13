using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Cofradinn.Modules.Utilities
{
    public class ButtonEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public UnityEvent _unityEventPressed;
        public UnityEvent _unityEventUnPressed;
        public void OnPointerDown(PointerEventData eventData)
        {
            _unityEventPressed.Invoke();
            // Debug.Log("activooooooooooo");

        }
        public void OnPointerUp(PointerEventData eventData)
        {
            _unityEventUnPressed.Invoke();
            //  Debug.Log("Desactivooooooooooo");

        }
    }
}