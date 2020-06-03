using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Collections;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject tree;
    [SerializeField]
    Texture2D image;
    [SerializeField]
    int width = 30, height = 30;
    int size = 100;
    int zpos;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        for(int z = 0; z < width; z++)
        {
            zpos = z;
            for(int x = 0; x < height; x++)
            {
                GameObject obj = Instantiate(tree);
                obj.transform.position = new Vector3((x + Random.Range(-0.3f, 0.3f)) * (size / width), 0, (zpos + Random.Range(-0.3f, 0.3f)) * (size / height));
                
                if (image.GetPixel(Mathf.RoundToInt(obj.transform.position.x) / (size / width) * (1024 / width), Mathf.RoundToInt(obj.transform.position.z) / (size / width) * (1024 / height)).r == 0)
                {
                    Debug.Log("Bey bey");
                    Destroy(obj);
                }
            }
        }
    }
}
