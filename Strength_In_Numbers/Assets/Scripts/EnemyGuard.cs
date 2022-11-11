using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGuard : MonoBehaviour
{
    Player playerScript;
    public Health enemyHealth;
    public float currentHP;
    [SerializeField]
    float maxHP;
    public NavMeshAgent agent;
    public Transform player;
    public float attackRange;
    bool inAttackRange;
    public LayerMask playerMask;
    public float attackTime;
    bool alreadyAttacked;
    public Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
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
        if (player != null)
        {
            transform.LookAt(new Vector3(player.position.x, player.position.y - 1f, player.position.z)) ;
        }
        if (currentHP <= 0f)
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
            
            Invoke(nameof(Attack), attackTime);
            
        }
        if (playerScript.isAttacking && inAttackRange)
        {
            
            TakeDamage(playerScript.playerDmg);
            
        }
    }
    void Attack()
    {
        alreadyAttacked = false;
        anim.Play("attack");
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

   

    void TakeDamage(float damage){
        currentHP -= damage;
        enemyHealth.SetHealth(currentHP);

    }

}
