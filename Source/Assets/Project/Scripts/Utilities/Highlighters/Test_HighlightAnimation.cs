using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cofradinn.Modules.Utilities
{
    public class Test_HighlightAnimation : MonoBehaviour
    {
        [SerializeField] private List<HighlightAnimation> _list;
        [SerializeField] private Text _text;
        private HighlightAnimation _currentHL;
        private int _num;

        public void __Next()
        {
            _num++;
            if (_num >= _list.Count)
            {
                _num = 0;
            }
            _currentHL = _list[_num];
            _text.text = _currentHL.name;
        }
        public void __Enable()
        {
            _currentHL.__SetActiveAnimationHighlight(true);
        }
        public void __Disable()
        {
            _currentHL.__SetActiveAnimationHighlight(false);
        }
        public void __StarColor()
        {
            _currentHL._SetStartColor();
        }

        private void Start()
        {
            foreach (var item in _list)
            {
                item._Init();
            }
            _num = 0;
            _currentHL = _list[_num];
            _text.text = _currentHL.name;

        }
    }
}
