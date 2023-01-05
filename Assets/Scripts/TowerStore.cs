using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStore : MonoBehaviour
{

    [SerializeField] Tower towerPrefab;

    public void BuildTower()
    {
        if(GridBuilder.Instance.CanBuildOnSelection())
        {
            var tile = GridBuilder.Instance.GetSelectedTile();
            var copy = Instantiate(towerPrefab, tile.position, Quaternion.identity);
            copy.transform.parent = tile;

            GridBuilder.Instance.BuiltOnSelection();
        }
    }
}
