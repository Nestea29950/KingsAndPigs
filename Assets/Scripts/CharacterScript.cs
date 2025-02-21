using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{   
    public GameObject[] Hearts; // Tableau des cœurs
    private int currentHealth; // Nombre de vies restantes
    private bool isInvincible = false; // Pour éviter les collisions multiples
    private float invincibilityTime = 1.5f; // Temps d'invincibilité après un coup

    private Animator animator;
    private float moveSpeed = 2f;
    private float jumpForce = 300f;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isGrounded;
    private SpriteRenderer spriteRenderer; // Pour faire clignoter le perso

    public Transform groundedLeft;
    public Transform groundedRight;

    [SerializeField] private InputActionReference movementAction;
    [SerializeField] private InputActionReference jumpAction;   

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = Hearts.Length; // Initialise la vie au nombre de cœurs
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapArea(groundedLeft.position, groundedRight.position);
        float horizontalInput = movementAction.action.ReadValue<Vector2>().x * moveSpeed;

        MovePlayer(horizontalInput);
        Flip(rb.linearVelocityX);
        if (jumpAction.action.WasPressedThisFrame() && isGrounded)
        {
            isJumping = true;
        }
    }

    void MovePlayer(float _horizontalInput)
    {
        Vector3 targetVelocity = new Vector2(_horizontalInput, rb.linearVelocityY);
        rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref velocity, .05f);
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocityX));

        if (isJumping && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    void Flip(float velocity)
    {
        if (velocity > 1)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (velocity < -1)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            Debug.Log("Collision avec : " + other.gameObject.name); // Affiche le nom de l’objet touché

        if (other.CompareTag("Enemy") && !isInvincible) // Vérifie si pas déjà invincible
        {
            StartCoroutine(GetDamage());
        }
    }

    private IEnumerator GetDamage()
    {
        isInvincible = true; // Active l'invincibilité

        if (currentHealth > 1)
        {
            currentHealth--; // Réduit la vie
            Hearts[currentHealth].SetActive(false); // Désactive un cœur
            StartCoroutine(Blink()); 
        }
        else
        {
            Destroy(gameObject); // Détruit le personnage si plus de vie
        }

        yield return new WaitForSeconds(invincibilityTime); // Attente avant la fin d'invincibilité
        isInvincible = false;
    }

    private IEnumerator Blink()
    {
        for (int i = 0; i < 5; i++) // Clignote 5 fois
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
