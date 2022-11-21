using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttack : MonoBehaviour
{

    
    Rigidbody rb;
    GameObject cam;
    Player player;
    CreateRock create;
    //Parent for rock
    Transform holdSpot;
    [SerializeField] LayerMask everything;
    public Transform check;
    public float rockDmg;
    public AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GameObject.FindWithTag("MainCamera");
        holdSpot = GameObject.FindWithTag("holdPoint").transform;
        transform.SetParent(holdSpot);
        player = holdSpot.GetComponentInParent<Player>();
        create = holdSpot.GetComponent<CreateRock>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        Vector3 mouseWorldPoint = Vector3.zero;
        
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, everything))
        {
            
            mouseWorldPoint = hit.point;

        }
        Vector3 aimDir = (mouseWorldPoint - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(aimDir, Vector3.up);
        if (transform.parent != null)
        {
            transform.localPosition = Vector3.zero;
        } 

        if (Input.GetMouseButtonUp(0) && create.equipped)
        {
            Invoke(nameof(Attack), 0.3f);
        }

        

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("sword") || collision.gameObject.CompareTag("archer"))
        {
            Destroy(collision.gameObject);
            audioSrc.Play();
        }
        if (collision.gameObject.CompareTag("castle"))
        {
            collision.gameObject.GetComponent<Castle>().TakeDmg(rockDmg);
            audioSrc.Play();
        }    
       
       
    }

    void Attack()
    {
        
        transform.SetParent(null);
        rb.AddForce(transform.forward * player.throwForce, ForceMode.Impulse);
        create.equipped = false;
        Destroy(gameObject, 1f);
    }
       
}
