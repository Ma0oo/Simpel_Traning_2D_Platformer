using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class TransformPathMover : MonoBehaviour, IMover
{
    [SerializeField] private List<PathPoint> _pathPoints;
    [SerializeField] private int _indexStartPoint;

    private Animator _animator;
    private Coroutine _actionMove;

    private void OnValidate()
    {
        foreach (var item in _pathPoints)
        {
            item.SetPoint(transform.position);
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        OnPointReached();
    }

    private void OnEnable()
    {
        foreach (var point in _pathPoints)
        {
            point.PointReached += OnPointReached;
        }
    }

    private void OnDisable()
    {
        foreach (var point in _pathPoints)
        {
            point.PointReached -= OnPointReached;
        }
    }

    private void OnPointReached()
    {
        if (_pathPoints != null)
        {
            _indexStartPoint++;
            if (_indexStartPoint >= _pathPoints.Count)
                _indexStartPoint = 0;
            _pathPoints[_indexStartPoint].StartMove(_animator, (cor) =>  _actionMove = StartCoroutine(cor) );
        }
    }

    private void OnDrawGizmosSelected()
    {
        Color[] colors = new Color[] { Color.red, Color.green, Color.blue };
        float _raduisShere = 0.18f;
        if (_pathPoints != null)
        {
            for (int i = 0; i < _pathPoints.Count; i++)
            {
                if (i == _pathPoints.Count - 1)
                    return;
                Gizmos.color = colors[i % colors.Length];
                Gizmos.DrawWireSphere(_pathPoints[i].Point, _raduisShere);
                Gizmos.DrawLine(_pathPoints[i].Point, _pathPoints[i + 1].Point);
            }
        }
    }
}

[System.Serializable]
public class PathPoint
{
    [SerializeField] private string _nameAnimationTrigger;
    [SerializeField] private Vector2 _point;
    [SerializeField] private float _speedToPoint = 1f;

    public event UnityAction PointReached;

    public Vector2 Point => _point;

    public void SetPoint(Vector2 newPoint)
    {
        if (_point == Vector2.zero)
            _point = newPoint;
    }

    public void StartMove(Animator animator, UnityAction<IEnumerator> actionStartCoroutine)
    {
        animator.SetTrigger(_nameAnimationTrigger);
        animator.speed = _speedToPoint;
        actionStartCoroutine?.Invoke(Moving(animator.transform));
    }

    private IEnumerator Moving(Transform transform)
    {
        Vector2 curentPosition = transform.position;
        while (curentPosition != _point)
        {
            transform.position = Vector2.MoveTowards(transform.position, _point, Time.deltaTime * _speedToPoint);
            curentPosition = transform.position;
            yield return null;
        }
        PointReached?.Invoke();
    }
}