using UnityEngine;

namespace CCBowling
{

    public class ScoreUIManager : MonoBehaviour
    {

        public FrameUI[] frameUIElements;
        public GameObject framePanel;

        [SerializeField] private float transitionTime = 1.25f; // Time to move on or off screen
        [SerializeField] private Vector2 offScreenPosition = new Vector2(100, 100);
        [SerializeField] private Vector2 onScreenPosition = new Vector2(100, -10);

        private float transitionStartTime;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ResetFrames();
        }

        private void Update()
        {
            if (Vector2.Distance(offScreenPosition,onScreenPosition) > 0.01f)
            {
                // Calculate the lerp time
                float lerpTime = Mathf.Clamp( ((Time.time - transitionStartTime) / transitionTime),0, 1);

                // Only move the scoreboard if it is off screen
                framePanel.transform.localPosition = Vector2.Lerp(offScreenPosition, onScreenPosition, lerpTime);
            }
        }

        private void OnDisable()
        {
            // Move the scoreboard off screen when disabled
            framePanel.transform.localPosition = offScreenPosition;
        }

        private void OnEnable()
        {
            // Set the starting time of the transiiton
            transitionStartTime = Time.time;
        }

        // This is called for a single throw on a frame
        public void SetFrameScore(int frameNum, int throwNum, int pins, int totalPins)
        {
            frameUIElements[frameNum].UpdateScore(throwNum, pins, totalPins);
        }

        // This is called to show the score for that frame
        public void SetFrameTotal(int frameNum, int scoreTotal)
        {
            frameUIElements[frameNum].UpdateTotal(scoreTotal);
        }

        // This is called to reset all the frames
        public void ResetFrames()
        {
            if (framePanel == null)
            {
                Debug.LogWarning("Frame panel is not set up.");
                return;
            }

            // Go through each frame to reset it
            for (int i = 0; i < frameUIElements.Length; i++)
            {
                frameUIElements[i].ResetFrame();
            }

        }
    }

}