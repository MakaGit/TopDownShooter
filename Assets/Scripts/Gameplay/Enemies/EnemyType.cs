using System;

namespace TopDownShooter
{
    [Flags]
    public enum EnemyType
    {
        Undefined = 0,

        Weak = 1 << 1,
        Fast = 1 << 2,
        Strong = 1 << 3
    }
}
