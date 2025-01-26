using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour {
    private float ud;
    private float lr;
    private Vector2 dir;
    [SerializeField] float spd;

    Rigidbody2D rigidbody2d;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        ud = Input.GetAxisRaw("Vertical");
        lr = Input.GetAxisRaw("Horizontal");
        dir = new Vector2(lr, ud);

        if (dir.magnitude >= 0.04) {
            dir.Normalize();
        }
    }

    private void FixedUpdate() {
        Vector2 position = (Vector2)rigidbody2d.position + dir * spd * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }
}
