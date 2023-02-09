using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent _onUse;
    [SerializeField] private Sprite _spriteUsed;
    [SerializeField] private GameObject _hint;
    private bool _alreadyUsed = false;
    SpriteRenderer _spriteRenderer;

    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Use()
    {
        _onUse.Invoke();
        _spriteRenderer.sprite = _spriteUsed;
        HideHint();
        _alreadyUsed = true;
    }

    public void ShowHint()
    {
        
       if (!_alreadyUsed) 
       {
            _hint.SetActive(true);
       }
    }

    public void HideHint()
    {
        _hint.SetActive(false);
    }
}
