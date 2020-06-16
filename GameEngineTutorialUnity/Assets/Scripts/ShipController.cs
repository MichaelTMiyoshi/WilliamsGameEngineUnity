using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    int maxHealth;
    int currentHealth;
    public int health { get { return currentHealth; } }
    Rigidbody2D rigidbody2D;
    [SerializeField] private GameObject laserPrefab;
    //AudioSource audioSource;
    //public AudioClip pew;
    [SerializeField] private float laserForce;
    public Vector2 moveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = GameManager.instance.maxHealth;
        //audioSource = GetComponent<AudioSource>();
        laserForce = 200.0f;
        maxHealth = GameManager.instance.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // even after using Approximately, there is a small amount of drift
        // in the x-direction.
        if (Mathf.Approximately(horizontal, 0.0f)) { horizontal = 0.0f; }
        if (Mathf.Approximately(vertical, 0.0f)) { vertical = 0.0f; }
        Vector2 move = new Vector2(horizontal, vertical);
        Vector2 position = rigidbody2D.position;
        moveVelocity = move * speed;
        position += move * speed * Time.deltaTime;
        //position += moveVelocity * Time.deltaTime;

        rigidbody2D.MovePosition(position);

        bool firing;
        if (Input.GetAxisRaw("Fire1") == 0) { firing = false; }
        else { firing = true; }

        if (Input.GetKeyDown(KeyCode.Space) || firing)
        {
            Launch();
        }
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log("Health: " + currentHealth);
    }

    void Launch()
    {
        GameObject laserObject = Instantiate(laserPrefab, rigidbody2D.position + Vector2.right * 0.5f, Quaternion.identity);
        LaserController laser = laserObject.GetComponent<LaserController>();
        laser.Launch(Vector2.right, laserForce);
        //PlaySound(launch);
    }

    //public void PlaySound(AudioClip clip)
    //{
        //audioSource.PlayOneShot(clip);
    //}
}
