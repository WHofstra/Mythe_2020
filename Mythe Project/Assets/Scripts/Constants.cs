using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public class InputString
    {
        public const string HORIZONTAL    = "Horizontal";
        public const string VERTICAL      = "Vertical";
        public const string MOUSE_X       = "Mouse X";
        public const string MOUSE_Y       = "Mouse Y";
        public const string RUN           = "Run";
        public const string WEAPON_SWITCH = "WeaponSwitch";
    }

    public class Layer
    {
        public const int PLAYER = 8;
        public const int HAZARD = 9;
        public const int SOIL   = 10;
        public const int ENEMY  = 11;
        public const int ATTACK = 12;
        public const int ROCK   = 13;
        //Layer 14 is 'Post Processing'
    }

    public class AnimatorTriggerString
    {
        public const string PUNCH       = "Punch";
        public const string HIT         = "Hit";
        public const string VINE_ATTACK = "Attack";
        public const string LIFT        = "Lift";
        public const string THROW_ROCK  = "Throw_Rocks";
        public const string RUNNING     = "Running";
    }

    public class ObjectName
    {
        public const string PLAYER        = "Player";
        public const string PLAYER_ATTACK = "AttackHitBox";
    }

    public class ComponentNames
    {
        public const string HALO    = "Halo";
        public const string ENABLED = "enabled";
    }

    public enum States
    {
        HOME,
        PLAYING,
        GAME_OVER
    }

    public enum SecondaryWeapon
    {
        VINES, ROCKS
    }
}
