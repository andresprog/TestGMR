using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Modules.Utilities
{
    public class FollowTo : MonoBehaviour
    {
        public Transform _target;
        public Vector3 _delta;

        private void Update()
        {
            if (_target == null) return;

            transform.position = _target.position + _delta;
            transform.rotation = _target.rotation;
        }
    }
}
