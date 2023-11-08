using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject gun;
    public AudioClip bulletEffect;
    public GameObject bulletPrefab;
    public GameObject crossHair;

    private GameManager gameManager;
    private CharacterController characterController;
    private float speed = 50f;
    private GameObject mainCamera;
    private int scopeUp;
    private int sensitivity;
    private float maxY = 60.0f;
    private float minY = -60.0f;
    private float smoothTime = 0.1f; // Adjust the smooth time to control the camera rotation speed
    private Vector2 rotation = Vector2.zero;
    private Vector2 currentRotation = Vector2.zero;
    private Vector2 targetRotation = Vector2.zero;
    private Vector2 rotationVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        sensitivity = MainManager.Instance.userDPI;
        characterController = GetComponent<CharacterController>();
        mainCamera = GameObject.Find("Main Camera");
        scopeUp = -1;
        InitializePosition();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button is held down
        if (Input.GetMouseButtonDown(0))
        {
            //Instantiate bulletPrefab
            Vector3 bulletPos = mainCamera.transform.position + mainCamera.transform.forward * 10f;
            GameObject bullet = Instantiate(bulletPrefab, bulletPos, mainCamera.transform.rotation);
            MainManager.Instance.numOfBullet++;
            // Add Rigidbody component to the bullet
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 800f, ForceMode.Impulse);

            // Play bullet sound effect
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.PlayOneShot(bulletEffect);
            Destroy(newAudioSource, bulletEffect.length);
        }

        if (Input.GetMouseButtonDown(1))
        {
            scopeUp *= -1;
        }


        // Rotate the gun based on mouse movement
        rotation.y += Input.GetAxis("Mouse X") * sensitivity;
        rotation.x -= Input.GetAxis("Mouse Y") * sensitivity;
        rotation.x = Mathf.Clamp(rotation.x, minY, maxY);
        currentRotation = Vector2.SmoothDamp(currentRotation, rotation, ref rotationVelocity, smoothTime);
        transform.localRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
        // Apply the new rotation to the camera
        mainCamera.transform.rotation = transform.rotation;
        gun.transform.rotation = transform.rotation;
        crossHair.transform.rotation = transform.rotation;


        // Position the camera behind the gun
        if (scopeUp == 1)
        {
            // camera position with scopeUp

            //Debug
            gun.transform.position = transform.position + transform.TransformDirection(new Vector3(0f, 3.98f, 4.37f));
            crossHair.SetActive(false);
        }
        else
        {
            // camera position without scopeUp
            mainCamera.transform.position = transform.position + transform.TransformDirection(new Vector3(0f, 7f, 0f));
            gun.transform.position = transform.position + transform.TransformDirection(new Vector3(5f, 3f, 7f));
            crossHair.transform.position = transform.position + transform.TransformDirection(new Vector3(0f, 7f, 69f));
            if (!crossHair.activeSelf)
            {
                crossHair.SetActive(true);
            }
        }

        /*
        if (characterController != null && mainCamera != null)
        {
            // Get the camera's forward direction without the y-component
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0f;
            cameraForward = cameraForward.normalized;

            // Get the horizontal and vertical input values
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calculate the direction to move the player
            Vector3 moveDirection = cameraForward * verticalInput + mainCamera.transform.right * horizontalInput;

            // Normalize the direction to prevent faster diagonal movement
            if (moveDirection.sqrMagnitude > 1)
            {
                moveDirection.Normalize();
            }

            // Move the player in the calculated direction
            characterController.Move(moveDirection * speed * Time.deltaTime);

            // Rotate the player to face the same direction as the camera
            if (moveDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(cameraForward);
            }
            
        }
        */
    }

    void InitializePosition() 
    {
        mainCamera.transform.position = transform.position + new Vector3(0f, 7f, 0f);

        gun.transform.position = transform.position + new Vector3(5f, 3f, 7f);

        crossHair.transform.position = transform.position + new Vector3(0f, 7f, 69f);
    }
}
