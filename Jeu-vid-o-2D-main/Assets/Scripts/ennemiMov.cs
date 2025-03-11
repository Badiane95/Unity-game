using NUnit.Framework;
using UnityEngine;

public class EnnemiMov : MonoBehaviour
{
   public Rigidbody2D rb;
   public BoxCollider2D bc;
   public LayerMask listObstacleLayers;
   public float groundCheckRadius = 0.15f;
   public Animator animator;
   public float moveSpeed = 3f;
   public bool isFacingRight = false ;
   public float distanceDetection = 1f;
   public void FixedUpdate() {
    if (rb.linearVelocity.y != 0) {
        return;
    }
    rb.linearVelocity = new Vector2 ( //deplacement automatique vers la droite 
         moveSpeed * transform.right.normalized.x,
        rb.linearVelocityY
    );    
        animator.SetFloat("VelocityX", Mathf.Abs(rb.linearVelocityX));
        if(HasNotTouchedGround() || HasCollisionWithObstacles()) {Flip();}
   }

    bool HasNotTouchedGround() {
        Vector2 detectionPosition = new Vector2(
            bc.bounds.center.x + (transform.right.normalized.x * bc.bounds.size.x/2),
            bc.bounds.min.y
        ); 
        return !Physics2D.OverlapCircle(
            detectionPosition,
            groundCheckRadius,
            listObstacleLayers
        );
    }
    private void Flip() {
        if ((transform.right.normalized.x > 0 && !isFacingRight) || (transform.right.normalized.x < 0 && isFacingRight)) {
            transform.Rotate(0,180,0);
            isFacingRight = !isFacingRight;
        } 
    }

    bool HasCollisionWithObstacles() {
        RaycastHit2D hit = Physics2D.Linecast(
            bc.bounds.center,
            bc.bounds.center + new Vector3 (
                distanceDetection * transform.right.normalized.x,
                0,
                0
            ),
            listObstacleLayers
        );
        return hit.transform != null;
    }
    public void OnDrawGizmos() {
        if (bc !=null ) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                bc.bounds.center,
                 bc.bounds.center + new Vector3 (
                distanceDetection * transform.right.normalized.x,
                0,
                0
                )
            );
            Vector2 detectionPosition = new Vector2(
            bc.bounds.center.x + (transform.right.normalized.x * bc.bounds.size.x/2),
            bc.bounds.min.y
            ); 
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(
            detectionPosition,
            groundCheckRadius
            );
        }
    }
}
