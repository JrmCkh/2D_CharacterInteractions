using UnityEngine;

/** Animatable is a Interaction (I.e. inherits from Interactable). */
public class Animatable : Interactable
{
    private Animator animator;
    private bool start;

    private void Awake()
    {
        this.animator = gameObject.GetComponent<Animator>();
    }

    public override void Interact()
    {
        this.animator.SetBool("startAnimation", this.start);
        this.start = !this.start;
    }
}