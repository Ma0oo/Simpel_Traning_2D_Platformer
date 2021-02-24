﻿using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class ParallaxEffector : MonoBehaviour
{
    [SerializeField] private Vector2 _leftPoint;
    [SerializeField] private Vector2 _rightPoint;
    [SerializeField] private Rigidbody2D _rigidbodyPlayer;
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
            element.Move(_rigidbodyPlayer.velocity);
            element.TryDelete(_rigidbodyPlayer.position, _factorLenghtToDelete);
            element.TrySpawnNeighbour(_rigidbodyPlayer.position, _factorLenghtToSpawn);
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
        Gizmos.color = Color.magenta;
        Vector2 right = transform.position;
        right.x += _rightPoint.x;
        Gizmos.DrawWireSphere(right, 0.2f);

        Gizmos.color = Color.white;
        Vector2 left = transform.position;
        left.x += _leftPoint.x;
        Gizmos.DrawWireSphere(left, 0.2f);
    }

    enum ActionType
    {
        Add, Remove
    }
}
