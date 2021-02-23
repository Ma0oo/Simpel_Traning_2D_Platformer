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

    public void Move(Vector2 velocity)
    {
        Vector2 newPosition = transform.position;
        newPosition.x += velocity.x * Time.deltaTime * _speedFactor;
        transform.position = newPosition;
    }

    public void TryDelete(Vector2 playerPosition, float factorLengt)
    {
        if (CheckCrossingPlayerDistacne(playerPosition, Direction.Left, factorLengt) || CheckCrossingPlayerDistacne(playerPosition, Direction.Right, factorLengt))
        {
            _parallaxEffector.RemoveElement(this);
            Destroy(gameObject);
            Destroy(this);
        }
    }

    public void TrySpawnNeighbour(Vector2 playerPosition, float factorLengt)
    {
        if (CheckCrossingPlayerDistacne(playerPosition, Direction.Right, factorLengt))
            SpawnElement(ref _rightNeighbour, Direction.Left);
        else if (CheckCrossingPlayerDistacne(playerPosition, Direction.Left, factorLengt))
            SpawnElement(ref _leftNeighbour, Direction.Right);
    }

    public void SetNeighbour(Direction directionToParent, ParallaxElement parent)
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

    private bool CheckCrossingPlayerDistacne(Vector2 playerPosition, Direction direction, float factorLengtToBorlder)
    {
        if (direction == Direction.Left)
            return playerPosition.x < transform.position.x + _parallaxEffector.Width / 2 * factorLengtToBorlder * System.Convert.ToInt32(direction);
        else
            return playerPosition.x > transform.position.x + _parallaxEffector.Width / 2 * factorLengtToBorlder * System.Convert.ToInt32(direction);
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