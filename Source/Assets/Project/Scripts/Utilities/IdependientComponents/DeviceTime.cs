using System;
using UnityEngine;
using UnityEngine.UI;

namespace Cofradinn.MainMenu.Settings
{
    public class DeviceTime : IndependentComponent
    {
        [SerializeField] private Text _txtTime; 

        private void Update()
        {
            _txtTime.text = string.Format("{0:hh:mm tt}", DateTime.Now);
        }
    }
}
