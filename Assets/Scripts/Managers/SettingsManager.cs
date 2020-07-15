using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class SettingsManager : SingletonGameObject<SettingsManager>
    {
        [SerializeField] public List<Chunk> StartChunks = null;
        [SerializeField] public List<Chunk> Chunks = null;
        [SerializeField] public List<Enemy> Enemies = null;
        [SerializeField] public List<GameObject> LockedFoceGates = null;
        [SerializeField] public List<GameObject> OpenFoceGates = null;
    }
}
