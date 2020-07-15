using System;

namespace TopDownShooter
{
    public enum WeaponType
    {
        Undefined = 0,
        Pistol = 1,
        Rifle = 2,
        Throwable = 3
    }

    public static class WeaponTypeExtensions
    {
        public static string GetIdleAnimationTriggerName(this WeaponType _this)
        {
            if (_this == WeaponType.Undefined)
                throw new Exception("Trying to get trigger name for Undefined weapon type.");

            return $"{_this.ToString()}IdleTrigger"; // RifleIdleTrigger
        }

        public static string GetAttackAnimationTriggerName(this WeaponType _this)
        {
            if (_this == WeaponType.Undefined)
                throw new Exception("Trying to get trigger name for Undefined weapon type.");

            return $"{_this.ToString()}AttackingTrigger"; // RifleIdleTrigger
        }

        public static string GetAimingAnimationTriggerName(this WeaponType _this)
        {
            if (_this == WeaponType.Undefined)
                throw new Exception("Trying to get trigger name for Undefined weapon type.");

            return $"{_this.ToString()}IdleAimingTrigger"; // RifleIdleTrigger
        }

        public static string GetReloadingAnimationTriggerName(this WeaponType _this)
        {
            if (_this == WeaponType.Undefined)
                throw new Exception("Trying to get trigger name for Undefined weapon type.");

            return $"{_this.ToString()}ReloadingTrigger"; // RifleIdleTrigger
        }
    }
}
