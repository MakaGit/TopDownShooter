using System;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class Chunk : MonoBehaviour
    {
        public event Action<Chunk> EventPlayerEntered;

        public readonly Dictionary<ChunkDockPoint, Chunk> NeighbourChunks = new Dictionary<ChunkDockPoint, Chunk>();

        public List<ChunkDockPoint> DockPoints { get { return _dockPoints; } }
        public List<EnemySpawnPoint> EnemiesSpawnPoints { get { return _enemiesSpawnPoints; } }
        public List<BoxCollider> BoundingColliders { get { return _boundingColliders; } }

        [SerializeField] private List<ChunkDockPoint> _dockPoints = null;
        [SerializeField] private List<EnemySpawnPoint> _enemiesSpawnPoints = null;

        [SerializeField] private List<BoxCollider> _boundingColliders = null;

        private void OnTriggerEnter(Collider other)
        {
            EventPlayerEntered?.Invoke(this);
        }
    }
}
