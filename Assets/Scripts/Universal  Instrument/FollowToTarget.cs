using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToTarget : MonoBehaviour
{
    [SerializeField] private float _speedFollow;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _target;
    [SerializeField] private Clamper _clamperX;
    [SerializeField] private Clamper _clamperY;

    private void Update()
    {
        Vector3 target = Vector3.zero;
        if (!_clamperY.TryClamp(ref target.y, _target.position.y + _offset.y))
            target.y = transform.position.y;
        if (!_clamperX.TryClamp(ref target.x, _target.position.x + _offset.x))
            target.x = transform.position.x;
        target.z = _offset.z;
        transform.position = Vector3.MoveTowards(transform.position, target, _speedFollow * Time.deltaTime);
    }
}

[System.Serializable]
public class Clamper
{
    [SerializeField] private bool _isOn;
    [SerializeField] private float _min, _max;

    public bool TryClamp(ref float targetValue, float valueToClamp)
    {
        if (_isOn)
        {
            targetValue = Mathf.Clamp(valueToClamp, _min, _max);
            return true;
        }
        return false;
    }
}
