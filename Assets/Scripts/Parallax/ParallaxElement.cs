using UnityEngine;

public class ParallaxElement : MonoBehaviour
{
    [Range(0, 2f)] [SerializeField] private float _speedFactor;

    private ParallaxEffector _parallaxEffector;
    private ParallaxElement _leftNeighbour;
    private ParallaxElement _rightNeighbour;

    private void Awake()
    {
        Init();
    }

    public void MoveWithTarget(Vector2 velocityOfTarget)
    {
        Vector2 newPosition = transform.position;
        newPosition.x += velocityOfTarget.x * Time.deltaTime * _speedFactor;
        transform.position = newPosition;
    }

    public void TryDelete(Vector2 targetPostion, float factorLengt)
    {
        if (CheckCrossingDistanceByTarget(targetPostion, Direction.Left, factorLengt) || CheckCrossingDistanceByTarget(targetPostion, Direction.Right, factorLengt))
        {
            _parallaxEffector.RemoveElement(this);
            Destroy(gameObject);
        }
    }

    public void TrySpawnNeighbour(Vector2 targetPosition, float factorLengt)
    {
        if (CheckCrossingDistanceByTarget(targetPosition, Direction.Right, factorLengt))
            SpawnElement(ref _rightNeighbour, Direction.Left);
        else if (CheckCrossingDistanceByTarget(targetPosition, Direction.Left, factorLengt))
            SpawnElement(ref _leftNeighbour, Direction.Right);
    }

    private void SetNeighbour(Direction directionToParent, ParallaxElement parent)
    {
        if (directionToParent == Direction.Right)
            _rightNeighbour = parent;
        else
            _leftNeighbour = parent;
    }

    private void SpawnElement(ref ParallaxElement parallaxElement, Direction directionToParent)
    {
        int invertDirection = -1;
        if (parallaxElement == null)
        {
            Vector2 spawnPosition = transform.position;
            spawnPosition.x += _parallaxEffector.Width * System.Convert.ToInt32(directionToParent) * invertDirection;
            parallaxElement = Instantiate(this, spawnPosition, transform.rotation, _parallaxEffector.transform);
            parallaxElement.SetNeighbour(directionToParent, this);
            parallaxElement.gameObject.name = gameObject.name;
            _parallaxEffector.AddElement(parallaxElement);
        }
    }

    private bool CheckCrossingDistanceByTarget(Vector2 targetPosition, Direction direction, float factorLengtToBorlder)
    {
        float halfWidth = _parallaxEffector.Width / 2;
        if (direction == Direction.Left)
            return targetPosition.x < transform.position.x + halfWidth * factorLengtToBorlder * System.Convert.ToInt32(direction);
        else
            return targetPosition.x > transform.position.x + halfWidth * factorLengtToBorlder * System.Convert.ToInt32(direction);
    }

    private void Init()
    {
        _parallaxEffector = GetComponentInParent<ParallaxEffector>();
    }

    public enum Direction
    {
        Right = 1, Left = -1
    }
}