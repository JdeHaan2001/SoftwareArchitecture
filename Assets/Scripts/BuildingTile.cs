using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTile : MonoBehaviour
{
    private Color defaultColor = new Color(15, 113, 0);
    private Color availableColor = new Color(15, 200, 0);
    private Color unAvailableColor = new Color(15, 50, 0);

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
