using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerScript : MonoBehaviour
{
    /// <summary>
    /// 1 - The speed of the ship
    /// </summary>
    public int maxSpeed = 5;

    private bool swap = false;

    // 2 - Store the movement and the component

    private WeaponScript.typeBullet myBullet;
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    void Start()
    {
        // We start with the simple shot
        myBullet = WeaponScript.typeBullet.Simple;
        WeaponScript weapon = this.transform.Find("WeaponSpiral").GetComponent<WeaponScript>();
        weapon.shotPrefab.rotation = Quaternion.identity;
    }

    void Update()
    {
        // 3 - Retrieve axis information
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        
        // 4 - Movement per direction
        movement = new Vector2(
          maxSpeed * System.Math.Sign(inputX),
          maxSpeed * System.Math.Sign(inputY));

        // 5 - Checking weapon changement
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // We simply switch to the next type of weapon
            int typeInt = (int)(myBullet);
            myBullet = (WeaponScript.typeBullet)((typeInt + 1) % 3);
            swap = true;
        }

        // 6 - Shooting
        bool shoot = false;
        if (Input.GetKey(KeyCode.Space))
        {
            shoot = true;
        }

        if (shoot)
        {
            WeaponScript[] weapons = GetComponentsInChildren<WeaponScript>();
            foreach (WeaponScript weapon in weapons)
            {
                if (weapon != null)
                {
                    if (myBullet == weapon.myType)
                    {
                        // false because the player is not an enemy
                        if (swap && myBullet == WeaponScript.typeBullet.Spiral)
                        {
                            Debug.Log("hi");
                            this.transform.Find("WeaponSpiral").rotation = Quaternion.identity;
                            weapon.shotPrefab.rotation = Quaternion.identity;
                            swap = false;
                        }
                        
                        weapon.Attack(false, myBullet);
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        // 5 - Get the component and store the reference
        if (rigidbodyComponent == null)
        {
            rigidbodyComponent = GetComponent<Rigidbody2D>();
        }

        // 6 - Move the game object
        rigidbodyComponent.velocity = movement;
    }
}