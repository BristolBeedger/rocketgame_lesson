using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1.25f;
    [SerializeField] AudioClip crashClip;
    [SerializeField] AudioClip finishClip;
    Scene activeLevel;
    int activeLevelIndex;
    PlayerController pc;
    AudioSource collisionAudio;

    void Start() {
        activeLevel = SceneManager.GetActiveScene();
        activeLevelIndex = activeLevel.buildIndex;
        pc = GetComponent<PlayerController>();
        collisionAudio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag)
        {
            case "Finish":
                UpdateLevelIndex(activeLevel.buildIndex+1);
                StartFinishSequence();
                break;
            case "Friendly":
                Debug.Log("Friend");
                break;
            default:
                UpdateLevelIndex(activeLevel.buildIndex);
                StartCrashSequence();
                break;
        }
    }

    void StartFinishSequence()
    {
        collisionAudio.PlayOneShot(finishClip);
        pc.enabled = false;
        Invoke("LoadLevel", delay);
    }

    void StartCrashSequence() {
        /* TODO
        ** add crash sfx
        ** add particle effect    
        */
        collisionAudio.PlayOneShot(crashClip);
        pc.enabled = false;
        Invoke("LoadLevel", delay);
    }

    void UpdateLevelIndex(int levelIndex) {
        activeLevelIndex = levelIndex;
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
