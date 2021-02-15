using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour {
    
    public enum Weapon {
        SWORD,
        CROSSBOW,
        CANNON
    };

    public GameObject activeWeaponInstance;
    public Transform weaponHandAnchor;
    public Transform firingPosition;
    public GameObject crossbowBolt;
    public GameObject cannonShot;
    public GameObject swordInstance;
    public GameObject crossbowInstance;

    public float crossbowVelocity = 10;
    public int swordDamage = 5;
    public int crossbowDamage = 1;
    public int cannonDamage = 20;

    private Weapon activeWeapon;
    private int kills = 0;
    private int crossbowAmmo = 100;
    private int cannonAmmo = 1;
    private bool crossbowUpgraded = false;
    private bool cannonUpgraded = false;
    private bool swordUpgraded = false;

    // Start is called before the first frame update
    void Start() {
        setActiveWeapon(Weapon.CROSSBOW);
    }

    // Update is called once per frame
    void Update() {
        // TODO: Input Manager stuff
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

    void handleWeaponAttack() {
        
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
        Debug.Log(this.activeWeapon);
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
        }
    }

    void AttackSword() {
        
    }

    void AttackCrossbow() {
        if (crossbowAmmo > 0) {
            ShootProjectile(crossbowBolt, crossbowVelocity, crossbowDamage);
            UseAmmo(Weapon.CROSSBOW, 1);
        }
    }

    void AttackCannon() {
        if (cannonAmmo > 0) {
            ShootProjectile(crossbowBolt, crossbowVelocity, crossbowDamage);
            UseAmmo(Weapon.CANNON, 1);
        }
    }

    void ShootProjectile(GameObject projectile, float shootVelocity, int damage) {
        GameObject projectileInstance = GameObject.Instantiate(projectile);
        Projectile projectileAttributes = projectileInstance.GetComponent<Projectile>();
        projectileAttributes.speed = shootVelocity;
        projectileAttributes.damage = damage;
        projectileInstance.transform.position = firingPosition.position;
        projectileInstance.transform.forward = firingPosition.forward;
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
}
