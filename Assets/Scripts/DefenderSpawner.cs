using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    Defender defender;
    GameObject defenderParent;
    const string DEFENDER_PARENT_GAME = "Defenders";

    private void Start()
    {
        CreateDefenderParent();
    }

    private void CreateDefenderParent()
    {
        defenderParent = GameObject.Find(DEFENDER_PARENT_GAME);

        if (!defenderParent)
        {
            defenderParent = new GameObject(DEFENDER_PARENT_GAME);
        }
    }

    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAT(GetSquareClicked());
    }

    public void SEtSelectedDefended(Defender defenderToSelect)
    {
        defender = defenderToSelect;
    }

    private void AttemptToPlaceDefenderAT(Vector2 gridPos)
    {
        var StarDisplay = FindObjectOfType<StarDisplay>();
        int defenderCost = defender.GetStarCost();

        if (StarDisplay.HaveEnoughStars(defenderCost))
        {
            SpawnDefender(gridPos);
            StarDisplay.SpendStars(defenderCost);
        }
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector2 gridPos = SnapToGrid(worldPos);

        return gridPos;
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);

        return new Vector2(newX, newY); ;
    }

    private void SpawnDefender(Vector2 roundedPos)
    {
        Defender newDefender = Instantiate(defender, roundedPos, transform.rotation) as Defender;
        newDefender.transform.parent = defenderParent.transform;
    }
}
