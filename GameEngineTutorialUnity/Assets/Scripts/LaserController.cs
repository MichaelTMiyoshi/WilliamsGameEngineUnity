using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2D.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //MeteorController
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.screenBounds.x < rigidbody2D.position.x)
        {
            Destroy(gameObject);
        }
    }
}
