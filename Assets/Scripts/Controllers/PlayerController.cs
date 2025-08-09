using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
/// Manages Player input and movement
/// Inherits its path AI finding from MoveableObject from InterfaceAI
public class PlayerController : MoveableObject, InterfaceAI
{
    public static event Action isPlayerStationary;

    public bool isMoving { get; private set; }

    [SerializeField] private GameObject enemyManager;

    private void Awake()
    {
        LoadData();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TriggerPlayerMovement();
        }
    }

    void TriggerPlayerMovement()
    {
        if (enemyManager == null)
        {
            Debug.LogError("enemyManager is not assigned in the inspector!");
            return;
        }

        EnemyAI enemyAI = enemyManager.GetComponent<EnemyAI>();
        if (enemyAI == null)
        {
            Debug.LogError("EnemyAI script not found on enemyManager!");
            return;
        }

        if (enemyAI.isMoving)
        {
            Debug.Log("Enemy is moving, wait...");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hoveredObject = hit.collider.gameObject;

            if (hoveredObject != null)
            {
                Vector3 clickedPosition = hoveredObject.transform.position;
                Debug.Log($"Clicked on position: {clickedPosition}");

                if (!isMoving)
                {
                    endPosition = new Vector3(
                        Mathf.Floor(clickedPosition.x),
                        1,
                        Mathf.Floor(clickedPosition.z)
                    );

                    // Temporarily mark enemy’s cell as obstacle
                    SetOrResetCell(enemyManager.transform.position);

                    SetPath(); // Calculate path

                    // Unmark enemy’s cell after pathfinding
                    SetOrResetCell(enemyManager.transform.position, false);
                }

                // Start moving if path is found
                if (path != null && path.Count > 0 && !isMoving)
                {
                    Debug.Log($"Starting movement. Path length: {path.Count}");
                    StopAllCoroutines();
                    StartCoroutine(FollowPath());
                }
                else
                {
                    Debug.LogWarning("No path found or already moving.");
                }
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }

    public IEnumerator FollowPath()
    {
        isMoving = true;
        yield return StartCoroutine(Startmoving());

        isMoving = false;
        Debug.Log("Player finished moving.");
        isPlayerStationary?.Invoke();
    }
}
