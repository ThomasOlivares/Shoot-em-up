using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle hitpoints and damages
/// </summary>
public class HealthScript : MonoBehaviour
{
    /// <summary>
    /// Total hitpoints
    /// </summary>
    public int hp = 1;
    public Transform maxHealthBar;
    public Transform healthBar;

    private int maxHp;
    private Transform maxHealthTransform;
    private Transform healthTransform;

    /// <summary>
    /// Enemy or player?
    /// </summary>
    public bool isEnemy = true;

    public void Start()
    {
        // Create a new shot
        maxHealthTransform = Instantiate(maxHealthBar) as Transform;
        healthTransform = Instantiate(healthBar) as Transform;

        // Assign position
        maxHealthTransform.position = transform.position + new Vector3(0, -1, 0);
        healthTransform.position = transform.position + new Vector3(0, -1, -1);

        maxHealthTransform.localScale = new Vector3(0.5f, 0.3f, 0);
        healthTransform.localScale = new Vector3(0.5f, 0.3f, 0);

        //Save maximum hp
        maxHp = hp;
    }

    public void Update()
    {
        // Assign position
        maxHealthTransform.position = transform.position + new Vector3(0, -1, 0);
        healthTransform.position = maxHealthTransform.position + new Vector3(0, 0, -1);

        float hpPercent = (float)hp / (float)maxHp;
        healthTransform.localScale = new Vector3(hpPercent*0.5f, 0.3f, 0);
    }

    /// <summary>
    /// Inflicts damage and check if the object should be destroyed
    /// </summary>
    /// <param name="damageCount"></param>
    public void Damage(int damageCount)
    {
        hp -= damageCount;

        if (hp <= 0)
        {
            // Dead!
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        print("collide");
        // Is this a shot?
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
        PlayerScript player = otherCollider.gameObject.GetComponent<PlayerScript>();
        EnemyScript enemy = otherCollider.gameObject.GetComponent<EnemyScript>();
        Debug.Log(shot + " " + player + " " + enemy);
        if (shot != null)
        {
            
            // Avoid friendly fire
            if (shot.isEnemyShot != isEnemy)
            {
                print("shot");
                Damage(shot.damage);

                // Destroy the shot
                Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
            }
        }
        
        // Is this a player ?
        if (player != null)
        {
            print("player");
            Destroy(this.gameObject);
        }

        // Is this an ennemy ?
        if (enemy != null)
        {
            print("enemy");
            Damage(enemy.damage);

            // Destroy the enemy
            Destroy(enemy.gameObject);
        }
        
    }

    private void OnDestroy()
    {
        Destroy(maxHealthTransform.gameObject);
        Destroy(healthTransform.gameObject);
    }
}

