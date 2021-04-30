using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hodgkins
{
    public class ScorpionController : MonoBehaviour
    {

        public Transform groundRing;

        public Transform armL;

        private Vector3 armLStartingPos;

        public List<StickyFoot> feet = new List<StickyFoot>();

        //CharacterController pawn;

        private NavMeshAgent nav;

        public Transform attackTarget;

        private float disToTarget;

        private Vector3 attackPos = new Vector3(-1.1f, 1.5f, 6);


        void Start()
        {
            //pawn = GetComponent<CharacterController>();
            nav = GetComponent<NavMeshAgent>();
            armLStartingPos = armL.localPosition;
        }


        void Update()
        {
            Move();

            CheckForAttack();

            int feetStepping = 0;
            int feetMoved = 0;
            foreach (StickyFoot foot in feet)
            {
                if (foot.isAnimating) feetStepping++;
                if (foot.footHasMoved) feetMoved++;
            }

            if (feetMoved >= 8)
            {
                foreach (StickyFoot foot in feet)
                {
                    foot.footHasMoved = false;
                }
            }

            foreach (StickyFoot foot in feet)
            {
                if (feetStepping < 4)
                {
                    if (foot.TryToStep()) feetStepping++;
                }
            }

        }

        private void CheckForAttack()
        {
            disToTarget = Vector3.Distance(attackTarget.position, transform.position);

            if (disToTarget <= 7)
            {                
                armL.localPosition = AnimMath.Slide(armL.localPosition, attackPos, .01f);
                //SoundEffectBoard.PlayThrownPunch();
            }

            if(disToTarget > 7)
            {
                armL.localPosition = armLStartingPos;
            }
            
        }

        private void Move()
        {
            //float v = Input.GetAxisRaw("Vertical");
            //float h = Input.GetAxisRaw("Horizontal");
            if (attackTarget != null) nav.SetDestination(attackTarget.position);

            Vector3 velocity = transform.forward;
            //velocity.Normalize();
            //pawn.SimpleMove(velocity * 5);

            transform.Rotate(0, 30 * Time.deltaTime, 0);

            Vector3 localVelocity = groundRing.InverseTransformDirection(velocity);

            groundRing.localPosition = AnimMath.Slide(groundRing.localPosition, localVelocity * 2, .0001f);

            //groundRing.localEulerAngles = new Vector3(0, h * 30, 0);
        }

        
    }
}