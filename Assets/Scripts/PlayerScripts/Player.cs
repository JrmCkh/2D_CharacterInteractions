using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private const int SPEED = 5;
    private const int MAX_HEALTH = 100;

    [SerializeField]
    private HealthBar healthBar;
    private int currentHealth;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 movement;
    private Vector2 boxSize = new Vector2(0.1f, 1f);

    private PanelOpener panel;

    /**
     * Unity calls Awake only once during the lifetime of the script instance.
     * Awake is used to initialize variables or states before the application starts.
     */
    private void Awake()
    {
        this.rb = gameObject.GetComponent<Rigidbody2D>();
        this.animator = gameObject.GetComponent<Animator>();
        this.panel = gameObject.GetComponent<PanelOpener>();
    }

    /** Start us cakked before the first frame update. */
    private void Start()
    {
        this.healthBar.SetMaxHealth(MAX_HEALTH);
        this.currentHealth = MAX_HEALTH;
    }

    /**
     * FixedUpdate is run either once, zero, or serveral times per frame.
     * Used for physics-related functions such as applying forces or torques (e.g. movement).
     */
    private void FixedUpdate()
    {
        this.rb.MovePosition(this.rb.position + this.movement * SPEED * Time.fixedDeltaTime);
    }

    /** Run once per frame. */
    private void Update()
    {
        // Interact: 'E' key
        if (Input.GetKeyDown(KeyCode.E))
            CheckInteraction();

        // Open Inventory/Bag: 'B' key
        if (Input.GetKeyDown(KeyCode.B))
        {
            gameObject.GetComponentInChildren<InventoryManager>().OpenInventory();
            this.panel.OpenPanel();
        }

        // Test take damage: 'P' key
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(20);
        }
    }

    public void OnMovement(InputValue value)
    {
        this.movement = value.Get<Vector2>();
        if (this.movement.x != 0 || this.movement.y != 0)
        {
            this.animator.SetFloat("X", this.movement.x);
            this.animator.SetFloat("Y", this.movement.y);
            this.animator.SetBool("IsWalking", true);
        }
        else
        {
            this.animator.SetBool("IsWalking", false);
        }
    }

    public void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, this.boxSize, 0, Vector2.zero);
        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
    }

    private void TakeDamage(int damage)
    {
        this.currentHealth -= damage;
        this.healthBar.SetHealth(this.currentHealth);
    }
}