using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    [SerializeField]
    private Color defaultColor = new Color(0, 113, 0);
    [SerializeField]
    private Color availableColor = new Color(0, 200, 0);
    [SerializeField]
    private Color unAvailableColor = new Color(0, 50, 0);
    [Space()]
    [SerializeField]
    private Transform towerParentTransform = null;

    private BuildingTile tile = null;

    private GameObject towerToBuy = null;

    private bool isBuyingTower = false;

    private void Start()
    {
        if (towerParentTransform == null)
            Debug.LogError("towerParentTransform is NULL", this);
    }

    private void Update()
    {
        handleTileColorChange();
    }

    private void handleTileColorChange()
    {
        if (tile != null)
        {
            tile.ChangeColor(this.defaultColor);
            tile = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (!isBuyingTower) return;
            if (hit.transform.tag != "BuildTile") return;

            BuildingTile tempTile = hit.transform.GetComponent<BuildingTile>();

            if (tempTile == null) return;

            if (tempTile.GetHasTowerOnTile())
                tempTile.ChangeColor(this.unAvailableColor);
            else
                tempTile.ChangeColor(this.availableColor);

            tile = tempTile;

            if (Input.GetMouseButtonDown(0))
                putTowerOnTile();
            else if (Input.GetMouseButtonDown(1)) //Cancels buying the tower
                isBuyingTower = false;
        }
    }

    private void putTowerOnTile()
    {
        if (towerToBuy == null) return;

        Instantiate(towerToBuy, tile.transform.position, Quaternion.identity, towerParentTransform);
        tile.SetTowerObj(towerToBuy);
        MoneyManager.Instance.RemoveMoney(towerToBuy.GetComponent<Tower>().BuyCost);
        isBuyingTower = false;
    }

    public void HandleTowerBuy(GameObject pTower)
    {
        isBuyingTower = true;
        towerToBuy = pTower;
    }
}
