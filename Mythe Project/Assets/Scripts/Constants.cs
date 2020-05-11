using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public class InputString
    {
        public const string HORIZONTAL = "Horizontal";
        public const string VERTICAL   = "Vertical";
        public const string MOUSE_X    = "Mouse X";
        public const string MOUSE_Y    = "Mouse Y";
        public const string RUN        = "Run";
    }

    public class Layer
    {
        public const int PLAYER = 8;
        public const int HAZARD = 9;
        public const int SOIL   = 10;
        public const int ENEMY  = 11;
    }
}
