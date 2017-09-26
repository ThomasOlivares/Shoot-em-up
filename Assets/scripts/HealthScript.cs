﻿using System.Collections;
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

    /// <summary>
    /// Enemy or player?
    /// </summary>
    public bool isEnemy = true;

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
}

