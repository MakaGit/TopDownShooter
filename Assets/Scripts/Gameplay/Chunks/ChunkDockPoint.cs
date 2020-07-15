using UnityEngine;

namespace TopDownShooter
{
    public class ChunkDockPoint : MonoBehaviour
    {
        [SerializeField] public ChunkDockPointType Type = ChunkDockPointType.Undefined;
        [SerializeField] public Transform GateTransform = null;
    }
}