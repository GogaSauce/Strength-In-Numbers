using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryArcher : MonoBehaviour
{
    Player playerScript;
    public Health health;
    public float currenthp;
    [SerializeField]
    float maxHP;
    public Transform playerTrans;
    public float attackRange;
    bool inAttackRange;
    public LayerMask playerMask;
    public float attackTime;
    bool alreadyAttacked;
    [SerializeField] GameObject arrow;
    [SerializeField] Transform shotPoint;
    [SerializeField] float forwardForce;
    Animator anim;
    float dist;
    [SerializeField] ParticleSystem pop;

    // Start is called before the first frame update
    void Start()
    {
        currenthp = maxHP;
        health.SetMaxHealth(maxHP);
        health.slider.value = maxHP;
        if (GameObject.FindWithTag("Player") != null)
        {
            playerTrans= GameObject.FindWithTag("Player").transform;
            playerScript = playerTrans.GetComponent<Player>();
        }
        anim = GetComponent<Animator>();


    }
    // Update is called once per frame
    void Update()
    {


       
        if (playerTrans!= null)
        {
            dist = Vector3.Distance(transform.position, playerTrans.position);
            transform.LookAt(playerTrans);
        }
        if (currenthp <= 0f)
        {
            Instantiate(pop, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        inAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
        
        if (inAttackRange && !alreadyAttacked)
        {
            alreadyAttacked = true;
            anim.SetTrigger("isattack");
            Invoke(nameof(Attack), attackTime);

        }
        if (playerScript.isAttacking && dist <= playerScript.stompRange + 0.4f)
        {

            TakeDamage(playerScript.playerDmg);

        }
    }
    void Attack()
    {
        alreadyAttacked = false;
        Rigidbody obj = Instantiate(arrow, shotPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        obj.transform.LookAt(playerTrans);
        obj.AddForce(obj.transform.forward * forwardForce, ForceMode.Impulse);
        //anim.Play("attack");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }



    void TakeDamage(float damage)
    {
        currenthp -= damage;
        health.SetHealth(currenthp);

    }


}
