using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float thrustMod = 1000f;
    [SerializeField] float rotationalMod = 100f;
    [SerializeField] AudioClip engineClip;

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
        if(Input.GetKey(KeyCode.Space)) {
            rb.AddRelativeForce(Vector3.up*thrustMod*Time.deltaTime);
            if(!thrustAudio.isPlaying)
                thrustAudio.PlayOneShot(engineClip);
        }
        else
            thrustAudio.Stop();
    }
    void ProcessRotation() {
        if(Input.GetKey(KeyCode.D))
            ApplyRotation(Vector3.back);
        else if(Input.GetKey(KeyCode.A))
            ApplyRotation(Vector3.forward);
    }

    void ApplyRotation(Vector3 direction)
    {
        rb.freezeRotation = true; //freeze rotation for manual control
        tr.Rotate(direction * rotationalMod * Time.deltaTime);
        rb.freezeRotation = false; //allowing physics to regain control
    }
}
