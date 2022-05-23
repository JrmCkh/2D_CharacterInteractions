using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private InventorySlot inventorySlot;

    /** Awake is called only once when object is initialized. */
    public void Awake()
    {
        this.canvas = transform.parent.GetComponent<Canvas>();
        this.inventorySlot = GetComponentInChildren<InventorySlot>();
    }

    /** Set the data of the dragged inventory slot. */
    public void SetData(InventoryItem item)
    {
        this.inventorySlot.DrawSlot(item);
    }

    /** Update is called once per frame */
    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform) this.canvas.transform,
            Input.mousePosition,
            this.canvas.worldCamera, 
            out position
                );
        transform.position = this.canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        gameObject.SetActive(val);
    }
}