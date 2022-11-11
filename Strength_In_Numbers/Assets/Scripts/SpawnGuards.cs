using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGuards : MonoBehaviour
{

    public GameObject[] guard;
    [SerializeField] float time = 5f;
    [SerializeField] float repeat = 7f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", time, repeat);
    }

    void Spawn()
    {
        int index = Random.Range(0, guard.Length);
        Instantiate(guard[index], transform.position, transform.rotation);
    }
}
