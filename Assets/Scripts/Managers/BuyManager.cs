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

    private BuildingTile tile = null;

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
            Debug.Log("Changed color back to default");
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.tag != "BuildTile") return;

            BuildingTile tempTile = hit.transform.GetComponent<BuildingTile>();

            if (tempTile == null) return;

            if (tempTile.GetHasTowerOnTile())
                tempTile.ChangeColor(this.unAvailableColor);
            else
                tempTile.ChangeColor(this.availableColor);

            tile = tempTile;
            Debug.Log("Changed color");
        }
    }
}
