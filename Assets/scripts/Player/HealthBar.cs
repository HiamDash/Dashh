using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public float lives;

    void Start()
    {
        lives = 100f;
    }

    
    void Update()
    {
        lives -= Time.deltaTime * 1f;
        bar.fillAmount = lives;
    }
}
