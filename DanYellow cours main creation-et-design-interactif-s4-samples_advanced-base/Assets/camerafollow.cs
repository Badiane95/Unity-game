using UnityEngine;

public class CameraFollowBehavior : MonoBehaviour
{
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime;
    public Transform target;
    private Vector3 nextPosition;

    void Start()
    {
        // Initialisation si nécessaire
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            nextPosition = target.position + new Vector3(
                offset.x,
                offset.y,
                transform.position.z
            );

            transform.position = Vector3.SmoothDamp(
                transform.position,
                nextPosition,
                ref velocity,
                smoothTime
            );
        }
    }
}
