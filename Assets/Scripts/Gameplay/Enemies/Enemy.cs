using UnityEngine;

namespace TopDownShooter
{
    public class Enemy : Character
    {
        [SerializeField] public EnemyType Type = EnemyType.Undefined;
    }
}