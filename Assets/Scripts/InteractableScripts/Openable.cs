using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
/** Openable is a Interaction (i.e. inherits from Interactable). */
public class Openable : Interactable
{
    [SerializeField]
    private Sprite open;
    [SerializeField]
    private Sprite closed;

    private SpriteRenderer sr;
    private bool isOpen;

    public override void Interact()
    {
        if (this.isOpen)
            this.sr.sprite = this.closed;
        else
            this.sr.sprite = this.open;
        this.isOpen = !this.isOpen;

        if (GetComponent<PanelOpener>() != null)
            GetComponent<PanelOpener>().OpenPanel();
    }

    private void Start()
    {
        this.sr = GetComponent<SpriteRenderer>();
        this.sr.sprite = this.closed;
    }
}