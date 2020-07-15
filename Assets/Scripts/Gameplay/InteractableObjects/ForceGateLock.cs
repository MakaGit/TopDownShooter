using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class ForceGateLock : InteractableObject
    {
        [SerializeField] private Collider _forceGate = null;
        [SerializeField] private Collider _forceGateLock = null;
        [SerializeField] private OutlineAddon _outlineAddon = null;
        [SerializeField] private GameObject _key = null;
        [SerializeField] private AudioSource audioSource = null;

        [SerializeField] private string ItemTag = null;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _outlineAddon.enabled = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _outlineAddon.enabled = false;
            }
        }

        public override void Interact()
        {
            audioSource.Play();
            _key.SetActive(true);
            _forceGate.enabled = true;
            _forceGateLock.enabled = false;
            _outlineAddon.enabled = false;
        }

        public override string GetItemTag()
        {
            return ItemTag;
        }
    }
}
