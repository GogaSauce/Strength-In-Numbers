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
    public bool inStompRange;
    public float stompRange;
    public LayerMask enemy;
    float castleRange;
    Transform castle;
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
        castle = GameObject.FindWithTag("castle").transform;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        castleRange = Vector3.Distance(transform.position, castle.position);

       
        inStompRange = Physics.CheckSphere(transform.position, stompRange, enemy);
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
            
        }
        if (Input.GetKeyDown(KeyCode.E) && castleRange <= 4.9f)
        {
            isAttacking = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && inStompRange)
        {
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stompRange);
    }

}
