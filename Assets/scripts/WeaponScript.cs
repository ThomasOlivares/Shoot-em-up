using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class WeaponScript : MonoBehaviour
{
    //--------------------------------
    // 1 - Designer variables
    //--------------------------------

    /// <summary>
    /// Projectile prefab for shooting
    /// </summary>
    /// 
    public enum typeBullet
    {
        Simple,
        Diagonal,
        Spiral
    };

    public Transform shotPrefab;
    public typeBullet myType;

    /// <summary>
    /// Cooldown in seconds between two shots
    /// </summary>
    public float shootingRate = 0.25f;

    //--------------------------------
    // 2 - Cooldown
    //--------------------------------

    private float shootCooldown;

    void Start()
    {
        shootCooldown = 0f;
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    //--------------------------------
    // 3 - Shooting from another script
    //--------------------------------

    /// <summary>
    /// Create a new projectile if possible
    /// </summary>
    public void Attack(bool isEnemy, typeBullet myBullet)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            switch (myBullet)
            {
                case typeBullet.Simple:
                    AttackBullet(isEnemy);
                    break;
                case typeBullet.Diagonal:
                    AttackBullet(isEnemy);
                    break;
                case typeBullet.Spiral:
                    AttackBullet(isEnemy);
                    this.transform.Rotate(new Vector3(0, 0, 15));
                    shotPrefab.Rotate(new Vector3(0, 0, 15));
                    break;
            }
        }
    }

    public void AttackBullet(bool isEnemy)
    {
        // Create a new shot
        Transform shotTransform = Instantiate(shotPrefab) as Transform;

        // Assign position
        shotTransform.position = transform.position;

        // The is enemy property
        ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            shot.isEnemyShot = isEnemy;
        }

        // Make the weapon shot always towards it
        MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();

        if (move != null)
        {
            if (shot.isEnemyShot)
                move.direction = -this.transform.right; // towards in 2D space is the right of the sprite
            else
            {
                move.direction = this.transform.right;
                EnergyScript energy = GetComponentInParent<EnergyScript>();
                energy.Decrement(1);
            }
        }
    }

    public void AttackSpiralBullet(bool isEnemy)
    {

    }

    /// <summary>
    /// Is the weapon ready to create a new projectile?
    /// </summary>
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}

