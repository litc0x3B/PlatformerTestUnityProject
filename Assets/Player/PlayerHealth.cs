using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IHealth
{
    private float _hp;
    [SerializeField, Min(0)] private float _maxHp;
    private UIBar _bar;


    public float MaxHp { get { return _maxHp; } }

    private void Start()
    {
        _hp = _maxHp;
        _bar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<UIBar>();
        _bar.Init(0, MaxHp, _hp);
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
        {
            Debug.LogWarning("Attempt to deal negative damage");
            return;
        }

        _hp -= damage;
        if (_hp <= 0)
        {
            _hp = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        _bar.SetValue(_hp);
    }

    public void Heal(float hp)
    {
        if (hp < 0)
        {
            Debug.LogWarning("Attempt to heal negative amount of health");
            return;
        }

        _hp += hp;

        if (hp > MaxHp)
        {
            hp = MaxHp;
        }
        _bar.SetValue(_hp);
    }
}
