using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrenght = 1000f;
    [SerializeField] float rotationStrength = 100f;
    Rigidbody rb;
    [SerializeField] AudioSource audioSource;

    void OnEnable()
    {
       thrust.Enable(); 
       rotation.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyThrust();
        ApplyRotation();
    }

    private void ApplyRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput > 0)
        {
            ImplementRotation(-rotationStrength);
        }
        else if (rotationInput < 0)
        {
            ImplementRotation(rotationStrength);

        }
    }

    private void ImplementRotation(float rotationStrength)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationStrength * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }

    private void ApplyThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrenght * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
