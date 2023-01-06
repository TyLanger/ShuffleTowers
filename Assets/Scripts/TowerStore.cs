using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStore : MonoBehaviour
{

    [SerializeField] Tower towerPrefab;
    [SerializeField] Tower goldTowerPrefab;

    public void BuildTower()
    {
        TrySpawnTower(towerPrefab);
    }

    public void BuildGoldTower()
    {
        TrySpawnTower(goldTowerPrefab);
    }

    void TrySpawnTower(Tower tower)
    {
        if (GridBuilder.Instance.CanBuildOnSelection())
        {
            var tile = GridBuilder.Instance.GetSelectedTile();
            var copy = Instantiate(tower, tile.position, Quaternion.identity);
            copy.transform.parent = tile;

            GridBuilder.Instance.BuiltOnSelection();
        }
    }
}
