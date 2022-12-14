using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRock : MonoBehaviour
{

    public bool equipped = false;
    public GameObject rock;
    bool isTouchingGround;
    [SerializeField] float radius;
    public LayerMask ground;
    List<GameObject> objs = new List<GameObject>();
    GameObject obj;

    private void Update()
    {
        isTouchingGround = Physics.CheckSphere(transform.position, radius, ground);

        if (isTouchingGround && !equipped)
        {
            Debug.Log("instantiate");
            obj = Instantiate(rock, transform.position, Quaternion.identity);
            objs.Add(obj);           
            equipped = true;
            

        }
        if (obj == null && equipped)
        {
            equipped = false;
        }
        if (objs.Count > 1)
        {
            int randindex = Random.Range(0, objs.Count);
            objs.RemoveAt(randindex);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
