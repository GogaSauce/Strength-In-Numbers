using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider slider;
    float currentHealth;
    float maxHealth;
    // Start is called before the first frame update
    public void SetHealth(float health)
    {
        currentHealth = health;
        slider.value = currentHealth;
       
    }
    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        slider.maxValue = maxHealth;
    }

    
}
