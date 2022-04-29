using System;
using System.Runtime.Serialization;

namespace Source.Refactored
{
    public struct AnimStates
    {
        public const string Idle = "Idle";
        public const string Run = "Run";
        public const string Jump = "jump";
        public const string Fall = "Fall";
        public const string Throw = "ThrowWeapon";
        public const string Death = "Death";
        public const string Hurt = "Hurt";
    }
}