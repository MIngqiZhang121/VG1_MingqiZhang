using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Rigidbody2D _rb;

    float randomSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        randomSpeed = Random.Range(0.4f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = Vector2.left * randomSpeed;
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
