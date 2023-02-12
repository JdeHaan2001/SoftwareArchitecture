using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    public void SetObjectActive(bool pIsActive) => this.gameObject.SetActive(pIsActive);
}
