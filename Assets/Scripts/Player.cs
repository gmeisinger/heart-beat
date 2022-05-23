using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public GameObject bulletPrefab;
    public MovingState moving;
    public DodgeState dodging;

    public override void Start ()
    {
        base.Start();

		states.Add("moving", new MovingState(this, SM));
		states.Add("dodging", new DodgeState(this, SM));
        SM.Initialize(states["moving"]);
    }

    public void fireBullet(Vector2 target)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Bullet>().direction = target;
    }

    public override void updateMovement(float h, float v)
	{
		updateMovement(h,v,1f);
	}

    public override void updateMovement(float h, float v, float speed=1.0f)
    {
        
        if (h != 0 && v != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            h *= moveLimiter;
            v *= moveLimiter;
        }

        float spd = runSpeed * speed;
        Vector2 velocity = new Vector2(h * spd, v * spd);
        //body.MovePosition(body.position + velocity);
        

        if(body.velocity != velocity)
        {
            body.velocity = velocity;
        }

        moveDirection = new Vector2(h, v);
    }
}
