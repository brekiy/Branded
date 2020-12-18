using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float acceleration = 5.0f;
    public float horizSens = 1.0f;
    public float vertSens = 1.0f;
    public float jumpHeight = 2.0f;
    public float dashCooldown = 1.0f;
    public float gravity = -9.81f;
    public bool invertY = false;
    public Transform cameraTransform;
    public CharacterController playerController;

    private Quaternion originalRotation;
    private Quaternion originalCameraRotation;
    private Vector3 velocity;
    private float rotationX = 0f;
    private float rotationY = 0f;
    private float maxAngleX = 360.0f;
    private float minAngleX = -360.0f;
    private float maxAngleY = 89.0f;
    private float minAngleY = -89.0f;
    private bool disabledInput = false;
    private int floorMask;
    private float dashTimer;


    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.localRotation;
        originalCameraRotation = cameraTransform.localRotation;
        floorMask = LayerMask.GetMask("Floor");
    }

    // Update is called once per frame
    void Update()
    {
        handleCamera();
        // handleMovement();
        charControllerMovement();
    }

    void handleCamera() {
        rotationX += horizSens * Input.GetAxis("Mouse X");
        float invert = invertY ? -1 : 1;
        rotationY += invert * vertSens * Input.GetAxis("Mouse Y");
        rotationX = ClampAngle(rotationX, minAngleX, maxAngleX);
        rotationY = ClampAngle(rotationY, minAngleY, maxAngleY);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
        transform.localRotation = originalRotation * xQuaternion;
        cameraTransform.localRotation = originalCameraRotation * yQuaternion;
    }

    // void handleMovement() {
    //     Vector2 playerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    //     playerInput = Vector2.ClampMagnitude(playerInput, 1f);
    //     Vector3 targetVelocity = cameraTransform.TransformDirection(playerInput.x, 0f, playerInput.y) * moveSpeed;
    //     float maxAccelChange = acceleration * Time.deltaTime;
    //     velocity.x = Mathf.MoveTowards(velocity.x, targetVelocity.x, maxAccelChange);
    //     velocity.z = Mathf.MoveTowards(velocity.z, targetVelocity.z, maxAccelChange);
    //     Vector3 displacement = velocity * Time.deltaTime;
    //     transform.localPosition += displacement;
    // }

    void charControllerMovement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        playerController.Move(move * moveSpeed * Time.deltaTime);
        if (IsGrounded() && Input.GetButtonDown("Jump")) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
        // Debug.Log("grounded? " + IsGrounded());
        if (dashTimer >= 0f) dashTimer -= Time.deltaTime;
    }

    // IEnumerator Dash() {
    //     SetDisabledControls(true);

    //     yield return new WaitForSecondsRealtime(dashCooldown);
    //     SetDisabledControls(false);
    //     yield return null;
    // }

    public static float ClampAngle(float angle, float min, float max) {
		if (angle < -360f) angle += 360f;
		if (angle > 360f) angle -= 360f;
		return Mathf.Clamp(angle, min, max);
	}

    // grounded check
    bool IsGrounded() {
        Ray ray = new Ray(playerController.bounds.center, -transform.up);
        float radius = playerController.bounds.extents.x - 0.05f;
        float ray_distance = playerController.bounds.extents.y + 0.05f;
        if (Physics.SphereCast(ray, radius, ray_distance, floorMask)) {
            // dashed = false;
            return true;
        } else return false;
    }

    public void SetDisabledInput(bool value) {
        disabledInput = value;
    }
}
