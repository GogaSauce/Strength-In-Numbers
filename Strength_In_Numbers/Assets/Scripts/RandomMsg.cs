using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RandomMsg : MonoBehaviour
{
    [SerializeField] string[] randmsg;

    private void Start()
    {
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
        int rand = Random.Range(0, randmsg.Length);
        tmp.text = randmsg[rand];
    }
}
