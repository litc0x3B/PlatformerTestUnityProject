using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   
    [SerializeField] private Transform _target;
    [SerializeField] private float _maxDistance = 5.0f;
    [SerializeField] private float _speed = 1.0f;

    void Start()
    {
        Debug.Log(gameObject.name);
    }

    void Update()
    {
        if (((Vector2)transform.position - (Vector2)_target.position).sqrMagnitude > Mathf.Pow(_maxDistance, 2))
        {
            Vector3 newPos = Vector2.Lerp(transform.position, _target.position, _speed * Time.deltaTime);
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        }
    }
}
