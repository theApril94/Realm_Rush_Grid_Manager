using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [SerializeField] Tower towerPrefab;
    public bool IsPlacable { get { return isPlaceable; } }


    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if(!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        } 
            
    }

    void OnMouseDown()
    { 
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBLockPath(coordinates))
        {
            bool isSuccsusful = towerPrefab.CreateTower(towerPrefab, transform.position);
            if(isSuccsusful)
            {
                gridManager.BlockNode(coordinates);

                pathfinder.NotifiRecievers();
            }
            
        }
    }
}
