using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public Health healthbar;
    public float currentHp;
    public float hp;
    public Player player;
    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {   

        currentHp = hp;
        healthbar.SetMaxHealth(hp);
        healthbar.slider.value = hp;
        playerTransform = GameObject.FindWithTag("Player").transform;
        player = playerTransform.GetComponent<Player>();
        
    }

    //void Update()
    //{
    //    float dist = Vector3.Distance(transform.position, playerTransform.position);

    //    if (dist <= 4.9f && player.isAttacking)
    //    {
    //        TakeDmg(player.playerDmg);
    //    }



    //}


    public void TakeDmg(float damage)
    {
        currentHp -= damage;
        healthbar.SetHealth(currentHp);
    }

}
