using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisions : MonoBehaviour
{
    [SerializeField] private LayerMask _collideWithLayers;

    public LayerMask CollideWithLayers { get { return _collideWithLayers; } }


    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer)) & _collideWithLayers.value) == 0)
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
    }
}
