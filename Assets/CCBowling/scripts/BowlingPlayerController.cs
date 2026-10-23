using UnityEngine;
using UnityEngine.InputSystem;

namespace CCBowling
{
    public class BowlingPlayerController : MonoBehaviour
    {


        public GameObject[] ballPrefabs; // What do I want to spawn

        public Transform arrowObject;

        public Vector3 arrowMoveAmount = new Vector3(1.5f, 0, 0);

        public float speed = 1.5f;

        public Vector3 ballOffset; // Where should the ball appear? (And lock)

        public float ballLaunchSpeed = 24f;




        // Tracked variables
        private GameObject currentBall;
        private int ballsShot = 0;
        private Vector3 arrowStartPosition;
        private bool isBallThrown = false;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            arrowStartPosition = arrowObject.position;
            NextShot();
        }

        // Update is called once per frame
        void Update()
        {
            ReadPlayerInput();
            MoveArrow();
            SyncBallPosition();
        }

        private void ReadPlayerInput()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                ThrowBall();
            }
        }

        private void ThrowBall()
        {
            if (isBallThrown || currentBall == null)
            {
                Debug.Log("isBallThrown : " + isBallThrown);
                Debug.Log("currentBall == null : " + (currentBall == null));
                return;
            }

            Rigidbody ballRb;
            if (currentBall.TryGetComponent<Rigidbody>(out ballRb))
            {
                ballRb.isKinematic = false;
                ballRb.AddForce(Vector3.forward * ballLaunchSpeed, ForceMode.Impulse);
                isBallThrown = true;
            }

            if (arrowObject != null) arrowObject.gameObject.SetActive(false);

        }

        private void MoveArrow()
        {
            arrowObject.position = arrowStartPosition + (Mathf.Sin(Time.time * speed) * arrowMoveAmount);
        }


        private void SyncBallPosition()
        {
            if (currentBall == null || isBallThrown) return;

            currentBall.transform.position = arrowObject.transform.position + ballOffset;
        }




        public void NextShot() // Prepare yourself for the next shot
        {

            if (currentBall != null)
            {
                Destroy(currentBall.gameObject);
            }

            ballsShot++;

            // The int overload of Random.Range excludes the max, so Length - 1
            // meant the last prefab in the list could never be picked.
            int whichBall = Random.Range(0, ballPrefabs.Length);
            Vector3 spawnPosition = arrowObject.transform.position + ballOffset;
            currentBall = Instantiate(ballPrefabs[whichBall], spawnPosition, Quaternion.identity);
            currentBall.name = "Ball_" + ballsShot.ToString();

            // Without this the flag stays true after the first throw and
            // ThrowBall refuses every ball after it.
            isBallThrown = false;

            if (arrowObject != null) arrowObject.gameObject.SetActive(true);

        }


    }

}