using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemySO : ScriptableObject
{
    public float Health = 10f;
    public float Speed = 5f;
    public int Money = 5;
    public int Damage = 5;
    public GameObject enemy;

    public void Spawn(Vector3 pPosition, Quaternion pRotation)
    {
        Instantiate(enemy).GetComponent<Enemy>().SetData(this);
    }
}
