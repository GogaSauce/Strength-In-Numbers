using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("tree"))
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 2.5f);
        }
    }
}
