using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the logic for the healthbar of the enemies.
/// </summary>
public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;
    private Slider healthBar;

    private void Start()
    {
        healthBar = GetComponent<Slider>();

        healthBar.maxValue = enemy.GetHealth();
        healthBar.value = healthBar.maxValue;
    }

    private void OnEnable()
    {
        enemy.OnEnemyDamage += updateHealthBar;
    }

    private void OnDisable()
    {
        enemy.OnEnemyDamage -= updateHealthBar;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform, Vector3.up);
    }

    private void updateHealthBar(int pDamage)
    {
        healthBar.value -= pDamage;
    }
}
