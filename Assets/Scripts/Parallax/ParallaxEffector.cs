using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class ParallaxEffector : MonoBehaviour
{
    [SerializeField] private Vector2 _leftPoint;
    [SerializeField] private Vector2 _rightPoint;
    [SerializeField] private Rigidbody2D _rigidbodyTarget;
    [Range(0, 2f)] [SerializeField] private float _factorLenghtToSpawn;
    [Range(0, 2f)] [SerializeField] private float _factorLenghtToDelete;

    private List<ParallaxElement> _parallaxElements;

    public float Width => _rightPoint.x - _leftPoint.x;

    private void Awake()
    {
        _parallaxElements = GetComponentsInChildren<ParallaxElement>().ToList();
    }

    private void Update()
    {
        foreach (var element in _parallaxElements)
        {
            element.MoveWithTarget(_rigidbodyTarget.velocity);
            element.TryDelete(_rigidbodyTarget.position, _factorLenghtToDelete);
            element.TrySpawnNeighbour(_rigidbodyTarget.position, _factorLenghtToSpawn);
        }
    }

    public void AddElement(ParallaxElement element)
    {
        StartCoroutine(ActionWithElements(element, ActionType.Add));
    }

    public void RemoveElement(ParallaxElement element)
    {
        StartCoroutine(ActionWithElements(element, ActionType.Remove));
    }

    private IEnumerator ActionWithElements(ParallaxElement parallaxElement, ActionType actionType)
    {
        yield return new WaitForEndOfFrame();
        if (actionType == ActionType.Add)
            _parallaxElements.Add(parallaxElement);
        else
            _parallaxElements.Remove(parallaxElement);
    }

    private void OnDrawGizmosSelected()
    {
        DrawPointSphere(_rightPoint, 0.2f, Color.magenta);
        DrawPointSphere(_leftPoint, 0.2f, Color.white);
    }

    private void DrawPointSphere(Vector2 point, float radius, Color color)
    {
        Gizmos.color = color;
        Vector2 pointShere = transform.position;
        pointShere.x += point.x;
        Gizmos.DrawWireSphere(pointShere, radius);
    }

    enum ActionType
    {
        Add, Remove
    }
}
