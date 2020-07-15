using System.Linq;
using System.Collections;
using UnityEngine;

namespace TopDownShooter
{
    public class Weapon : InventoryItem
    {
        [SerializeField] public WeaponType Type = WeaponType.Undefined;
        [SerializeField] public Transform LeftHandIKTarget = null;

        [SerializeField] private Transform _shootingPoint = null;

        [SerializeField] private AudioClip _shootingAudio = null;
        [SerializeField] private AudioClip _reloadingAudio = null;

        [SerializeField] private ParticleSystem _muzzleFlashParticleSystem = null;
        [SerializeField] private ParticleSystem _tracerParticleSystem = null;

        [SerializeField] private int MaxAmmo = 0;
        [SerializeField] private string ammoTag = null;

        private AudioSource _audioSource = null;
        [SerializeField] private AudioSource _audioSourceSh = null;
        private int _currentAmmo = 0;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _currentAmmo = MaxAmmo;
        }
        public override void Drop(Vector3 dropPosition)
        {
            if (_visualModel.gameObject.activeSelf) // If weapon is in hands now
            {
                _visualModel.SetParent(transform);
            }

            base.Drop(dropPosition);
        }

        public override void Apply(Character character)
        {
            _visualModel.gameObject.SetActive(true);

            _visualModel.SetParent(character.RightHandBone);
            _visualModel.localPosition = Vector3.zero;
            _visualModel.localRotation = Quaternion.identity;

            character.ApplyWeapon(this);
        }

        public override void Unapply()
        {
            _visualModel.gameObject.SetActive(false);

            _visualModel.SetParent(transform);
        }

        public virtual void Reload()
        {
            _tracerParticleSystem.Stop();
            var character = gameObject.GetComponentInParent<Character>();
            var bo = character.Reload(ammoTag);
            if (bo)
            {
                _audioSource.PlayOneShot(_reloadingAudio);
                _currentAmmo = MaxAmmo;
            }
        }


        public virtual void Shoot(bool isShooingStarted)
        {
            if (!isShooingStarted)
            {
                _tracerParticleSystem.Stop();
                CancelInvoke("Shooting");
                return;
            }

            if (_currentAmmo > 0)
            {
                _tracerParticleSystem.Play();
                float _time = 0f;

                for (int i = _currentAmmo; i > 0; i--)
                {
                    Invoke("Shooting", _time);
                    _time += 0.2f;
                }
            }
            else
            {
                Reload();
            }
        }

        public void Shooting()
        {
            var ray = new Ray(_shootingPoint.position, transform.forward);
            Debug.DrawLine(ray.origin, ray.GetPoint(50f), Color.red, 3f);

            var raycastHits = Physics.RaycastAll(ray, float.MaxValue, LayerMask.GetMask("Enemies", "Obstacles"));

            var hitResults = raycastHits.ToList();
            hitResults.Sort((x, y) => x.distance.CompareTo(y.distance));

            _muzzleFlashParticleSystem.Play();
            _audioSourceSh.Play(); //_audioSource.PlayOneShot(_shootingAudio);

            for (int i = 0; i < hitResults.Count; ++i)
            {
                // Gameplay code
                if (hitResults[i].collider.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
                {
                    return;
                }

                var characterHealthComponent = hitResults[i].collider.GetComponentInParent<CharacterHealthComponent>();
                if (characterHealthComponent != null)
                {
                    //Debug.Log("Character was hit!");
                    characterHealthComponent.ModifyHealth(-25.0f);
                }
            }

            _currentAmmo--;
            if (_currentAmmo <= 0)
            {
                Reload();
            }
        }

        public override void Use(Character character, ListElement listElement)
        {
            base.Use(character, listElement);
        }

        //AI


        public virtual void AIShoot(bool isShooingStarted)
        {
            if (!isShooingStarted)
            {
                CancelInvoke("AIShooting");
                return;
            }

            if (!IsInvoking("AIShooting"))
            {
                Invoke("AIShooting", Random.Range(0, 5));
            }
        }
        public void AIShooting()
        {
            var _time = 0.0f;
            for (int i = Random.Range(0, 10); i > 0; i--)
            {
                Invoke("AIShotToPlayer", _time);
                _time += 0.2f;
            }
            
        }

        public void AIShotToPlayer()
        {
            var ray = new Ray(_shootingPoint.position, transform.forward);
            Debug.DrawLine(ray.origin, ray.GetPoint(50f), Color.red, 3f);

            var raycastHits = Physics.RaycastAll(ray, float.MaxValue, LayerMask.GetMask("Enemies", "Obstacles", "Player"));

            var hitResults = raycastHits.ToList();
            hitResults.Sort((x, y) => x.distance.CompareTo(y.distance));

            _muzzleFlashParticleSystem.Play();
            _tracerParticleSystem.Play();
            _audioSource.PlayOneShot(_shootingAudio);

            for (int i = 0; i < hitResults.Count; ++i)
            {
                // Gameplay code
                if (hitResults[i].collider.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
                {
                    return;
                }

                var characterHealthComponent = hitResults[i].collider.GetComponentInParent<CharacterHealthComponent>();
                if (characterHealthComponent != null)
                {
                    characterHealthComponent.ModifyHealth(-1.0f);
                }
            }
        }
    }
}