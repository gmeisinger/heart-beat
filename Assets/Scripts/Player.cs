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
}
