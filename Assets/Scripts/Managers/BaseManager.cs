using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{

    [SerializeField][Range(10, 500)]
    private int baseHealth = 50;
    [SerializeField]
    private BaseHealthUI healthUI;

    public System.Action<int> OnDealDamageToBase;
    public System.Action OnLoseGame;

    /// <summary>
    /// Deals damage to the base
    /// </summary>
    public void DealDamageToBase(int pDamage)
    {
        //Making sure that baseHealth doesn't go below 0
        if ((baseHealth - pDamage) < 0)
            baseHealth = 0;
        else
            baseHealth -= pDamage;

        OnDealDamageToBase?.Invoke(baseHealth);
        Debug.Log($"Base has {baseHealth} remaining");

        if (baseHealth <= 0)
            OnLoseGame?.Invoke();
    }

    /// <summary>
    /// Returns the current health of the base
    /// </summary>
    public int GetBaseHealth() => baseHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            DealDamageToBase(other.GetComponent<Enemy>().GetDamage());

            Debug.Log($"Destroying object {other.name}");
            Destroy(other.gameObject);
        }
    }
}
