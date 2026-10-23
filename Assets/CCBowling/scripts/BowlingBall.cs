using UnityEngine;

namespace CCBowling
{
    // Lecture 7, Section 1.
    // Goes on the ball prefab, so every variant gets it.
    //
    // The pit detection lives on BowlingPit, not here. This script only reports
    // what the ball runs into on the way down.
    public class BowlingBall : MonoBehaviour
    {

        public AudioSource ballAudio;
        public AudioSource pinAudio;

        // Unity calls this when this object HITS something. Note the type:
        // Collision here, Collider for triggers. They are not the same.
        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Pin"))
            {
                Debug.Log("Hit a pin: " + other.gameObject.name);
                if (pinAudio != null)
                {
                    pinAudio.Play();
                }
            }

            if (other.gameObject.CompareTag("Lane"))
            {
                Debug.Log("Hit the lane: " + other.gameObject.name);

                if(ballAudio != null)
                {
                    ballAudio.Play();
                }

            }


        }





    }
}
