using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CollisionHandler : MonoBehaviour
{
    Scene activeLevel;
    int activeLevelIndex;
    PlayerController pc;

    void Start() {
        activeLevel = SceneManager.GetActiveScene();
        activeLevelIndex = activeLevel.buildIndex;
        pc = GetComponent<PlayerController>();
    }

    void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag)
        {
            case "Finish":
                UpdateLevelIndex(activeLevel.buildIndex+1);
                LoadLevel();
                break;
            case "Friendly":
                Debug.Log("Friend");
                break;
            case "Fuel":
                Debug.Log("Fuel Added");
                break;
            default:
                UpdateLevelIndex(activeLevel.buildIndex+1);
                //LoadLevel(activeLevel.buildIndex);
                break;
        }
    }

    void UpdateLevelIndex(int levelIndex) {

    }

    void LoadLevel() {
        if (activeLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(activeLevelIndex);
            activeLevel = SceneManager.GetActiveScene();
        }
        else {
            SceneManager.LoadScene(0);
            activeLevel = SceneManager.GetActiveScene();
        }
        
    }
}
