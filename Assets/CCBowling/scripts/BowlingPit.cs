using UnityEngine;

namespace CCBowling
{
    // Lectures 7 and 8.
    // Goes on the pit at the end of the lane. Its collider must have
    // Is Trigger ticked, and the ball prefab must be tagged "Ball".
    //
    // The pit owns the detection rather than the ball, so there is one script
    // watching one place instead of one on every ball that ever spawns.
    public class BowlingPit : MonoBehaviour
    {
        private BowlingGameManager gameManager;

        // Stops a second object in the pit reporting a throw that already ended.
        private bool throwReported = false;

        void Start()
        {
            // Unity 6 name. FindObjectOfType still works but warns in the Console.
            gameManager = FindFirstObjectByType<BowlingGameManager>();

            if (gameManager == null)
            {
                Debug.LogWarning("No BowlingGameManager in the scene");
            }
        }

        // Unity calls this when something enters a collider marked Is Trigger.
        // Note the type: Collider here, Collision for OnCollisionEnter.
        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Ball")) return;
            if (throwReported || gameManager == null) return;

            throwReported = true;

            // Lecture 8. Report first, then clean up. Do it the other way round
            // and the object is gone before it can speak.
            Destroy(other.gameObject);

            // Ready for the next ball. The manager takes resetDelay seconds to
            // spawn one, so this is clear long before anything else arrives.
            throwReported = false;

            Invoke("CheckPins", 2f);
        }

        private void CheckPins()
        {
            Debug.Log("In the pit, throw is over");
            gameManager.NextThrow();
        }


    }
}