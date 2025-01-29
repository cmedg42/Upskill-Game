using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour {
    private InputAction moveAction;
    private InputAction lightAttack;
    private InputAction specialAtk1;
    private InputAction specialAtk2;
    private InputAction specialAtk3;

    public LayerMask enemyLayers;
    [SerializeField]private Transform lightAtkPt;
    public float lightAtkRng = 0.5f;

    public bool canMove = true;
    public bool canAttack = true;
    private bool facingRight = true;

    public Animator animator;

    //private float ud;
    //private float lr;
    private Vector2 dir;
    [SerializeField] float spd;

    Rigidbody2D rigidbody2d;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();

        moveAction = InputSystem.actions.FindAction("Move");
        lightAttack = InputSystem.actions.FindAction("LightAttack");
        specialAtk1 = InputSystem.actions.FindAction("Special1");
        specialAtk2 = InputSystem.actions.FindAction("Special2");
        specialAtk3 = InputSystem.actions.FindAction("Special3");
        dir = Vector2.zero;
    }

    private void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + dir * spd * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }


    // Update is called once per frame
    void Update()
    {
        //ud = Input.GetAxisRaw("Vertical");
        //lr = Input.GetAxisRaw("Horizontal");
        //dir = new Vector2(lr, ud);
        //movement
        dir = moveAction.ReadValue<Vector2>();
        if (canMove && dir.magnitude >= 0.04)
        {
            dir.Normalize();
        }
        else
        {
            dir = Vector2.zero;
        }

        //fighting
        if (!canAttack) { return; }
        if (lightAttack.WasPressedThisFrame())
        {
            LightAttack();
            animator.SetBool("isAttacking", true);
        }
        else if (specialAtk1.WasPressedThisFrame())
        {

        }
        else if (specialAtk2.WasPressedThisFrame())
        {

        }
        else if (specialAtk3.WasPressedThisFrame())
        {

        }

        // flips player
        if (dir.x < 0 && facingRight || dir.x > 0 && !facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
        }

        // determines when idling and when walking
        animator.SetFloat("Speed", Mathf.Abs(dir.magnitude));
    }

    void LightAttack() {
        //implement light attack here. set animation? fire event? Idk how
        Debug.Log("LightAttack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(lightAtkPt.position, lightAtkRng, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit enemy: " + enemy.name);
            enemy.GetComponent<Health>().ChangeHealth(5);
        }
    }

    public void OnAttackFinished()
    {
        animator.SetBool("isAttacking", false);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(lightAtkPt.position, lightAtkRng);
    }
}
