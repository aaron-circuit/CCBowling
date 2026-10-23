using Unity.VisualScripting;
using UnityEngine;

namespace CCBowling
{

    public class MenuUISingleton : MonoBehaviour
    {
        public ScoreUIManager scoreUI;

        public GameObject strikeUIObject;
        public GameObject spareUIObject;

        private static MenuUISingleton menuUI;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (MenuUISingleton.menuUI != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                MenuUISingleton.menuUI = this;
            }
        }

        public static ScoreUIManager GetScoreUI()
        {
            return menuUI.scoreUI;
        }

        public static void ShowStrike()
        {
            if (menuUI.strikeUIObject != null)
            {
                menuUI.strikeUIObject.SetActive(true);
            }
        }
        public static void ShowSpare()
        {
            if (menuUI.spareUIObject != null)
            {
                menuUI.spareUIObject.SetActive(true);
            }
        }


    }
}