using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simply moves the current game object
/// </summary>
public class MoveScript : MonoBehaviour
{
    // 1 - Designer variables

    /// <summary>
    /// Object speed
    /// </summary>
    public Vector2 speed = new Vector2(10, 10);

    /// <summary>
    /// Moving direction
    /// </summary>
    /// 

    // Can apply to IA, bullet, etc...
    public enum mvtType
    {
        Line,
        ZigZag,
    };

    public int range = 3;
    private float position;
    public Vector2 direction = new Vector2(-1, 0);
    public bool isEnnemy;
    public mvtType myMvt;

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    private void Start()
    {
        switch (myMvt)
        {
            case mvtType.Line:
                break;
            case mvtType.ZigZag:
                direction = new Vector2(-1, -1);
                break;
        }
    }

    void Update()
    {
        // 2 - Movement
        
        movement = new Vector2(
            speed.x * direction.x,
            speed.y * direction.y);
    }

    void FixedUpdate()
    {
        if (rigidbodyComponent == null)
        {
            rigidbodyComponent = GetComponent<Rigidbody2D>();
        }

        if(myMvt == mvtType.ZigZag)
        {
            position += movement.y;
            if (position >= range)
            {
                direction = new Vector2(-1, -1);
            }
            else if (position <= -range)
            {
                direction = new Vector2(-1,  1);
            }
        }
        


        // Apply movement to the rigidbody
        rigidbodyComponent.velocity = movement;
    }
}

