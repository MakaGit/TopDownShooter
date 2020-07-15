using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TopDownShooter
{
    public class ForceGate : InteractableObject
    {
        [SerializeField] private GameObject forceField = null;
        [SerializeField] private AudioSource audioSource = null;

        private bool isOpen = false;
        private Coroutine CurAction = null;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OpenGate(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OpenGate(false);       
            }
        }
        private void OpenGate(bool inter)
        {
            if(inter)
            {
                if (isOpen)
                {
                     return;
                }
                else
                {
                    isOpen = true;
                    StartCoroutine(openGate(isOpen));
                }
            }
            else
            {
                if (isOpen)
                {
                    isOpen = false;
                    StartCoroutine(openGate(isOpen));
                }
                else
                {
                    return;
                }
            }
        }

        private IEnumerator openGate(bool inter)
        {
            var mesh = forceField.GetComponent<MeshRenderer>();
            Sequence seq = DOTween.Sequence();
            audioSource.PlayOneShot(audioSource.clip);

            if (inter)
            {
                seq.Append(mesh.material.DOFade(0.0f, 1.0f));
                yield return seq.WaitForCompletion();
                forceField.SetActive(false);
            }
            else
            {
                forceField.SetActive(true);
                seq.Append(mesh.material.DOFade(1.0f, 1.0f));
                yield return seq.WaitForCompletion();
            }

            yield break;
        }
    }
}
