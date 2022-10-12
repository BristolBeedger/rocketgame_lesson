using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float thrustMod = 1000f;
    [SerializeField] float rotationalMod = 100f;
    [SerializeField] AudioClip engineClip;
    [SerializeField] ParticleSystem centerThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    Rigidbody rb;
    Transform tr;
    AudioSource thrustAudio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        thrustAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if(Input.GetKey(KeyCode.Space))
        {
            CenterThrusterLogic();
        }
        else {
            thrustAudio.Stop();
            centerThrustParticles.Stop();
        }  
    }

    void CenterThrusterLogic()
    {
        rb.AddRelativeForce(Vector3.up * thrustMod * Time.deltaTime);
        if (!thrustAudio.isPlaying)
        {
            thrustAudio.PlayOneShot(engineClip);
        }
        if (!centerThrustParticles.isPlaying)
            centerThrustParticles.Play();
    }

    void ProcessRotation() {
        if(Input.GetKey(KeyCode.D))
        {
            LeftThrusterLogic();
        }
        else if(Input.GetKey(KeyCode.A))
        {
            RightThrusterLogic();
        }
        else
        {
            EndRotationalLogic();
        }
    }

    private void EndRotationalLogic()
    {
        leftThrustParticles.Stop();
        rightThrustParticles.Stop();
    }

    private void RightThrusterLogic()
    {
        ApplyRotation(Vector3.forward);
        if (!rightThrustParticles.isPlaying) rightThrustParticles.Play();
    }

    private void LeftThrusterLogic()
    {
        ApplyRotation(Vector3.back);
        if (!leftThrustParticles.isPlaying) leftThrustParticles.Play();
    }

    void ApplyRotation(Vector3 direction)
    {
        rb.freezeRotation = true; //freeze rotation for manual control
        tr.Rotate(direction * rotationalMod * Time.deltaTime);
        rb.freezeRotation = false; //allowing physics to regain control
    }
}
