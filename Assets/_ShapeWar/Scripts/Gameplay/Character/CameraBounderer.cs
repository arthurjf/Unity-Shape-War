using UnityEngine;

namespace br.com.arthurjf.shapewar.Gameplay.Character
{
    public class CameraBounderer : MonoBehaviour
    {
        private Camera _mainCamera;
        private float _minX, _maxX, _minY, _maxY;

        private void Start()
        {
            _mainCamera = Camera.main;

            CalculateBounds();
        }

        private void Update()
        {
            LimitObjectWithinBounds(transform);
        }

        private void CalculateBounds()
        {
            _minX = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            _maxX = _mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            _minY = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
            _maxY = _mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        }

        private void LimitObjectWithinBounds(Transform objectTransform)
        {
            var clampedX = Mathf.Clamp(objectTransform.position.x, _minX, _maxX);

            var clampedY = Mathf.Clamp(objectTransform.position.y, _minY, _maxY);

            objectTransform.position = new Vector3(clampedX, clampedY, objectTransform.position.z);
        }
    }
}
