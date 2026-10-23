using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace CCBowling
{

    public class FrameUI : MonoBehaviour
    {
        public TMP_Text[] throwTexts;      // Throw1 and Throw2
        public TMP_Text totalText;

        private bool frameLocked;

        public void UpdateScore(int throwNumber, int pins, int totalPins)
        {
            int box = throwNumber - 1;
            if (box < 0 || box >= throwTexts.Length || throwTexts[box].text.Length > 0) return;

            if (throwNumber == 1 && totalPins == 10)
            {
                throwTexts[box].text = "X";
            }
            else if (throwNumber == 2 && totalPins == 10)
            {
                throwTexts[box].text = "/";
            }
            else if (pins == 0)
            {
                throwTexts[box].text = "-";
            }
            else
            {
                throwTexts[box].text = pins.ToString();
            }
        }

        public void UpdateTotal(int total)
        {
            totalText.text = total.ToString();
        }

        public void ResetFrame()
        {
            if (totalText != null) totalText.text = "";

            foreach (TMP_Text tmTxt in throwTexts)
            {
                tmTxt.text = "";
            }
        }
    }

}