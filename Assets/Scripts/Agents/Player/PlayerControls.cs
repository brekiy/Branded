using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    public enum Weapon {
        SWORD,
        CROSSBOW,
        CANNON
    };

    public Transform cameraTransform;
    public CharacterController playerController;
    public GameObject activeWeaponInstance;
    public Transform weaponHandAnchor;
    public Transform firingPosition;
    public GameObject crossbowBolt;
    public GameObject cannonShot;
    public GameObject swordInstance;
    public GameObject crossbowInstance;

    public float moveSpeed = 5.0f;
    public float acceleration = 5.0f;
    public float horizSens = 1.0f;
    public float vertSens = 1.0f;
    public float jumpHeight = 2.0f;
    public float dashCooldown = 1.0f;
    public float gravity = -9.81f;
    public bool invertY = false;

    public float crossbowVelocity = 25;
    public int swordDamage = 5;
    public int crossbowDamage = 1;
    public int cannonDamage = 20;

    private Quaternion originalRotation;
    private Quaternion originalCameraRotation;
    private Vector3 velocity;
    private Vector3 lastPos;
    private Animator anim;
    private Weapon activeWeapon;
    private float rotationX = 0f;
    private float rotationY = 0f;
    private float maxAngleX = 360.0f;
    private float minAngleX = -360.0f;
    private float maxAngleY = 89.0f;
    private float minAngleY = -89.0f;
    private float dashTimer;
    private int floorMask;
    private int kills = 0;
    private int crossbowAmmo = 100;
    private int cannonAmmo = 1;
    private bool disabledInput = false;
    private bool crossbowUpgraded = false;
    private bool cannonUpgraded = false;
    private bool swordUpgraded = false;


    // Start is called before the first frame update
    void Start() {
        originalRotation = transform.localRotation;
        originalCameraRotation = cameraTransform.localRotation;
        floorMask = LayerMask.GetMask("Floor");
        setActiveWeapon(Weapon.CROSSBOW);
    }

    // Update is called once per frame
    void Update() {
        HandleCamera();
        ControllerMovement();
        WeaponHandling();
        lastPos = firingPosition.position;
    }

    void FixedUpdate() {
        // lastPos = firingPosition.position;
    }

    void HandleCamera() {
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

    void ControllerMovement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        playerController.Move(move * moveSpeed * Time.deltaTime);
        if (IsGrounded() && Input.GetButtonDown("Jump")) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (!IsGrounded()) {
            velocity.y += gravity * Time.deltaTime;
        }
        playerController.Move(velocity * Time.deltaTime);
        if (dashTimer >= 0f) dashTimer -= Time.deltaTime;
    }

    public static float ClampAngle(float angle, float min, float max) {
		if (angle < -360f) angle += 360f;
		if (angle > 360f) angle -= 360f;
		return Mathf.Clamp(angle, min, max);
	}

    void WeaponHandling() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            switch(activeWeapon) {
                case Weapon.SWORD:
                    AttackSword();
                    break;
                case Weapon.CROSSBOW:
                    AttackCrossbow();
                    break;
                case Weapon.CANNON:
                    AttackCannon();
                    break;
            }
        }
        handleWeaponSwitch();
    }

    void handleWeaponSwitch() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            setActiveWeapon(Weapon.SWORD);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            setActiveWeapon(Weapon.CROSSBOW);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            setActiveWeapon(Weapon.CANNON);
        }
    }

    void setActiveWeapon(Weapon newActiveWeapon) {
        if (this.activeWeapon != newActiveWeapon || this.activeWeapon == null) {
            Debug.Log("Setting new active weapon " + newActiveWeapon);
            if (activeWeaponInstance != null) Destroy(activeWeaponInstance);
            this.activeWeapon = newActiveWeapon;
            switch(newActiveWeapon) {
                case Weapon.SWORD:
                    activeWeaponInstance = GameObject.Instantiate(swordInstance);
                    break;
                case Weapon.CROSSBOW:
                    activeWeaponInstance = GameObject.Instantiate(crossbowInstance);
                    break;
                case Weapon.CANNON:
                    activeWeaponInstance = GameObject.Instantiate(crossbowInstance);
                    break;
            }
            activeWeaponInstance.transform.position = weaponHandAnchor.position;
            activeWeaponInstance.transform.forward = weaponHandAnchor.forward;
            activeWeaponInstance.transform.parent = weaponHandAnchor;
            anim = activeWeaponInstance.GetComponent<Animator>();
        }
    }

    void AttackSword() {
        
    }

    void AttackCrossbow() {
        if (crossbowAmmo > 0) {
            ShootProjectile(crossbowBolt, crossbowVelocity, crossbowDamage);
            UseAmmo(Weapon.CROSSBOW, 1);
            anim.SetTrigger("Fire");
        } else {
            Debug.Log("click no xbow ammo");
        }
    }

    void AttackCannon() {
        if (cannonAmmo > 0) {
            ShootProjectile(crossbowBolt, crossbowVelocity, crossbowDamage);
            UseAmmo(Weapon.CANNON, 1);
        }
    }

    void ShootProjectile(GameObject projectile, float speed, int damage) {
        GameObject projectileInstance = GameObject.Instantiate(projectile, firingPosition.position, firingPosition.rotation);
        projectileInstance.GetComponent<Projectile>().Setup(speed, damage);
    }

    void UpgradeWeapon(Weapon weapon) {
        switch (weapon) {
            case Weapon.SWORD:
                swordUpgraded = true;
                break;
            case Weapon.CROSSBOW:
                crossbowUpgraded = true;
                break;
            case Weapon.CANNON:
                cannonUpgraded = true;
                break;
        }
    }

    public void UseAmmo(Weapon weapon, int count) {
        switch (weapon) {
            case Weapon.CROSSBOW:
                if (!crossbowUpgraded) crossbowAmmo -= count;
                break;
            case Weapon.CANNON:
                cannonAmmo -= count;
                break;
        }
    }

    public void AddKill() {
        kills += 1;
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
