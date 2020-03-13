using Cofradinn.Components.PopUps;
using Cofradinn.Modules.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Systems.PopUps
{
    public class PopUpsSystem : SingletonComponent<PopUpsSystem>
    {
        [Header("Components")]
      //  [SerializeField] private ConfirmMessageHandler _confirmMessage;
        [SerializeField] private MessageHandler _confirmMessage;
        [SerializeField] private MessageHandler _decisionMessage;
        [SerializeField] private MessageHandler _saveDecisionMessage;

        protected override void OnAwake()
        {

        }
        public void __ShowConfirmPopUp(string Title, string Description, Action<PopUpsEventName> command)
        {
            _confirmMessage.__ShowPopUp(Title, Description, command);
        }
        public void __ShowDecisionPopUp(string Title, string Description, Action<PopUpsEventName> command)
        {
            _decisionMessage.__ShowPopUp(Title, Description, command);
        }
        public void __ShowSaveDecisionPopUp(string Title, string Description, Action<PopUpsEventName> command)
        {
            _saveDecisionMessage.__ShowPopUp(Title, Description, command);
        }
        public void __ClosedPopUp()
        {
            
        }
    }
}
