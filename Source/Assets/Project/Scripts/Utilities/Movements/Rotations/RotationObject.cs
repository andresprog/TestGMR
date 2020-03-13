using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Modules.Utilities
{
    public class RotationObject : MonoBehaviour
    {
        [SerializeField] private Vector3 _movement;
        [SerializeField] private bool _move;

        public void __MoveWheels(bool move) 
        {
            _move = move;
        }

        void Update()
        {
            if (!_move) return;

            float x = transform.localEulerAngles.x + _movement.x;
            float y = transform.localEulerAngles.y + _movement.y;
            float z = transform.localEulerAngles.z + _movement.z;

            transform.Rotate(_movement);
        }
    }
}
