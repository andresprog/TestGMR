using Cofradinn.Components.PopUps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Components.PopUps
{
    public class PopUpsSystemButton : MonoBehaviour
    {
        [SerializeField] private PopUpsEventName _eventName;
        [SerializeField] private MessageHandler _decisionMessageHandler;

        public void __Onclick()
        {
            _decisionMessageHandler.__ReceiveEvents(_eventName);
        }
    }
}