using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDTrigger : DialogueTrigger
{
    protected enum ColObj {
        PLAYER, PLAYER_PUNCH, VINE, ROCK
    }

    [SerializeField] protected ColObj _collisionObject;

    protected void OnCollisionEnter(Collision collision)
    {
        switch (_collisionObject)
        {
            case ColObj.ROCK:
                WithRocks(collision.collider);
                break;
        }
    }

    protected void OnTriggerEnter(Collider collider)
    {
        switch (_collisionObject)
        {
            case ColObj.PLAYER:
                WithPlayerObject(collider);
                break;
            case ColObj.VINE:
                WithVineAttack(collider);
                break;
            case ColObj.ROCK:
                WithRocks(collider);
                break;
        }
    }

    protected void OnTriggerStay(Collider collider)
    {
        switch (_collisionObject)
        {
            case ColObj.PLAYER_PUNCH:
                WithPlayerAttack(collider);
                break;
        }
    }

    protected void WithPlayerObject(Collider coll)
    {
        if (coll.gameObject.layer.Equals(Constants.Layer.PLAYER) &&
            coll.gameObject.name.Equals(Constants.ObjectName.PLAYER))
        {
            TriggerNextSceneTrigger();
        }
    }

    protected void WithPlayerAttack(Collider coll)
    {
        if (coll.gameObject.layer.Equals(Constants.Layer.PLAYER) &&
            coll.gameObject.name.Equals(Constants.ObjectName.PLAYER_ATTACK) && Input.GetMouseButtonDown(0))
        {
            TriggerNextSceneTrigger();
        }
    }

    protected void WithVineAttack(Collider coll)
    {
        if (coll.gameObject.layer.Equals(Constants.Layer.ATTACK)) {
            TriggerNextSceneTrigger();
        }
    }

    protected void WithRocks(Collider coll)
    {
        if (coll.gameObject.layer.Equals(Constants.Layer.ROCK)) {
            TriggerNextSceneTrigger();
        }
    }
}
