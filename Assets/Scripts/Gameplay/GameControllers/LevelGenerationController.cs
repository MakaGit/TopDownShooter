using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TopDownShooter
{
    public class LevelGenerationController : MonoBehaviour
    {
        private void Start()
        {
            // Сгенерировать первый чанк
            var initialChunk = GenerateStartChunk(ChunkDockPointType.Undefined, false);

            // Выставить необходимые координаты
            initialChunk.transform.position = Vector3.zero;

            // Обставить со всех сторон другими чанками
            GenerateNeighbourChunks(initialChunk);
        }

        private Chunk GenerateStartChunk(ChunkDockPointType type, bool generateAdditionalElements = true)
        {
            var suitableChunks = type != ChunkDockPointType.Undefined
                ? SettingsManager.Instance.StartChunks.FindAll(c => c.DockPoints.Exists(p => p.Type == type))
                : SettingsManager.Instance.StartChunks;

            var prefab = suitableChunks[Random.Range(0, suitableChunks.Count)];

            var spawnedChunk = Instantiate(prefab);

            if (generateAdditionalElements)
            {
                SpawnEnemies(spawnedChunk);
            }

            spawnedChunk.EventPlayerEntered += OnPlayerEnteredChunk;
            return spawnedChunk;
        }

        private Chunk GenerateRandomChunk(ChunkDockPointType type, bool generateAdditionalElements = true)
        {
            var suitableChunks = type != ChunkDockPointType.Undefined
                ? SettingsManager.Instance.Chunks.FindAll(c => c.DockPoints.Exists(p => p.Type == type))
                : SettingsManager.Instance.Chunks;

            var prefab = suitableChunks[Random.Range(0, suitableChunks.Count)];

            var spawnedChunk = Instantiate(prefab);

            if (generateAdditionalElements)
            {
                SpawnEnemies(spawnedChunk);
            }

            spawnedChunk.EventPlayerEntered += OnPlayerEnteredChunk;
            return spawnedChunk;
        }

        private void GenerateNeighbourChunks(Chunk center)
        {
            // Обходить варианты направлений, смотреть, есть ли уже созданный чанк для этого направления,
            // если нет - создавать


            foreach (var centerDockPoint in center.DockPoints)
            {
                if (center.NeighbourChunks.ContainsKey(centerDockPoint))
                    continue;

                var suitableChunks = centerDockPoint.Type != ChunkDockPointType.Undefined
                    ? SettingsManager.Instance.Chunks.FindAll(c => c.DockPoints.Exists(p => p.Type == centerDockPoint.Type))
                    : SettingsManager.Instance.Chunks;

                while (suitableChunks.Count > 0)
                {
                    int randomizedIndex = Random.Range(0, suitableChunks.Count);
                    var prefab = suitableChunks[randomizedIndex];
                    suitableChunks.RemoveAt(randomizedIndex);

                    var neighbour = Instantiate(prefab);

                    var neighbourDockPoint = neighbour.DockPoints.Find(p => p.Type == centerDockPoint.Type);

                    var targetDockPointRotation =
                        Quaternion.LookRotation(-centerDockPoint.transform.forward, centerDockPoint.transform.up);

                    var rotationOffset = targetDockPointRotation * Quaternion.Inverse(neighbourDockPoint.transform.rotation);
                    neighbour.transform.rotation *= rotationOffset;

                    Vector3 offset = centerDockPoint.transform.position - neighbourDockPoint.transform.position;
                    neighbour.transform.position += offset;

                    if (IsEnoughSpaceForChunk(neighbour))
                    {
                        SpawnEnemies(neighbour);
                        neighbour.EventPlayerEntered += OnPlayerEnteredChunk;

                        SpawnGates(centerDockPoint.GateTransform, true);
                        center.NeighbourChunks[centerDockPoint] = neighbour;
                        neighbour.NeighbourChunks[neighbourDockPoint] = center;
                        break;
                    }
                    else
                    {
                        Destroy(neighbour.gameObject);
                        if(suitableChunks.Count == 0)
                        {
                            SpawnGates(centerDockPoint.GateTransform, false);
                        }
                    }
                }
            }
        }

        private bool IsEnoughSpaceForChunk(Chunk chunk)
        {
            foreach (var boundingCollider in chunk.BoundingColliders)
            {
                var colliderPosition = boundingCollider.transform.TransformPoint(boundingCollider.center);

                var intersectedColliders = Physics.OverlapBox(
                    colliderPosition,
                    boundingCollider.size / 2f,
                    boundingCollider.transform.rotation,
                    LayerMask.GetMask("Chunk"),
                    QueryTriggerInteraction.Collide);

                DrawBox(colliderPosition,
                    boundingCollider.size / 2f,
                    boundingCollider.transform.rotation,
                    Color.red,
                    5f);

                if (intersectedColliders.Length != 0)
                    return false;
            }

            return true;
        }

        private void SpawnEnemies(Chunk chunk)
        {
            foreach (var spawnPointSettings in chunk.EnemiesSpawnPoints)
            {
                if (Random.Range(0, 2) == 1)
                {
                    var settingsManager = SettingsManager.Instance;

                    var suitableEnemies = settingsManager.Enemies.FindAll(
                        el => (el.Type & spawnPointSettings.EnemyType) != 0);
                    var enemyPrefab = suitableEnemies[Random.Range(0, suitableEnemies.Count)];

                    var enemyObject = Instantiate(enemyPrefab,
                        spawnPointSettings.transform.position + Vector3.up * 0.01f,
                        spawnPointSettings.transform.rotation);
                    enemyObject.transform.SetParent(spawnPointSettings.transform);
                }
            }
        }

        private void SpawnGates(Transform transform, bool isOpen)
        {
            if (isOpen)
            {
                int randomizedIndex = Random.Range(0, SettingsManager.Instance.OpenFoceGates.Count);
                var gate = Instantiate(SettingsManager.Instance.OpenFoceGates[randomizedIndex], transform.position, transform.rotation, transform);
            }
            else
            {
                int randomizedIndex = Random.Range(0, SettingsManager.Instance.LockedFoceGates.Count);
                var gate = Instantiate(SettingsManager.Instance.LockedFoceGates[randomizedIndex], transform.position, transform.rotation, transform);
            }
        }

        private void OnPlayerEnteredChunk(Chunk chunk)
        {
            GenerateNeighbourChunks(chunk);
        }


        // DEBUG
        public static void DrawBox(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Color color, float duration)
        {
            DrawBox(new Box(origin, halfExtents, orientation), color, duration);
        }

        public static void DrawBox(Box box, Color color, float duration)
        {
            Debug.DrawLine(box.frontTopLeft, box.frontTopRight, color, duration);
            Debug.DrawLine(box.frontTopRight, box.frontBottomRight, color, duration);
            Debug.DrawLine(box.frontBottomRight, box.frontBottomLeft, color, duration);
            Debug.DrawLine(box.frontBottomLeft, box.frontTopLeft, color, duration);

            Debug.DrawLine(box.backTopLeft, box.backTopRight, color, duration);
            Debug.DrawLine(box.backTopRight, box.backBottomRight, color, duration);
            Debug.DrawLine(box.backBottomRight, box.backBottomLeft, color, duration);
            Debug.DrawLine(box.backBottomLeft, box.backTopLeft, color, duration);

            Debug.DrawLine(box.frontTopLeft, box.backTopLeft, color, duration);
            Debug.DrawLine(box.frontTopRight, box.backTopRight, color, duration);
            Debug.DrawLine(box.frontBottomRight, box.backBottomRight, color, duration);
            Debug.DrawLine(box.frontBottomLeft, box.backBottomLeft, color, duration);
        }

        public struct Box
        {
            public Vector3 localFrontTopLeft { get; private set; }
            public Vector3 localFrontTopRight { get; private set; }
            public Vector3 localFrontBottomLeft { get; private set; }
            public Vector3 localFrontBottomRight { get; private set; }

            public Vector3 localBackTopLeft
            {
                get { return -localFrontBottomRight; }
            }

            public Vector3 localBackTopRight
            {
                get { return -localFrontBottomLeft; }
            }

            public Vector3 localBackBottomLeft
            {
                get { return -localFrontTopRight; }
            }

            public Vector3 localBackBottomRight
            {
                get { return -localFrontTopLeft; }
            }

            public Vector3 frontTopLeft
            {
                get { return localFrontTopLeft + origin; }
            }

            public Vector3 frontTopRight
            {
                get { return localFrontTopRight + origin; }
            }

            public Vector3 frontBottomLeft
            {
                get { return localFrontBottomLeft + origin; }
            }

            public Vector3 frontBottomRight
            {
                get { return localFrontBottomRight + origin; }
            }

            public Vector3 backTopLeft
            {
                get { return localBackTopLeft + origin; }
            }

            public Vector3 backTopRight
            {
                get { return localBackTopRight + origin; }
            }

            public Vector3 backBottomLeft
            {
                get { return localBackBottomLeft + origin; }
            }

            public Vector3 backBottomRight
            {
                get { return localBackBottomRight + origin; }
            }

            public Vector3 origin { get; private set; }

            public Box(Vector3 origin, Vector3 halfExtents, Quaternion orientation) : this(origin, halfExtents)
            {
                Rotate(orientation);
            }

            public Box(Vector3 origin, Vector3 halfExtents)
            {
                this.localFrontTopLeft = new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z);
                this.localFrontTopRight = new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z);
                this.localFrontBottomLeft = new Vector3(-halfExtents.x, -halfExtents.y, -halfExtents.z);
                this.localFrontBottomRight = new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z);

                this.origin = origin;
            }

            public void Rotate(Quaternion orientation)
            {
                localFrontTopLeft = RotatePointAroundPivot(localFrontTopLeft, Vector3.zero, orientation);
                localFrontTopRight = RotatePointAroundPivot(localFrontTopRight, Vector3.zero, orientation);
                localFrontBottomLeft = RotatePointAroundPivot(localFrontBottomLeft, Vector3.zero, orientation);
                localFrontBottomRight = RotatePointAroundPivot(localFrontBottomRight, Vector3.zero, orientation);
            }

            static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
            {
                Vector3 direction = point - pivot;
                return pivot + rotation * direction;
            }
        }
    }
}