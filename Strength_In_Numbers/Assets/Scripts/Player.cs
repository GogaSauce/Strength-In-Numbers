using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
    [SerializeField] float arrowDmg;
    public bool isAttacking;
    public float playerDmg;
    public Animator anim;
    public bool inStompRange;
    public float stompRange;
    public LayerMask enemy;
    float castleRange;
    Transform castle;
    public float throwForce;
    bool isEquipped;
    public Canvas aimPoint;
    public CameraStyle style;
    public Transform combatLookAt;
    public CinemachineFreeLook basic;
    public CinemachineFreeLook combat;
    public enum CameraStyle
    {
        Basic,
        Combat
    }
    // Start is called before the first frame update
    void Start()
    {
        style = CameraStyle.Basic;
        currentHealth = maxHealth;
        health.SetMaxHealth(maxHealth);
        health.slider.value = maxHealth;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        rb = GetComponent<Rigidbody>();
        castle = GameObject.FindWithTag("castle").transform;
        
    }

    
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
            anim.SetFloat("speed", Mathf.Abs(dir.magnitude));
            if (style == CameraStyle.Basic)
            {

                basic.gameObject.SetActive(true);
                combat.gameObject.SetActive(false);
                float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

                float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
                transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                transform.position += moveDir * moveSpeed * Time.deltaTime;
            }
            else if (style == CameraStyle.Combat)
            {
                basic.gameObject.SetActive(false);
                combat.gameObject.SetActive(true);
                float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

                float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
                transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                transform.position += moveDir * moveSpeed * Time.deltaTime;
            }

        }
        else
        {
            anim.SetFloat("speed", 0f);
        }
        
        
        
        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("attack");
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("pickRock");

        }

        if (Input.GetMouseButton(1))
        {
            style = CameraStyle.Combat;
            aimPoint.gameObject.SetActive(true);

        }
        else
        {
            style = CameraStyle.Basic;
            aimPoint.gameObject.SetActive(false);

        }
        if (Input.GetMouseButtonUp(0) && GetComponentInChildren<CreateRock>().equipped)
        {
            anim.SetTrigger("throwRock");
            Debug.Log("setTriggerRock");

        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("sword"))
        {
            TakeDamage(swordDmg);
        }
        if (other.gameObject.CompareTag("arrow"))
        {
            TakeDamage(arrowDmg);
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
