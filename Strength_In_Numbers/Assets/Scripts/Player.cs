using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float currentHealth;
    public float maxHealth;
    public Health health;
    public Rigidbody rb;
    public float moveSpeed;
    float currentVelocity;
    public float smoothTime = 0.1f;
    public Transform cam;
    [SerializeField]
    float swordDmg;
    public bool isAttacking;
    public float playerDmg;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        health.SetMaxHealth(maxHealth);
        health.slider.value = maxHealth;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(horizontal, 0f, vert).normalized;
        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z)* Mathf.Rad2Deg + cam.eulerAngles.y;

            float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.Play("playerAttack");
            isAttacking = true;
        }
        else if (!Input.GetKey(KeyCode.E))
        {
            isAttacking = false;
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("sword"))
        {
            TakeDamage(swordDmg);
        }

    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        health.SetHealth(currentHealth);
    }

    
}
