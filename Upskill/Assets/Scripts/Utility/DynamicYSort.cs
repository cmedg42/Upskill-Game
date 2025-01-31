using UnityEngine;

public class DynamicYSort : MonoBehaviour
{

    private Transform loc;
    private SpriteRenderer sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loc = this.gameObject.transform;
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float y = loc.position.y;
        sprite.sortingOrder = (int)(y * -10);
    }
}
