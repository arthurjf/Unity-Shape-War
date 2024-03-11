using UnityEngine;

namespace br.com.arthurjf.shapewar.Gameplay.Character
{
    // INHERITANCE
    public class EnemyCharacter : CharacterBase
    {
        [SerializeField] private Transform m_target;
        [SerializeField] private float m_followDistance = 5f;
        [SerializeField] private float m_moveSmoothTime = 0.8f;
        [SerializeField] private float m_sightRadius = 3f;
        [SerializeField] private float m_shootDistance = 6f;
        [SerializeField] private LayerMask m_layerMask;
        [SerializeField] private int m_defeatScoreEarn = 50;

        private float _maxRotationPerFrame;
        private float _smoothedVelocityInput;
        private float _smoothVelocity;

        public int DefeatScoreEarn => m_defeatScoreEarn;

        public Transform Target
        {
            set => m_target = value;
        }

        private void Update()
        {
            var targetVelocityInput = 0f;

            if (HasTargetOnSight())
            {
                Shoot();
            }

            if (m_target != null)
            {
                _maxRotationPerFrame = m_rotateSpeed * Time.deltaTime;

                var targetAngle = GetTargetAngle() - 90f;

                Rotate(targetAngle);

                var distanceToTarget = Vector3.Distance(transform.position, m_target.position);

                if (distanceToTarget > m_followDistance)
                {
                    targetVelocityInput = m_moveSpeed;
                }
            }
            _smoothedVelocityInput = Mathf.SmoothDamp(_smoothedVelocityInput, targetVelocityInput, ref _smoothVelocity, m_moveSmoothTime);

            Move(_smoothedVelocityInput);
        }

        private bool HasTargetOnSight()
        {
            var hit = Physics2D.CircleCast(transform.position, m_sightRadius, transform.up, m_shootDistance, m_layerMask);

            if (hit.collider != null)
            {
                PlayerCharacter playerCharacter = hit.collider.GetComponent<PlayerCharacter>();

                return playerCharacter != null;
            }

            return false;
        }

        // POLYMORPHISM
        protected override void Rotate(float targetAngle)
        {
            var desiredRotation = Quaternion.Euler(0f, 0f, targetAngle);

            var angleDifference = Quaternion.Angle(transform.rotation, desiredRotation);

            var clampedAngleDifference = Mathf.Clamp(angleDifference, -_maxRotationPerFrame, _maxRotationPerFrame);

            var limitedRotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, clampedAngleDifference);

            transform.rotation = limitedRotation;
        }

        // POLYMORPHISM
        protected override void Move(float amount)
        {
            transform.Translate(amount * Time.fixedDeltaTime * transform.up, Space.World);
        }

        private float GetTargetAngle()
        {
            var direction = m_target.position - transform.position;

            var targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            return targetAngle;
        }
    }
}
