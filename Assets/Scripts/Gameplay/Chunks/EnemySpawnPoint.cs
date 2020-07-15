using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] public EnemyType EnemyType = EnemyType.Undefined;
    }
}
