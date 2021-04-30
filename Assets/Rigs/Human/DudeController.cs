using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hodgkins
{
    public class DudeController : MonoBehaviour
    {
        public enum States
        {
            Idle,
            Walk
        }

        private CharacterController pawn;

        public float moveSpeed = 5;

        public float stepSpeed = 5;

        private bool wantsToSprint;

        public Vector3 walkScale = Vector3.one;

        public AnimationCurve ankleRotationCurve;

        public bool isDead = false;

        public Camera cam;

        public States state { get; private set; }


        public Vector3 moveDir { get; private set; }

        void Start()
        {
            state = States.Idle;
            pawn = GetComponent<CharacterController>();
        }


        void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            wantsToSprint = Input.GetButton("Fire3");

            moveDir = transform.forward * v + transform.right * h;
            if (moveDir.sqrMagnitude > 1) moveDir.Normalize();

            pawn.SimpleMove(moveDir * moveSpeed);

            if (wantsToSprint) pawn.SimpleMove(moveDir * moveSpeed * 1.5f);

            bool isTryingToMove = (h != 0 || v != 0);
            if (isTryingToMove)
            {
                //turn to face correct direction
                float camYaw = cam.transform.eulerAngles.y;
                transform.rotation = AnimMath.Slide(transform.rotation, Quaternion.Euler(0, camYaw, 0), .02f);
            }

            state = (moveDir.sqrMagnitude > .1f) ? States.Walk : States.Idle;

            //if (HealthSystem.health <= 0) DeathAnim();

        }

        void DeathAnim()
        {

        }
    }
}