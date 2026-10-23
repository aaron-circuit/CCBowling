using UnityEngine;

// Lectures 7 and 8.
// One empty GameObject in the scene, called GameManager, with this on it.
//
// Nothing else decides what happens next. The Pit reports, the Pins answer
// questions, the ScoreManager does arithmetic, and this decides.

namespace CCBowling
{
    public class BowlingGameManager : MonoBehaviour
    {
        public BowlingPlayerController playerController;
        public BowlingScoreManager scoreManager;

        // Drag all ten pins in here. Size 10.
        // In Unit 2 you learn to fill this with code instead.
        public BowlingPin[] pins;

        // Seconds between the ball reaching the pit and the next ball appearing.
        // Long enough to watch the pins settle. 2 is about right.
        public float resetDelay = 2f;

        // Called by BowlingPit when the ball reaches the pit.
        public void NextThrow()
        {
            // Lecture 8. This is the count for THIS THROW only, because counted
            // pins get taken off the lane.
            int knockedDown = CountFallenPins();
            Debug.Log(knockedDown + " pins down this throw");

            if (scoreManager == null) return;
            scoreManager.AddThrow(knockedDown);

            // Lecture 8. Invoke calls a method by name after a delay, so the pins
            // are not yanked away the instant the ball disappears.
            Invoke("ReadyNextBall", resetDelay);
        }

        // Lecture 7 in one method, plus the Knockdown line from Lecture 8.
        public int CountFallenPins()
        {
            int fallen = 0;

            foreach (BowlingPin pin in pins)
            {
                if (pin.HasFallen())
                {
                    fallen++;

                    // Counted once, then off the lane, so the next throw only
                    // reports the pins IT knocked down.
                    pin.Knockdown();
                }
            }

            return fallen;
        }

        // Runs resetDelay seconds after the ball reaches the pit.
        private void ReadyNextBall()
        {
            if (scoreManager.GameIsOver())
            {
                Debug.Log("Game over. Final score: " + scoreManager.TotalScore());
                return;
            }

            // Fresh rack at the start of every frame, and only then.
            if (scoreManager.FrameIsOver())
            {
                ResetAllPins();
            }

            playerController.NextShot();
        }

        public void ResetAllPins()
        {
            foreach (BowlingPin pin in pins)
            {
                pin.ResetPin();
            }
        }
    }

}