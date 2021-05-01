using System;
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

        public float gravityMultiplier = 2;
        public float jumpInpulse = 10;

        public Transform ground;
        private Vector3 groundStartPos;

        private float timeLeftGrounded = 0;

        public bool isGrounded
        {
            get // return true if pawn is on ground OR "coyote-time" isn't zero
            {
                return pawn.isGrounded || timeLeftGrounded > 0;
            }
        }

        /// <summary>
        /// How fast player is currently moving vertically (y-axis), in m/s
        /// </summary>
        private float verticalVelocity = 0;

        //public bool isDead = false;

        public Camera cam;

        public States state { get; private set; }


        public Vector3 moveDir { get; private set; }

        void Start()
        {
            state = States.Idle;
            pawn = GetComponent<CharacterController>();
            groundStartPos = ground.localPosition;
            //SoundEffectBoard.PlaySong();
        }


        void Update()
        {
            MoveDude();

            JumpDude();

            state = (moveDir.sqrMagnitude > .1f) ? States.Walk : States.Idle;

            //if (HealthSystem.health <= 0) DeathAnim();

        }


        void MoveDude()
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
        }
        private void JumpDude()
        {
            bool isJumpHeld = Input.GetButton("Jump");
            bool onJumpPress = Input.GetButtonDown("Jump");

            // apply gravity
            verticalVelocity += gravityMultiplier * Time.deltaTime;


            //adds lateral movement to vertical movement
            Vector3 moveDelta = moveDir * moveSpeed + verticalVelocity * Vector3.down;

            // move pawn
            CollisionFlags flags = pawn.Move(moveDelta * Time.deltaTime);
            ground.localPosition = AnimMath.Slide(ground.localPosition, new Vector3 (0, -.5f, 0), .05f);

            if (pawn.isGrounded)
            {
                verticalVelocity = 0; // on ground, zero-out vert-velocity
                timeLeftGrounded = .2f;
                ground.localPosition = groundStartPos;
            }

            if (isGrounded)
            {
                if (isJumpHeld)
                {
                    SoundEffectBoard.PlayJump();
                    verticalVelocity = -jumpInpulse;
                    timeLeftGrounded = 0; // not on ground (for animation's sake)
                }
            }
        }

        void DeathAnim()
        {

        }
    }
}