using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreak : MonoBehaviour {
    public GameObject boxBreak1;
    public GameObject boxBreak2;
    public GameObject boxBreak3;
    public GameObject boxBreak4;
    public GameObject sword;
    public float explosionForce = 10f;
    public float pushForce = 5f;
    public float delay = 0.5f;
    public LayerMask playerLayer;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (IsPlayerAboveBlock()) {
            // Destroy the box
            Destroy(gameObject);

            // Create smaller pieces of the box
            GameObject[] boxPieces = new GameObject[] {boxBreak1, boxBreak2, boxBreak3, boxBreak4};
            foreach (GameObject boxPiece in boxPieces)
            {
                GameObject piece = Instantiate(boxPiece, transform.position, Quaternion.identity);

                Rigidbody2D rb = piece.GetComponent<Rigidbody2D>();

                Vector2 direction = Random.insideUnitCircle.normalized;

                rb.AddForce(direction * explosionForce, ForceMode2D.Impulse);
            }

            // Spawn a sword after a delay
            Invoke("SpawnSword", delay);
        } else {
            // Push the box
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 direction = -collision.contacts[0].normal.normalized;
            rb.AddForce(direction * pushForce, ForceMode2D.Impulse);
        }
    }

    private bool IsPlayerAboveBlock()
    {
        Vector2 position = transform.position;
        float distance = 0.5f;

        Vector2 direction = Vector2.up;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, playerLayer);

        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    private void SpawnSword() {
        Instantiate(sword, transform.position, Quaternion.identity);
    }
}


