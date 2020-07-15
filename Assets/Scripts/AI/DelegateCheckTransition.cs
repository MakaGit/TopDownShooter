using System;
using UnityEngine.Assertions;

namespace TopDownShooter
{
    public class DelegateCheckTransition<TOwner, TTarget> : Transition<TOwner, TTarget>
        where TOwner : IState
        where TTarget : IState
    {
        private readonly Func<TOwner, TTarget, bool> _canTransitDelegate;

        public DelegateCheckTransition(
            Func<TOwner, TTarget, bool> canTransitDelegate,
            TOwner ownerState,
            TTarget targetState,
            int priority = 0) : base(ownerState, targetState, priority)
        {
            Assert.IsNotNull(canTransitDelegate);

            _canTransitDelegate = canTransitDelegate;
        }

        public override bool CanTransit()
        {
            return _canTransitDelegate.Invoke(OwnerState, TargetState);
        }
    }
}