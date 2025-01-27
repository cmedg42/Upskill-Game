using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour {
    private InputAction moveAction;
    private InputAction lightAttack;
    private InputAction specialAtk1;
    private InputAction specialAtk2;
    private InputAction specialAtk3;

    public bool canMove = true;
    public bool canAttack = true;

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
    void Update() {
        //ud = Input.GetAxisRaw("Vertical");
        //lr = Input.GetAxisRaw("Horizontal");
        //dir = new Vector2(lr, ud);
        //movement
        dir = moveAction.ReadValue<Vector2>();
        if (canMove && dir.magnitude >= 0.04) {
            dir.Normalize();
        } else
        {
            dir = Vector2.zero;
        }
        
        //fighting
        if (!canAttack) { return; }
        if (lightAttack.IsPressed()){
            LightAttack();
        }else if (specialAtk1.IsPressed()){

        } else if (specialAtk2.IsPressed()){

        } else if (specialAtk3.IsPressed()){

        }
    }

    void LightAttack() {
        //implement light attack here. set animation? fire event? Idk how
    }
}
