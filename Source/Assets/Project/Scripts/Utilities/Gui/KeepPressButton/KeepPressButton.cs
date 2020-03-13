/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: KeepPressButton
 * ScriptType: Utility
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: jueves, 13 de febrero de 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Cofradinn.Modules.KeepPressButton
{
    public class KeepPressButton : MonoBehaviour
    {
        private Button _btn;
        private EventTrigger _event;

        private int _startCount = 5;
        private int _resetNumber = 5;
        private int _timer;
        private bool _isPressed;
        private int _count;

        private void Awake()
        {
            _count = _startCount;

               _btn = GetComponent<Button>();
            EventTrigger trigger = _btn.gameObject.AddComponent<EventTrigger>();

            var pointerDown = new EventTrigger.Entry();
            pointerDown.eventID = EventTriggerType.PointerDown;
            pointerDown.callback.AddListener(__OnPointerDown);
            trigger.triggers.Add(pointerDown);

            pointerDown = new EventTrigger.Entry();
            pointerDown.eventID = EventTriggerType.PointerUp;
            pointerDown.callback.AddListener(__OnPointerUp);
            trigger.triggers.Add(pointerDown);

            __ResetTimer();
        }
        private void __OnPointerDown(BaseEventData _event)
        {
            _isPressed = true;
        }
        private void __OnPointerUp(BaseEventData _event)
        {
            _isPressed = false;
        }

        private void Update()
        {
            if (_isPressed)
            {
                if (_timer < 0) __VirtualOnclick();
                else _timer--;
            }
            else
                __ResetTimer();
        }
        private void __VirtualOnclick()
        {
            _btn.onClick.Invoke();
            _count--;
            _count = Mathf.Clamp(_count, 0, _startCount);
            _timer = _resetNumber * _count;
        }
        private void __ResetTimer()
        {
            _timer = 0;
            _count = _startCount;
            _timer = _resetNumber * _count;
        }
    }
}
