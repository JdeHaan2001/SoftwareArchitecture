using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    private Canvas upgradeBtnCanvas = null;
    private BuildingTile tile = null;
    private GameObject towerToBuy = null;

    private WaveManager waveManager = null;

    private bool isBuyingTower = false;

    private void Start()
    {
        waveManager = GetComponent<WaveManager>();

        if (towerParentTransform == null)
            Debug.LogError("towerParentTransform is NULL", this);
        if (waveManager == null)
            Debug.LogError("waveManager is NULL", this);

        waveManager.OnWaveStart += hideButtonCanvas;
    }

    private void OnEnable()
    {
        if(waveManager != null)
            waveManager.OnWaveStart += hideButtonCanvas;
    }

    private void OnDisable()
    {
        if (waveManager != null)
            waveManager.OnWaveStart -= hideButtonCanvas;
    }

    private void Update()
    {
        handleRayCasting();
    }

    private void handleRayCasting()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (tile != null)
        {
            tile.ChangeColor(this.defaultColor);
            tile = null;
        }

        if (upgradeBtnCanvas != null)
        {
            if (Input.GetMouseButtonDown(1) && upgradeBtnCanvas.gameObject.activeSelf)
                upgradeBtnCanvas.gameObject.SetActive(false);
        }

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (!waveManager.IsWaveActive)
            {
                if (hit.transform.tag == "Tower")
                    handleSetButtonCanvasActive(hit);
                else if (hit.transform.tag == "BuildTile")
                    handleTileColorChange(hit);
            }
        }
    }

    private void handleSetButtonCanvasActive(RaycastHit pHit)
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Makes sure that there's only one upgrade/sell menu open and not multiple at the same time
            if (upgradeBtnCanvas != null)
                upgradeBtnCanvas.gameObject.SetActive(false);

            upgradeBtnCanvas = pHit.transform.GetComponent<Tower>().GetButtonPanel();
            upgradeBtnCanvas.gameObject.SetActive(true);
        }
    }

    private void hideButtonCanvas()
    {
        if (upgradeBtnCanvas != null)
            upgradeBtnCanvas.gameObject.SetActive(false);
    }

    private void handleTileColorChange(RaycastHit pHit)
    {
        BuildingTile tempTile = pHit.transform.GetComponent<BuildingTile>();

        if (!isBuyingTower) return;
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

    private void putTowerOnTile()
    {
        if (towerToBuy == null) return;

        Instantiate(towerToBuy, tile.transform.position + new Vector3(0, 1, 0), Quaternion.identity, towerParentTransform);
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
