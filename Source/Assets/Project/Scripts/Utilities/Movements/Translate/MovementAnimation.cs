using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Components
{
    public class MovementAnimation : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _speed;
        [SerializeField] private bool _instantly;
        [Header("Settings")]
        [SerializeField] private bool _deltaSpeedActive;
        [SerializeField] private float _deltaSpeed;
        [SerializeField] private float _minDeltaSpeed = 1;
        [SerializeField] private float _maxDeltaSpeed = 1000;
        [Header("ReadOnly")]
        [SerializeField] private bool _isMoving;

        public bool _IsMoving { get => _isMoving; }
        private Vector3 _targetPos;

        private IEnumerator _cor;

        public void _MoveToThePoint(Vector3 target)
        {
            _MoveToThePoint(target, _speed, _instantly);
        }
        public void _MoveToThePoint(Vector3 target, float speed, bool instantly)
        {
            _speed = speed;
            _instantly = instantly;
            _targetPos = target;

            StopAllCoroutines();

            if (!_instantly)
                StartCoroutine(___MovingObject());
            else
                transform.localPosition = _targetPos;
        }
        private IEnumerator ___MovingObject()
        {
            float distance = Vector3.Distance(transform.localPosition, _targetPos);
            float startDistance = distance / _deltaSpeed;

            while (distance > 0.03f)
            {
                _isMoving = true;
                distance = Vector3.Distance(transform.localPosition, _targetPos);

                float deltaSpeed = 0;
                //if (_deltaSpeedActive) deltaSpeed = startDistance;// Mathf.Clamp(distance, _speed, 1000);
                if (_deltaSpeedActive) deltaSpeed = Mathf.Clamp(distance, _minDeltaSpeed, _maxDeltaSpeed) * _deltaSpeed;
                else deltaSpeed = _speed;

                if (distance > 0.03f)
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, _targetPos, deltaSpeed * Time.deltaTime);
                else
                    break;

                yield return null;
            }

            _isMoving = false;
            transform.localPosition = _targetPos;
        }
    }
}
