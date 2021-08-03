using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderButton : MonoBehaviour
{
    [SerializeField] Defender defenderPrefab;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        LabelButtonWithCost();
    }

    private void LabelButtonWithCost()
    {
        Text costText = GetComponentInChildren<Text>();

        if (!costText)
        {
            Debug.LogError(name + "has not cost text");
        }
        else
        {
            costText.text = defenderPrefab.GetStarCost().ToString();
        }
    }

    private void OnMouseDown()
    {
        var buttons = FindObjectsOfType<DefenderButton>();

        foreach (DefenderButton button in buttons)
        {
            button.spriteRenderer.color = new Color32(80, 75, 75, 255); ;
        }

        spriteRenderer.color = Color.white;

        FindObjectOfType<DefenderSpawner>().SEtSelectedDefended(defenderPrefab);
    }
}
