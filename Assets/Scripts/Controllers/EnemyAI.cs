using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// Manages Enemy and movement
/// Inherits its path AI finding from MoveableObject class and blueprint from InterfaceAI
public class EnemyAI : MoveableObject, InterfaceAI
{
    // Reference to the Player objects Manager
    [SerializeField] private GameObject playerManager;

    private void Awake()
    {
        LoadData();
    }
    // Keeps track if Enemy is currently moving or not
    public bool isMoving { get; private set; }

    private void Start()
    {
        isMoving = false;
        PlayerController.isPlayerStationary += TriggerEnemyMovement;
        TriggerEnemyMovement();
    }

    //Starts Pathfinding and movement towards the player position
    private void TriggerEnemyMovement()
    {

        // Gets the player postion from the grid
        endPosition = new Vector3(
            Mathf.Floor(playerManager.transform.position.x),
            1,
            Mathf.Floor(playerManager.transform.position.z)
        );
        
        SetPath();

        //if path found start following it
        if (path != null && path.Count > 0 && !isMoving)
        {
            StopAllCoroutines();
            StartCoroutine(FollowPath());
        }
    }
    /// Moves the enemy along the calculated path.
    /// Stops before reaching the player final position.
    public IEnumerator FollowPath()
    {
        isMoving = true;

        // Remove the final step to avoid occupying the player tile
        path.RemoveAt(path.Count - 1);
        
        yield return StartCoroutine(Startmoving());

        isMoving = false;
    }

}
