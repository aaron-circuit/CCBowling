using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

namespace CCBowling
{


    public class SceneChanger : MonoBehaviour
    {

        [SerializeField] private float delayTime = 1.5f; // Delay after pressing to allow button to react
        [SerializeField] private LoadSceneMode sceneMode = LoadSceneMode.Additive; // Allows you to control how the scene loads

        [Header("Element Visibility on Load")]
        [SerializeField] private GameObject[] objectsToHideOnLoad; // A list of GameObjects to hide when a scene loads
        [SerializeField] private GameObject[] objectsToShowOnLoad; // A list of GameObjects to show when a scene loads

        private string nextSceneName; // We will use this to store the called scene name

        public void ChangeScene(string sceneName)
        {
            nextSceneName = sceneName; // Set the name of the scene we want to load. This must match the name in the build settings.
            Invoke("MoveToScene", delayTime); // Invoke the load with a delay.
        }

        private void MoveToScene()
        {
            // Perform the scene load after the specified delay.
            SceneManager.LoadSceneAsync(nextSceneName, sceneMode);
        }


        private void Start()
        {
            SceneManager.sceneLoaded += HandleSceneLoaded; // This tells Unity to wait for this event and fire HandleSceneLoaded when it happens
        }

        private void HandleSceneLoaded(Scene loadedScene, LoadSceneMode loadedSceneMode)
        {
            // Go through each object to hide and set it inactive
            foreach(GameObject hideObject in objectsToHideOnLoad)
            {
                hideObject.SetActive(false);
            }

            // Go through each object and set it active 
            foreach (GameObject showObject in objectsToShowOnLoad)
            {
                showObject.SetActive(true);
            }
        }
    }
}