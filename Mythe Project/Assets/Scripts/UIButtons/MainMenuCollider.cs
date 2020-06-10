using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuCollider : MonoBehaviour
{
    [SerializeField] string _changeMenuTo;
    MainMenu menu;

    void Start()
    {
        menu = FindObjectOfType<MainMenu>();

        if (menu != null) {
            menu = menu.GetComponent<MainMenu>();
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag.Equals(Constants.Tag.MIDDLE))
        {
            //Debug.Log("End! Finito!");
            menu.ChangeMenuTo(_changeMenuTo);
        }
    }
}
