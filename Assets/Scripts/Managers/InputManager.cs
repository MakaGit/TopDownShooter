using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TopDownShooter
{
    public class InputManager : SingletonGameObject<InputManager>
    {
        public Action<Vector3> EventPlayerMovementDirectionChanged;
        public Action<Vector3> EventPlayerLookPointChanged;
        public Action<bool> EventPlayerShootingOccured;
        public Action<bool> EventPlayerAimingOccured;
        public Action EventPickUpItemButtonPressed;
        public Action EventInteractButtonPressed;

        private Vector3 _targetMovementVector = Vector3.zero;

        private RaycastHit[] raucastResults = new RaycastHit[5];

        private void Update()
        {
            var newVelocity = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                newVelocity += Vector3.forward + -Vector3.right;
            }

            if (Input.GetKey(KeyCode.S))
            {
                newVelocity += -Vector3.forward + Vector3.right;
            }

            if (Input.GetKey(KeyCode.D))
            {
                newVelocity += Vector3.right + Vector3.forward;
            }

            if (Input.GetKey(KeyCode.A))
            {
                newVelocity += -Vector3.right + -Vector3.forward;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                EventPickUpItemButtonPressed?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                EventInteractButtonPressed.Invoke();
            }

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                EventPlayerShootingOccured?.Invoke(true);
            if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
                EventPlayerShootingOccured?.Invoke(false);
            if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
                EventPlayerAimingOccured?.Invoke(true);
            if (Input.GetMouseButtonUp(1) && !EventSystem.current.IsPointerOverGameObject())
                EventPlayerAimingOccured?.Invoke(false);

            newVelocity = newVelocity.normalized;

            if (_targetMovementVector != newVelocity)
            {
                _targetMovementVector = newVelocity;
                EventPlayerMovementDirectionChanged?.Invoke(_targetMovementVector);
            }


            //var plane = new Plane(Vector3.up, Vector3.zero);
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var raycastHit = Physics.RaycastNonAlloc(ray, raucastResults, float.MaxValue, LayerMask.GetMask("Enemies", "Obstacles", "Player"));

            //if (plane.Raycast(ray, out float distance))
            
                //Vector3 lookPoint = ray.GetPoint(distance);

                Vector3 lookPoint = raucastResults[0].point;
                EventPlayerLookPointChanged?.Invoke(lookPoint);
            
        }
    }
}
