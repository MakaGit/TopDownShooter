using UnityEngine;

namespace TopDownShooter
{
    public class CharacterAnimator : MonoBehaviour
    {
        public Transform LeftHandIKTarget { get; set; }

        [SerializeField] private Animator _animator = null;

        public void Die()
        {
            _animator.SetTrigger("DeathTrigger");
            _animator.SetLayerWeight(1, 0f);
        }

        public void SetIdleWeaponAnimation(string triggerName)
        {
            _animator.SetTrigger(triggerName);
        }

        public void SetAttackWeaponAnimation(string triggerName)
        {
            _animator.SetTrigger(triggerName);
        }

        public void SetAimingWeaponAnimation(string triggerName)
        {
            _animator.SetTrigger(triggerName);
        }

        public void SetReloadingWeaponAnimation(string triggerName)
        {
            _animator.SetTrigger(triggerName);
        }

        public void SetLocomotionRifle()
        {
            _animator.SetTrigger("LocomotionRifle");
            _animator.SetLayerWeight(1, 1.0f);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (LeftHandIKTarget == null)
            {
                return;
            }

            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandIKTarget.position);
        }
    }
}
