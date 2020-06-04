using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] GameObject[] _items;
    [SerializeField] int[] _itemAmount;
    [SerializeField] float _droppingRange;

    EnemyHealth eHealth;
    Rigidbody itemRB;
    Vector3 randomForce;
    Vector2 droppingScale;

    float randomAngle;

    void Start()
    {
        eHealth = GetComponent<EnemyHealth>();

        if (eHealth != null) {
            eHealth.Drop += DropItems;
        }

        droppingScale = new Vector2((_droppingRange * (1.0f/3.0f)), (_droppingRange * (2.0f/3.0f)));
        //Debug.Log(droppingScale); //Checking Purposes
    }

    void DropItems()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            for (int j = 0; j < _itemAmount[i]; j++)
            {
                GameObject obj = Instantiate(_items[i], transform.position, Quaternion.Euler(0, 0, 0));
                itemRB = obj.GetComponent<Rigidbody>();

                if (itemRB != null)
                {
                    randomAngle = Random.Range(0, 360);
                    randomForce = new Vector3(Mathf.Cos(randomAngle * (Mathf.PI / 180)) * droppingScale.x,
                        droppingScale.y, Mathf.Sin(randomAngle * (Mathf.PI / 180)) * droppingScale.x);
                    itemRB.AddForce(randomForce * 10);
                }
            }
        }
    }
}
