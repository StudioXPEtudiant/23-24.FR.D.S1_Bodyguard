using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] private Slider bar;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        bar.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update() {
        bar.value = currentHealth;
    }
}
