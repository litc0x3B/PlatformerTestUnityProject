using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    enum TriggerType { onTriggerEnter, onTriggerStay, onCollisionEnter, onCollisionStay }

    [SerializeField] private string _dealDamageToTag;
    [SerializeField] private TriggerType _triggerOn;
    [SerializeField] private float Damage;

    private void DealDamage(GameObject gameObject)
    {
        if (gameObject.tag == _dealDamageToTag)
        {
            gameObject.GetComponentInParent<IHealth>().TakeDamage(Damage);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_triggerOn == TriggerType.onTriggerStay)
        {
            DealDamage(other.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_triggerOn == TriggerType.onTriggerEnter)
        {
            DealDamage(other.gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_triggerOn == TriggerType.onCollisionEnter)
        {
            DealDamage(collision.gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_triggerOn == TriggerType.onCollisionEnter)
        {
            DealDamage(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_triggerOn == TriggerType.onCollisionStay)
        {
            DealDamage(collision.gameObject);
        }
    }
}
