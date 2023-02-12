using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTile : MonoBehaviour
{
    private GameObject towerObj;

    private MeshRenderer mesh;

    private bool hasTowerOnTile = false;   

    public bool GetHasTowerOnTile() => hasTowerOnTile;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void ChangeColor(Color pColor)
    {
        mesh.material.color = pColor;
    }

    public void SetTowerObj(GameObject pTowerObj)
    {
        towerObj = pTowerObj;
        hasTowerOnTile = true;
    }
}
