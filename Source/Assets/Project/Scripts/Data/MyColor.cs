using UnityEngine;
using System;

namespace Cofradinn.Data
{
    [Serializable]
    public class MyColor
    {
        [SerializeField] private Color _color;
        public Color _Color
        {
            get
            {
                _UpdateMyColor();
                return _color;
            }
            private set
            {
                _color = value;
            }
        }
        public float _r;
        public float _g;
        public float _b;
        public float _a;

        public MyColor(Color color)
        {
            __SetColor(color);
        }

        public MyColor()
        {
            _UpdateMyColor();
        }
        public void _UpdateMyColor()
        {
            _color = new Color(_r, _g, _b, _a);
        }

        public void __SetColor(Color color)
        {
            _color = color;
            this._r = color.r;
            this._g = color.g;
            this._b = color.b;
            this._a = color.a;
        }
    }
}