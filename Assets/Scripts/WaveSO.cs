using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave", order = 3)]
public class WaveSO : ScriptableObject
{
    [Tooltip("Amount of seconds for when the next enemy spawns")]
    [Range(0.5f, 5f)] public float EnemyDelay = 2f;
    [Tooltip("Maximum amount of enemies in a wave")]
    [Range(1, 50)] public int MaxEnemies = 10;
    [Tooltip("Types of enemies that will be spawned in this wave")]
    public List<EnemySO> enemies = new List<EnemySO>();
}
