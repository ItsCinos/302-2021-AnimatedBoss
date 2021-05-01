using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hodgkins
{       
    public class SoundEffectBoard : MonoBehaviour
    {
        public static SoundEffectBoard main;

        //public AudioClip soundJump;
        //public AudioClip soundSpring;
        public AudioClip soundShot;
        public AudioClip soundJump;
        public AudioClip soundDie;
        public AudioClip soundPunch;
        public AudioClip soundThrownPunch;
        public AudioClip soundSong;

        private AudioSource player;

        void Start()
        {
            if(main == null)
            {
                main = this;
                player = GetComponent<AudioSource>();
            } else {
                Destroy(this.gameObject);
            }
        }


        public static void PlayShot()
        {
            main.player.PlayOneShot(main.soundShot);
        }

        public static void PlayJump()
        {
            main.player.PlayOneShot(main.soundJump);
        }

        public static void PlayDie()
        {
            main.player.PlayOneShot(main.soundDie);
        }
        public static void PlayPunch()
        {
            main.player.PlayOneShot(main.soundPunch);
        }
        public static void PlayThrownPunch()
        {
            main.player.PlayOneShot(main.soundThrownPunch);
        }
        public static void PlaySong()
        {
            main.player.PlayOneShot(main.soundSong);
        }
    }
}