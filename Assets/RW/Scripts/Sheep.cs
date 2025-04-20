using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed;
    public float gotHayDestroyDelay;
    public float dropDestroyDelay;
    public float jumpHeight = 5.0f; // Increased jump height significantly
    public float jumpDuration = 0.6f; // Duration for the smooth jump
    public float minJumpDelay = 2.0f;
    public float maxJumpDelay = 7.0f;

    private bool hitByHay;
    private Collider myCollider;
    private Rigidbody myRigidbody;
    private SheepSpawner sheepSpawner;
    private bool hasJumped = false;
    private float jumpTimer;
    private bool isJumping = false;

    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.isKinematic = true;

        jumpTimer = Random.Range(minJumpDelay, maxJumpDelay);
        hasJumped = false;
        isJumping = false;
    }

    void Update()
    {
        if (hitByHay) return;

        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);

        if (!hasJumped && !isJumping)
        {
            jumpTimer -= Time.deltaTime;
            if (jumpTimer <= 0)
            {
                StartCoroutine(PerformJump());
                hasJumped = true;
            }
        }
    }

    private IEnumerator PerformJump()
    {
        isJumping = true;
        Vector3 startPos = transform.position;
        Vector3 peakPos = startPos + Vector3.up * jumpHeight;
        float halfDuration = jumpDuration / 2.0f;
        float elapsedTime = 0f;

        // Jump up smoothly
        while (elapsedTime < halfDuration)
        {
            transform.position = Vector3.Lerp(startPos, peakPos, elapsedTime / halfDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = peakPos; // Ensure it reaches the peak

        elapsedTime = 0f;

        // Jump down smoothly
        while (elapsedTime < halfDuration)
        {
            transform.position = Vector3.Lerp(peakPos, startPos, elapsedTime / halfDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = startPos; // Ensure it returns to the start position

        isJumping = false;
    }

    private void HitByHay()
    {
        if (sheepSpawner != null)
        {
            Debug.Log("Sheep Hit! Telling Spawner to increment score."); // DEBUG LOG
            sheepSpawner.IncrementScore();
        }
        else
        {
            Debug.LogError("Sheep cannot find its Spawner to increment score!"); // DEBUG LOG
        }

        sheepSpawner.RemoveSheepFromList(gameObject);
        hitByHay = true;
        runSpeed = 0;
        Destroy(gameObject, gotHayDestroyDelay);
    }

    private void Drop()
    {
        // Tell the spawner that a life was lost
        if (sheepSpawner != null)
        {
            sheepSpawner.LoseLife();
        }

        // Existing drop logic
        sheepSpawner.RemoveSheepFromList(gameObject); // This line might be redundant if LoseLife handles removal, but safe to keep for now.
        myRigidbody.isKinematic = false;
        myCollider.isTrigger = false;
        Destroy(gameObject, dropDestroyDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hay") && !hitByHay)
        {
            Debug.Log("Hay collision detected!"); // DEBUG LOG
            Destroy(other.gameObject);
            HitByHay();
        }
        else if (other.CompareTag("DropSheep"))
        {
            Drop();
        }
    }

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }
}
