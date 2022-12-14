using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float moveSpeed;
    public float horizontalInput;
    public float verticalInput;
    public Transform orientation;
    GameObject YesWatched;
    GameObject StartBox;
    Vector3 moveDirection;
    string currentSceneName;
    public bool CanMove = false;

    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        rb.freezeRotation = true;
        rb = GetComponent<Rigidbody>();
        YesWatched = GameObject.Find("YesWatched");
        StartBox = GameObject.Find("StartBox");
        Scene currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;
        Debug.Log(currentSceneName);
        if (currentSceneName != "L1") { 
            CanMove = true;
        }
    }

    // Use FixedUpdate because Unity like it better for physics
    void FixedUpdate()
    
    {
        if (CanMove) {
            MyInput();
            MovePlayer();
        }
        
    }

    void Update() {
        if (currentSceneName == "L1" ) {
            EnablePlayerMovement();
        }
    }

    private void MyInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer() {
        // calculate player movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void EnablePlayerMovement() {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition); // this assumes the mouse is at the center of the screen
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "YesWatched") {
            if (Input.GetMouseButtonDown(0)) {
                CanMove = true;
                StartBox.SetActive(false);
            }
        }
    }

}
