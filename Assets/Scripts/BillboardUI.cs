using UnityEngine;

namespace AugmentedRapCollection
{
    public class BillboardUI : MonoBehaviour
    {
        private Transform cameraTransform;

        private void Start()
        {
            if (Camera.main != null)
                cameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            if (cameraTransform == null)
            {
                if (Camera.main == null) return;
                cameraTransform = Camera.main.transform;
            }

            Vector3 direction = cameraTransform.position - transform.position;
            direction.y = 0f; // ignore vertical tilt

            if (direction.sqrMagnitude > 0.0001f)
                transform.rotation = Quaternion.LookRotation(-direction);
        }
    }
}