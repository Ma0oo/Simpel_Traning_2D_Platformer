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
        target.y = _clamperY.IsUse ? _clamperY.Clamp(_target.position.y + _offset.y) : transform.position.y;
        target.x = _clamperX.IsUse ? _clamperX.Clamp(_target.position.x + _offset.x) : transform.position.x;
        target.z = _offset.z;
        transform.position = Vector3.MoveTowards(transform.position, target, _speedFollow * Time.deltaTime);
    }
}

[System.Serializable]
public class Clamper
{
    [SerializeField] private bool _isUse;
    [SerializeField] private float _min, _max;

    public bool IsUse => _isUse;

    public float Clamp(float targetValue)
    {
        return Mathf.Clamp(targetValue, _min, _max);
    }
}
