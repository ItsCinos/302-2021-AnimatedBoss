using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

namespace Hodgkins
{
    public class GUIController : MonoBehaviour
    {
        
        public Text playerHealthDisplay;
        public Text bossHealthDisplay;

        public HealthSystem playerHealth;
        public HealthSystem bossHealth;

        public Transform player;
        public Transform playerHips;
        public Transform playerHead;
        public DudeController dc;

        public Transform boss;
        public Transform bossHips;
        public Transform bossTail;
        public NavMeshAgent sc;


        void Start()
        {
            
        }


        // Update is called once per frame
        void Update()
        {            
            playerHealthDisplay.text = playerHealth.health + "%";
            //updatedBossHealth = bossHealth / 10;
            bossHealthDisplay.text = bossHealth.health + "%";

            if (playerHealth.health <= 0)
            {
                Destroy(dc);
                playerHips.localRotation = AnimMath.Slide(playerHips.localRotation, new Quaternion(60, 0, 0, 0), .4f);
                playerHead.localRotation = AnimMath.Slide(playerHead.localRotation, new Quaternion(35, 0, 0, 0), .1f);
                player.localRotation = AnimMath.Slide(player.localRotation, new Quaternion(0, 0, -90, 0), .3f);

            }

            if (bossHealth.health <= 0)
            {
                
                Destroy(sc);
                Vector3 bossHipsDeathPos = bossHips.localPosition;
                bossHipsDeathPos.y = bossHips.localPosition.y - .25f;
                bossHips.localPosition = AnimMath.Slide(bossHips.localPosition, bossHipsDeathPos, .1f);

            }
        }
    }
}