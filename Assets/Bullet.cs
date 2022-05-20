using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int speed = 50000;
    public Vector2 direction;

    private float lifetime = 10f;
    private float age = 0f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(direction.normalized * speed);
    }

    // Update is called once per frame
    void Update()
    {
        // check age
        age += Time.deltaTime;
        if(age >= lifetime)
        {
            Destroy(gameObject);
        }
        // move bullet
    }
}
