using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUITextTo : MonoBehaviour
{
    [SerializeField] Vector2 _beginPosition;
    [SerializeField] Vector2 _velocity;

    MainMenu menu;

    RectTransform rTransform;
    Vector3 getPosition;
    Vector2 currentVelocity;

    void Awake()
    {
        rTransform = GetComponent<RectTransform>();
        menu = FindObjectOfType<MainMenu>();

        if (menu != null) {
            menu = menu.GetComponent<MainMenu>();
            menu.ResetCredits += SetBeginPosition;
        }

        SetBeginPosition();
    }

    void Update()
    {
        currentVelocity = _velocity + (_velocity * 20 * Input.GetAxis(Constants.InputString.SUBMIT));
        getPosition = rTransform.anchoredPosition;
        getPosition.x += currentVelocity.x * Time.deltaTime;
        getPosition.y += currentVelocity.y * Time.deltaTime;
        rTransform.anchoredPosition = getPosition;
    }

    void SetBeginPosition()
    {
        rTransform.anchoredPosition = new Vector3(_beginPosition.x, _beginPosition.y, 0);
    }
}
