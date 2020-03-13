using Cofradinn.Modules.Gui;
using System;
using TMPro;
using UnityEngine;

namespace Cofradinn.Components.PopUps
{
    public class MessageHandler : BasePanelGUI
    {
        [Header("Components:")]
        [SerializeField] private TMP_Text _txtTitle;
        [SerializeField] private TMP_Text _txtDescription;

        private Action<PopUpsEventName> _sendCommand;

        public void __ShowPopUp(string Title, string Description, Action<PopUpsEventName> command)
        {
            _ShowPanel();
            _sendCommand = command;
            _txtTitle.text = Title;
            _txtDescription.text = Description;
        }
        public void __Onclick(PopUpsEventName command)
        {
            _HidePanel();
            _sendCommand?.Invoke(command);
        }

        private void Awake()
        {
            _HidePanel();
        }

        public void __ReceiveEvents(PopUpsEventName eventName)
        {
            _HidePanel();
            _sendCommand?.Invoke(eventName);
        }
    }
}

