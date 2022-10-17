using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1.25f;
    [SerializeField] AudioClip crashClip;
    [SerializeField] AudioClip finishClip;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem finishParticles;

    Scene activeLevel;
    int activeLevelIndex;
    PlayerController pc;
    AudioSource collisionAudio;

    bool hasCollided = false;
    bool isCollisionDisabled = false;

    void Start() {
        activeLevel = SceneManager.GetActiveScene();
        activeLevelIndex = activeLevel.buildIndex;
        pc = GetComponent<PlayerController>();
        collisionAudio = GetComponent<AudioSource>();
    }

    void Update() {
        NextLevelLoad_DebugTool();
        DisableCollisions_DebugTool();
    }

    private void DisableCollisions_DebugTool()
    {
        if(Input.GetKeyDown(KeyCode.C)) {
            isCollisionDisabled = !isCollisionDisabled;
        }
    }

    private void NextLevelLoad_DebugTool()
    {
        if(Input.GetKeyDown(KeyCode.L)) {
            UpdateLevelIndex(activeLevel.buildIndex+1);
            LoadLevel();
        }
    }

    void OnCollisionEnter(Collision other) {
        if(!hasCollided && !isCollisionDisabled) {
            switch (other.gameObject.tag)
            {
            case "Finish":
                UpdateLevelIndex(activeLevel.buildIndex+1);
                StartFinishSequence();
                break;
            case "Friendly":
                break;
            default:
                UpdateLevelIndex(activeLevel.buildIndex);
                StartCrashSequence();
                break;
            }
        }
    }

    void StartFinishSequence()
    {
        hasCollided = true;
        collisionAudio.Stop();
        collisionAudio.PlayOneShot(finishClip);
        finishParticles.Play();
        pc.enabled = false;
        Invoke("LoadLevel", delay);
    }

    void StartCrashSequence() {
        /* TODO
        ** add particle effect    
        */
        hasCollided = true;
        collisionAudio.Stop();
        collisionAudio.PlayOneShot(crashClip);
        crashParticles.Play();
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
