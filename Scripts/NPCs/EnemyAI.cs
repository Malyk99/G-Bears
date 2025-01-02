using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 3f; // Movement speed of the enemy
    public Transform[] players; // Array to hold references to all players in the scene
    public float awarenessRadius = 10f; // The range within which the enemy can detect players
    public float fieldOfViewAngle = 90f; // Field of view in degrees
    public LayerMask obstructionMask; // Layers that block line of sight (e.g., walls)

    private Transform targetPlayer; // The player the enemy is currently chasing

    private void Update()
    {
        FindClosestPlayer(); // Determine the closest player within awareness and line of sight
        ChasePlayer();       // Move towards the closest valid target
    }

    // Find the closest player to the enemy
    private void FindClosestPlayer()
    {
        float closestDistance = Mathf.Infinity; // Set initial closest distance to a very high value
        Transform closestPlayer = null;

        foreach (Transform player in players)
        {
            if (player == null) continue; // Skip null references

            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float distanceToPlayer = Vector3.SqrMagnitude(player.position - transform.position); // Squared distance for performance

            // Check if the player is within awareness radius
            if (distanceToPlayer <= awarenessRadius * awarenessRadius)
            {
                // Check if the player is within the field of view
                float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
                if (angleToPlayer <= fieldOfViewAngle / 2f)
                {
                    // Check line of sight using Raycast
                    if (!Physics.Raycast(transform.position, directionToPlayer, Mathf.Sqrt(distanceToPlayer), obstructionMask))
                    {
                        // If line of sight is clear and within FOV, consider this player
                        if (distanceToPlayer < closestDistance)
                        {
                            closestDistance = distanceToPlayer;
                            closestPlayer = player;
                        }
                    }
                }
            }
        }

        targetPlayer = closestPlayer; // Update the target player
    }

    // Move towards the closest player
    private void ChasePlayer()
    {
        if (targetPlayer != null)
        {
            Vector3 direction = (targetPlayer.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    // Optional: Gizmos to visualize the awareness radius and field of view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, awarenessRadius);

        // Visualize the field of view
        Vector3 leftBoundary = Quaternion.Euler(0, -fieldOfViewAngle / 2, 0) * transform.forward * awarenessRadius;
        Vector3 rightBoundary = Quaternion.Euler(0, fieldOfViewAngle / 2, 0) * transform.forward * awarenessRadius;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}
