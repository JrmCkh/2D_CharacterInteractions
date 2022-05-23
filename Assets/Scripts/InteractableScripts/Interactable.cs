using UnityEngine;

[RequireComponent(typeof(Collider2D))]
/** Abstract class Interactable that should be inherited all interactable objects. */
public abstract class Interactable : MonoBehaviour
{
    private void Reset()
    {
        gameObject.GetComponent<Collider2D>().isTrigger = true;
    }

    public abstract void Interact();
}