using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKControl : MonoBehaviour {

    protected Animator animator;

    public bool ikActive = false;
    public Transform IKTarget = null;

    public Transform lookObj = null;

    [SerializeField] private float maxIKDistance;
    [SerializeField] private float maxIKRotation;

    [SerializeField] private Transform IKCenter;

    [SerializeField] private Transform rightHandObj = null;
    [SerializeField] private Transform leftHandObj = null;
    [SerializeField] private float recoilDistance = 0.15f;

    private Transform aimTarget;
    
    public void SetAimTarget(Transform target) {
        aimTarget = target;
    }


    public void Fire() {
        recoilImpulse = recoilDistance;
    }
    private float recoilImpulse;
    private float recoilRecoverySpeed = 0.5f;
    private void Update() {
        recoilImpulse = Mathf.MoveTowards(recoilImpulse, 0f, recoilRecoverySpeed * Time.deltaTime);

    }
    void Start() {
        animator = GetComponent<Animator>();
        defaultIKPos = IKTarget.transform.position;
        defaultIKRot = IKTarget.transform.rotation;
    }

    private Vector3 defaultIKPos;
    private Quaternion defaultIKRot;


    //a callback for calculating IK
    void OnAnimatorIK() {


        if (aimTarget != null) {
            Vector3 dirToTarget = (aimTarget.position - IKCenter.position).normalized;
            float angle = Vector3.Angle(dirToTarget, IKCenter.forward);
            if(angle > maxIKRotation) {
                IKTarget.position = defaultIKPos;
                IKTarget.rotation = defaultIKRot;
            } else {



                IKTarget.position = IKCenter.position + dirToTarget * maxIKDistance;
                IKTarget.rotation = Quaternion.LookRotation(dirToTarget);

                IKTarget.position = IKTarget.position + IKTarget.up * recoilImpulse;
            }

        } else {
            IKTarget.position = defaultIKPos;
            IKTarget.rotation = defaultIKRot;
        }


        if (animator) {

            //if the IK is active, set the position and rotation directly to the goal.
            if (ikActive) {
                // Set the look target position, if one has been assigned
                if (lookObj != null) {
                    animator.SetLookAtWeight(1, 0, 1, 0, 0.3f);
                    animator.SetLookAtPosition(lookObj.position - lookObj.up * recoilImpulse);
                }

                if (IKTarget != null) {
                    
                
                }

                // Set the right hand target position and rotation, if one has been assigned
                if (rightHandObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);

                }
                if (rightHandObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
                }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }
}
