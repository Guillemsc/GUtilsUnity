using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using GUtilsUnity.Extensions;
using GUtilsUnity.Types;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;
using TaskExtensions = GUtilsUnity.Extensions.TaskExtensions;

namespace GUtilsUnity.Features.StartEndPositionParticleSystems
{
    public sealed class StartEndPositionParticleSystem : MonoBehaviour
    {
        [Header("References")]
        public Transform Prefab;

        [Header("General")]
        public bool ScaleByScreenSize = true;

        [Header("Initial Delay")]
        [Min(0f)] public float SpawnDelay;

        [Header("Spawn Movement Values")]
        [Range(-360, 360)] public float SpawnMovementMinAngle = 150;
        [Range(-360, 360)] public float SpawnMovementMaxAngle = 210;
        [Min(0)] public float SpawnMovementMinDistance = 3;
        [Min(0)] public float SpawnMovementMaxDistance = 5;
        [Min(0)] public float SpawnMovementDuration = 0.5f;
        public AnimationCurve SpawnMovementEasing = AnimationCurveExtensions.DefaultEaseInOut;

        [Header("Movement Values")]
        [Min(0)] public float MovementDuration = 1;
        public AnimationCurve MovementEasing = AnimationCurveExtensions.DefaultEaseInOut;

        [Header("Final Position Radius")]
        [Min(0)] public float FinalPositionMinRadius = 0f;
        [Min(0)] public float FinalPositionMaxRadius = 5f;

        [Header("Scale Values")]
        public float InitialScale;
        public float FinalScale = 1f;
        public AnimationCurve ScaleEasing = AnimationCurveExtensions.DefaultEaseInOut;

        readonly HashSet<Tween> _activeTweens = new();

        ObjectPool<Transform> _pool;

        public event StartEndParticleSystemDelegate OnObjectSpawned;
        public event StartEndParticleSystemDelegate OnObjectStarted;
        public event StartEndParticleSystemDelegate OnObjectDespawned;

        void Awake()
        {
            _pool = new ObjectPool<Transform>(
                createFunc: () => Instantiate(Prefab, transform),
                actionOnGet: view =>
                {
                    view.gameObject.SetActive(false);
                },
                actionOnRelease: view =>
                {
                    view.gameObject.SetActive(false);
                });
        }

        void OnDestroy()
        {
            foreach (Tween activeTween in _activeTweens)
            {
                activeTween.Kill();
            }

            _pool.Clear();
            _activeTweens.Clear();
        }

        public void PlayParticle(Vector2 startPosition, Vector2 targetPosition)
        {
            PlayParticle(startPosition, targetPosition, 0f, Nothing.Instance);
        }

        public void PlayParticle(Vector2 startPosition, Vector2 targetPosition, float delay)
        {
            PlayParticle(startPosition, targetPosition, delay, Nothing.Instance);
        }

        public void PlayParticle(Vector2 startPosition, Vector2 targetPosition, object userData)
        {
            PlayParticle(startPosition, targetPosition, 0f, userData);
        }

        public void PlayParticle(Vector2 startPosition, Vector2 targetPosition, float delay, object userData)
        {
            if (_pool == null)
            {
                return;
            }

            Transform element = _pool.Get();
            OnObjectSpawned?.Invoke(element, userData);

            Sequence animationSequence = DOTween.Sequence();
            _activeTweens.Add(animationSequence);

            void OnStart()
            {
                OnObjectStarted?.Invoke(element, userData);
                element.SetPositionXY(startPosition);
            }

            void OnComplete()
            {
                OnObjectDespawned?.Invoke(element, userData);
                _pool.Release(element);
                _activeTweens.Remove(animationSequence);
            }

            float screenSizeScale = ScaleByScreenSize ? Screen.height * 0.001f : 1f;

            float finalPositionRadius = Random.Range(FinalPositionMinRadius, FinalPositionMaxRadius);
            float finalPositionAngle = Random.Range(0, 360f);
            Vector2 finalPositionDirection = Vector2Extensions.DirectionFromAngleDegrees(finalPositionAngle);
            Vector2 finalPositionToAdd = finalPositionDirection * (finalPositionRadius * screenSizeScale);
            targetPosition += finalPositionToAdd;

            float angleBetween = startPosition.AngleDegreesBetween(targetPosition);

            float randomSpawnAngle = Random.Range(SpawnMovementMinAngle, SpawnMovementMaxAngle);
            float randomSpawnDistance = Random.Range(SpawnMovementMinDistance, SpawnMovementMaxDistance);

            Vector2 spawnTargetDirection = Vector2Extensions.DirectionFromAngleDegrees(randomSpawnAngle + angleBetween);
            Vector2 spawnTargetPosition = startPosition + (spawnTargetDirection * (randomSpawnDistance * screenSizeScale));

            Vector3 currentPosition = element.position;
            Vector3 initialPosition = new(spawnTargetPosition.x, spawnTargetPosition.y, currentPosition.z);
            Vector3 finalPosition = new(targetPosition.x, targetPosition.y, currentPosition.z);

            Sequence movementSequence = DOTween.Sequence();
            movementSequence.Append(element.DOMove(initialPosition, SpawnMovementDuration).SetEase(SpawnMovementEasing));
            movementSequence.Append(element.DOMove(finalPosition, MovementDuration).SetEase(MovementEasing));

            Sequence scaleSequence = DOTween.Sequence();
            float scaleDuration = movementSequence.Duration();
            scaleSequence.Append(element.DOScale(InitialScale, 0f));
            scaleSequence.Append(element.DOScale(FinalScale, scaleDuration).SetEase(ScaleEasing));

            float totalDelay = SpawnDelay + delay;

            animationSequence.AppendInterval(totalDelay);
            animationSequence.AppendCallback(() => element.gameObject.SetActive(true));
            animationSequence.AppendCallback(OnStart);
            animationSequence.Append(movementSequence);
            animationSequence.Join(scaleSequence);

            animationSequence.onComplete += OnComplete;

            animationSequence.Play();
        }

        public Task AwaitUntilAllDespawned(CancellationToken cancellationToken)
        {
            return TaskExtensions.AwaitUntil(() => _activeTweens.Count == 0, cancellationToken);
        }
    }
}
