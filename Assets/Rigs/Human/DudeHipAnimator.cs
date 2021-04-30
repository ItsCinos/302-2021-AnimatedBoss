using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hodgkins
{
    public class DudeHipAnimator : MonoBehaviour
    {
        float rollAmount = .75f;

        Quaternion startingRot;
        DudeController dude;

        private bool wantsToSprint;

        void Start()
        {
            startingRot = transform.localRotation;
            dude = GetComponentInParent<DudeController>();
        }


        void Update()
        {
            switch (dude.state)
            {
                case DudeController.States.Idle:
                    AnimateIdle();
                    break;
                case DudeController.States.Walk:
                    AnimateWalk();
                    break;
            }
        }

        void AnimateIdle()
        {
            transform.localRotation = startingRot;
        }

        void AnimateWalk()
        {
            float time = Time.time * dude.stepSpeed * 2;
            float roll = Mathf.Sin(time) * rollAmount;

            transform.localRotation = Quaternion.Euler(10, 0, roll);

            wantsToSprint = Input.GetButton("Fire3");
            if(wantsToSprint)
            {
                transform.localRotation = Quaternion.Euler(20, 0, roll);
            }
        }
    }
}