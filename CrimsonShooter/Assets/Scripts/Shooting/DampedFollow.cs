using UnityEngine;

public class DampedFollow : MonoBehaviour {

    public Transform target;         // The camera or hand that the gun should follow
    public float smoothTime = 0.1f;  // The time it takes to catch up to the target position
    public Vector3 positionOffset;   // Offset for the gun's position relative to the target
    public Vector3 rotationOffset;   // Offset for the gun's rotation relative to the target

    private Vector3 currentVelocity; // Stores the current velocity for SmoothDamp

    void LateUpdate() {
        if (target == null)
            return;

        // Target position with offset
        Vector3 targetPosition = target.position + target.TransformDirection(positionOffset);
        // Smoothly move towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        // Target rotation with offset
        Quaternion targetRotation = target.rotation * Quaternion.Euler(rotationOffset);
        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime / smoothTime);
    }
}
