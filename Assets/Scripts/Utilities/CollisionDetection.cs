using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            // Get the collision normal vector
            Vector2 collisionNormal = collision.contacts[0].normal;

            // Calculate the angle between the collision normal and up vector
            float angle = Vector2.Angle(collisionNormal, Vector2.up);

            // Determine the side of the collision based on the angle
            if (angle < 45f)
            {
                // Top collision
                Debug.Log("Top collision");
            }
            else if (angle > 135f)
            {
                // Bottom collision
                Debug.Log("Bottom collision");
            }
            else if (collisionNormal.x > 0f)
            {
                // Right collision
                
                Debug.Log("Right collision");
            }
            else
            {
                // Left collision
                Debug.Log("Left collision");
            }
        }
        
    }
}