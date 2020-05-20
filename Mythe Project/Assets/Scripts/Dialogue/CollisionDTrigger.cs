using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDTrigger : DialogueTrigger
{
    protected void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer.Equals(Constants.Layer.PLAYER) &&
            collider.gameObject.name.Equals(Constants.ObjectName.PLAYER))
        {
            ChangeScene(_dialogue);
        }
    }
}
