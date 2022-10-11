using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag)
        {
            case "Finish":
                Debug.Log("You have reached the Finish!"); 
                break;
            case "Friendly":
                Debug.Log("This is a friendly object!");
                break;
            case "Fuel":
                Debug.Log("Fuel Added");
                break;
            default:
                ReloadLevel(SceneManager.GetActiveScene());
                break;
        }
    }

    void ReloadLevel(Scene activeLevel) {
        SceneManager.LoadScene(activeLevel.name);
    }
}
