using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utility class to hide an object. Mainly used in UI for a button
/// </summary>
public class HideObject : MonoBehaviour
{
    public void SetObjectActive(bool pIsActive) => this.gameObject.SetActive(pIsActive);
}
