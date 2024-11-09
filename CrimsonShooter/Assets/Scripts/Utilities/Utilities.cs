using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Utilities
{
    public static Quaternion GetRotationTowardsTarget(Transform fromTransform, Transform target, float angularSpeed, float deltaTime) {
        // Calculate the direction from `fromTransform` to the target
        Vector3 direction = target.position - fromTransform.position;

        // Set the Y component to zero to constrain rotation around the Y axis
        direction.y = 0;

        if (direction.magnitude > 0.1f) {
            // Create the target rotation while keeping Y axis up
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Calculate and return the smoothed rotation towards the target
            return Quaternion.RotateTowards(
                fromTransform.rotation,
                targetRotation,
                angularSpeed * deltaTime
            );
        }

        // Return the original rotation if the direction is too small
        return fromTransform.rotation;
    }

    public static Vector3 SetAgentStrafeLeft(NavMeshAgent agent, Transform transform, Transform originalTarget, float distance) {
        // Calculate the direction from the original target to the transform
        Vector3 directionToTransform = (transform.position - originalTarget.position).normalized;

        // Get a perpendicular direction to the calculated direction
        Vector3 perpendicularDirection = Vector3.Cross(directionToTransform, Vector3.up).normalized;
        perpendicularDirection.y = 0f;

        // Calculate two candidate points at the specified distance on each side of the perpendicular direction
        Vector3 candidatePoint1 = transform.position + perpendicularDirection * distance;
        float currentDistance = Vector3.Distance(transform.position, originalTarget.position);

        candidatePoint1 = Vector3.MoveTowards(originalTarget.position, candidatePoint1, currentDistance);

        // Find the closest point on the NavMesh to each candidate point
        NavMeshHit hit1;
        bool found1 = NavMesh.SamplePosition(candidatePoint1, out hit1, distance, NavMesh.AllAreas);

        // Select the closer valid point
        Vector3 targetPosition;
        if (found1) {
            targetPosition = hit1.position;
        }  else {
            // If no valid position was found on the NavMesh, return the current agent position
            targetPosition = agent.transform.position;
        }

        // Set the agent's destination to the chosen target position
        agent.SetDestination(targetPosition);
        return targetPosition;
    }
    public static Vector3 SetAgentStrafeRight(NavMeshAgent agent, Transform transform, Transform originalTarget, float distance) {
        // Calculate the direction from the original target to the transform
        Vector3 directionToTransform = (transform.position - originalTarget.position).normalized;

        // Get a perpendicular direction to the calculated direction
        Vector3 perpendicularDirection = Vector3.Cross(directionToTransform, Vector3.up).normalized;
        perpendicularDirection.y = 0f;

        // Calculate two candidate points at the specified distance on each side of the perpendicular direction
        Vector3 candidatePoint2 = transform.position - perpendicularDirection * distance;
        float currentDistance = Vector3.Distance(transform.position, originalTarget.position);

        candidatePoint2 = Vector3.MoveTowards(originalTarget.position, candidatePoint2, currentDistance);

        // Find the closest point on the NavMesh to each candidate point
        NavMeshHit hit2;
        bool found2 = NavMesh.SamplePosition(candidatePoint2, out hit2, distance, NavMesh.AllAreas);

        // Select the closer valid point
        Vector3 targetPosition;
        if (found2) {
            targetPosition = hit2.position;
            
        } else {
            // If no valid position was found on the NavMesh, return the current agent position
            targetPosition = agent.transform.position;
        }

        // Set the agent's destination to the chosen target position
        agent.SetDestination(targetPosition);
        return targetPosition;
    }

    public static Vector3 SetAgentStrafe(NavMeshAgent agent, Transform transform, Transform originalTarget, float distance) {
        // Calculate the direction from the original target to the transform
        Vector3 directionToTransform = (transform.position - originalTarget.position).normalized;

        // Get a perpendicular direction to the calculated direction
        Vector3 perpendicularDirection = Vector3.Cross(directionToTransform, Vector3.up).normalized;
        perpendicularDirection.y = 0f;

        // Calculate two candidate points at the specified distance on each side of the perpendicular direction
        Vector3 candidatePoint1 = transform.position + perpendicularDirection * distance;
        Vector3 candidatePoint2 = transform.position - perpendicularDirection * distance;

        // Find the closest point on the NavMesh to each candidate point
        NavMeshHit hit1, hit2;
        bool found1 = NavMesh.SamplePosition(candidatePoint1, out hit1, distance, NavMesh.AllAreas);
        bool found2 = NavMesh.SamplePosition(candidatePoint2, out hit2, distance, NavMesh.AllAreas);

        // Select the closer valid point
        Vector3 targetPosition;
        if (found1 && found2) {
            targetPosition = Vector3.Distance(candidatePoint1, hit1.position) < Vector3.Distance(candidatePoint2, hit2.position)
                ? hit1.position : hit2.position;
        } else if (found1) {
            targetPosition = hit1.position;
        } else if (found2) {
            targetPosition = hit2.position;
        } else {
            // If no valid position was found on the NavMesh, return the current agent position
            targetPosition = agent.transform.position;
        }

        // Set the agent's destination to the chosen target position
        agent.SetDestination(targetPosition);
        return targetPosition;
    }
    public static bool HasReachedDestination(NavMeshAgent agent, float tolerance = 0.1f) {
        // Check if the agent has a valid path and is close enough to the target
        if (!agent.pathPending && agent.remainingDistance <= tolerance) {
            // Check if the agent has stopped moving
            return true;
            //return !agent.hasPath || agent.velocity.sqrMagnitude < Mathf.Epsilon;
        }

        return false;
    }
}
