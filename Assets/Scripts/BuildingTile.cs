using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is a class that holds all the necessary information for a building tile.
/// </summary>
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

    /// <summary>
    /// Changes the color of the tile
    /// </summary>
    /// <param name="pColor">Color that the tile will change to</param>
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
