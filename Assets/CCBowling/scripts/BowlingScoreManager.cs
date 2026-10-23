using UnityEngine;
using UnityEngine.SceneManagement;

namespace CCBowling
{

    // Lecture 8, Section 1.
    // On its own empty GameObject called ScoreManager.
    //
    // Strikes and spares only. The tenth frame's bonus ball is ignored on
    // purpose, so a perfect game here scores 270 rather than 300.
    public class BowlingScoreManager : MonoBehaviour
    {
        // One score per frame. Ten frames, so indexes 0 to 9.
        public int[] frameScores = new int[10];

        // How many future throws still count towards each frame.
        // A strike owes 2, a spare owes 1.
        public int[] bonusThrowsLeft = new int[10];

        public int frame = 0;          // which frame we are on, 0 to 9 (or more, if you've modified your game)
        public int throwInFrame = 1;   // 1 or 2
        private int pinsThisFrame = 0;

        public ScoreUIManager scoreUI;


        public void Start()
        {
            if (scoreUI == null)
            {
                scoreUI = MenuUISingleton.GetScoreUI();
            }
        }

        // Called once per throw, with the number of pins that went down.
        public void AddThrow(int pins)
        {
            if (frame > 9) return;   // game over

            // STEP 1. Any earlier frame still owed bonus throws gets these pins too.
            // This is what makes a strike worth more than ten.
            for (int i = 0; i < frame; i++)
            {
                if (bonusThrowsLeft[i] > 0)
                {
                    frameScores[i] += pins;
                    if (scoreUI != null)
                    {
                        scoreUI.SetFrameTotal(i, frameScores[i]);
                    }
                    bonusThrowsLeft[i]--;
                }
            }

            // STEP 2. This frame always scores the pins you just knocked down.
            frameScores[frame] += pins;
            pinsThisFrame += pins;

            // STEP 2.1 Set the frame score
            if (scoreUI != null)
            {
                scoreUI.SetFrameScore(frame, throwInFrame, pins, pinsThisFrame);
                scoreUI.SetFrameTotal(frame, frameScores[frame]);
            }


            // STEP 3. Work out what happens next.
            if (throwInFrame == 1)
            {
                // Strike. The next two throws also count towards this frame.
                if (pins == 10)
                {
                    bonusThrowsLeft[frame] = 2;
                    MenuUISingleton.ShowStrike();
                    NextFrame();
                }
                else
                {
                    throwInFrame = 2;
                }
            }
            else if (throwInFrame == 2)
            {
                if (pinsThisFrame == 10)
                {
                    // Spare. The next throw also counts towards this frame.
                    bonusThrowsLeft[frame] = 1;
                    MenuUISingleton.ShowSpare();
                }

                NextFrame();
            }
            else if (throwInFrame == 3)
            {
                // They threw 3 balls. Frame is over.
                NextFrame();
            }

            Debug.Log($"[SCORING] Frame {frame + 1}, throw {throwInFrame - 1}. Total: {TotalScore()}");

        }

        private void NextFrame()
        {
            frame++;
            throwInFrame = 1;
            pinsThisFrame = 0;
        }

        public int TotalScore()
        {
            int total = 0;

            foreach (int score in frameScores)
            {
                total += score;
            }

            return total;
        }

        // True when this throw finished the frame, so the pins need resetting.
        public bool FrameIsOver()
        {
            return throwInFrame == 1;
        }

        // Ten frames is the whole game.
        public bool GameIsOver()
        {
            return frame > 9;
        }

        public void ResetScore()
        {
            for (int i = 0; i < frameScores.Length; i++)
            {
                frameScores[i] = 0;
                bonusThrowsLeft[i] = 0;
            }

            frame = 0;
            throwInFrame = 1;
            pinsThisFrame = 0;
        }
    }
}