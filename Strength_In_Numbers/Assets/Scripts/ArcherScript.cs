using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherScript : MonoBehaviour
{
    Player playerScript;
    public Health enemyHealth;
    public float hp;
    [SerializeField]
    float maxHP;
    public NavMeshAgent agent;
    public Transform player;
    public float attackRange;
    bool inAttackRange;
    public LayerMask playerMask;
    public float attackTime;
    bool alreadyAttacked;
    [SerializeField] GameObject arrow;
    [SerializeField] Transform shotPoint;
    [SerializeField] float forwardForce;
    //public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
        enemyHealth.SetMaxHealth(maxHP);
        enemyHealth.slider.value = maxHP;
        if (GameObject.FindWithTag("Player") != null)
        {
            player = GameObject.FindWithTag("Player").transform;
            playerScript = player.GetComponent<Player>();
        }



    }
    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        
        if (player != null)
        {
            transform.LookAt(new Vector3(player.position.x, player.position.y - 1f, player.position.z));
        }
        if (hp <= 0f)
        {
            Destroy(gameObject);
        }

        inAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
        if (!inAttackRange)
        {

            Chase();
        }
        else if (inAttackRange && !alreadyAttacked)
        {
            alreadyAttacked = true;
            agent.SetDestination(transform.position);
            Invoke(nameof(Attack), attackTime);

        }
        if (playerScript.isAttacking &&  dist <= playerScript.stompRange + 0.4f)
        {

            TakeDamage(playerScript.playerDmg);

        }
    }
    void Attack()
    {
        alreadyAttacked = false;
        Rigidbody obj = Instantiate(arrow, shotPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        obj.transform.LookAt(player);
        obj.AddForce(obj.transform.forward * forwardForce, ForceMode.Impulse);
        //anim.Play("attack");
    }
    void Chase()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }



    void TakeDamage(float damage)
    {
        hp -= damage;
        enemyHealth.SetHealth(hp);

    }

}
