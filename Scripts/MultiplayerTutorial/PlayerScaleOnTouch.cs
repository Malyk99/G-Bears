using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScaleOnTouch : MonoBehaviour
{
    // Store the original scale and mass of the player
    private Vector3 originalScale;
    private float originalMass;
    private Rigidbody playerRigidbody;
    private bool isScaledUp = false;
    private bool isScaledDown = false;

    // Time durations for scaling
    private float scaleUpDuration = 1f;  // Time it takes to scale up (1 second)
    private float scaleDownDuration = 1f; // Time it takes to scale down (1 second)
    private float stayLargeDuration = 10f; // Time it stays large (10 seconds)
    private float moveSpeedMultiplier = 1f; // Normal movement speed multiplier

    // Start is called before the first frame update
    void Start()
    {
        // Store the original scale of the player
        originalScale = transform.localScale;

        // Get the player's Rigidbody to modify its mass
        playerRigidbody = GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            originalMass = playerRigidbody.mass;
        }
    }

    // This method is triggered when the player enters a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player touches the "ScaleTrigger" to scale up
        if (other.CompareTag("ScaleTrigger") && !isScaledUp)
        {
            // Start the scaling up coroutine
            StartCoroutine(ScaleUpCoroutine());
        }

        // Check if the player touches the "ScaleTriggerDown" to scale down
        if (other.CompareTag("ScaleTriggerDown") && !isScaledDown)
        {
            // Start the scaling down coroutine
            StartCoroutine(ScaleDownCoroutine());
        }
    }

    // Coroutine to scale the player up over time
    private IEnumerator ScaleUpCoroutine()
    {
        // Ensure that scaling up doesn't trigger multiple times
        isScaledUp = true;

        Vector3 targetScale = originalScale * 3f; // 3x original size
        float elapsedTime = 0f;

        // Scale up over time (1 second)
        while (elapsedTime < scaleUpDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / scaleUpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the scale is exactly 3x at the end
        transform.localScale = targetScale;

        // Increase mass to simulate heavier weight
        if (playerRigidbody != null)
        {
            playerRigidbody.mass = originalMass * 2f; // Double the mass when scaled up
        }

        // Wait for the large size duration (10 seconds)
        yield return new WaitForSeconds(stayLargeDuration);

        // Start scaling down back to the original size
        StartCoroutine(ScaleDownCoroutine());
    }

    // Coroutine to scale the player down over time
    private IEnumerator ScaleDownCoroutine()
    {
        Vector3 targetScale;
        float elapsedTime = 0f;

        // If the player is scaled up, begin scaling down to the original size
        if (isScaledUp)
        {
            targetScale = originalScale;
            while (elapsedTime < scaleDownDuration)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, elapsedTime / scaleDownDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Reset mass to original
            if (playerRigidbody != null)
            {
                playerRigidbody.mass = originalMass;
            }
        }
        // If the player is at the original size, scale down further to 1/3 of the original size
        else if (!isScaledUp && !isScaledDown)
        {
            targetScale = originalScale / 3f; // 1/3 original size
            while (elapsedTime < scaleDownDuration)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, elapsedTime / scaleDownDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Double the movement speed when scaled down
            moveSpeedMultiplier = 2f; // Doubles the speed

            // Wait for small size duration (10 seconds) if needed
            yield return new WaitForSeconds(stayLargeDuration);

            // Reset scale and speed
            transform.localScale = originalScale;
            moveSpeedMultiplier = 1f; // Reset speed to normal

            isScaledDown = false;
        }

        // Ensure the scale is exactly the original size at the end
        transform.localScale = originalScale;
        isScaledUp = false;
    }

    // You can use this method to modify the player's movement speed elsewhere in the game (e.g., during update or fixed update)
    public float GetMovementSpeed()
    {
        return moveSpeedMultiplier;
    }
}

