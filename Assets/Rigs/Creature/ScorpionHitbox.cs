using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hodgkins
{
    public class ScorpionHitbox : MonoBehaviour
    {

        public float damageAmount = 20;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnTriggerEnter(Collider other)
        {
            HealthSystem health = other.GetComponent<HealthSystem>();
            DudeController dc = other.GetComponent<DudeController>();

            if (health && dc)
            {
                health.TakeDamage(damageAmount);
            }

            SoundEffectBoard.PlayPunch();

        }
    }
}