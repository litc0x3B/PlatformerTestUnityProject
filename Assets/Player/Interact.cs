using UnityEngine;

public class Interact : MonoBehaviour
{
    private IInteractable _interactable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            _interactable = collision.GetComponent<IInteractable>();
            _interactable.ShowHint();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            _interactable?.HideHint();
            _interactable = null;
        }
    }

    private void Update()
    {

        if (Input.GetButtonDown("Use"))
        {
            _interactable?.Use();
        }
    }
}
