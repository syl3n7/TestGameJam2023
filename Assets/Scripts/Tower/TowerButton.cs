using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerButton : MonoBehaviour
{

    GridManager gridManager;
    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button towerButton = root.Q<Button>("Tower");

        towerButton.clicked += () => gridManager.OnMouseClickOnUI();
    }
}
