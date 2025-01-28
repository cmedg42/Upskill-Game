using UnityEngine;

public class Health : MonoBehaviour
{

    public float health = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHealth(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Gibbed");
            Destroy(this.gameObject);
        }
    }
}
