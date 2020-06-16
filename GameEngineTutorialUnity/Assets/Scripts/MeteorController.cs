using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public GameObject ship;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (rigidbody2D.position.x < -GameManager.instance.screenBounds.x)
        {
            ShipController shipScript = ship.GetComponent<ShipController>();
            shipScript.ChangeHealth(-1);
            Destroy(gameObject);
        }
    }

    public void Spawn(Vector2 direction, float force)
    {
        rigidbody2D.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Ship")
        {
            ShipController ship = collision.gameObject.GetComponent<ShipController>();
            ship.ChangeHealth(-1);
        }
        else
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
