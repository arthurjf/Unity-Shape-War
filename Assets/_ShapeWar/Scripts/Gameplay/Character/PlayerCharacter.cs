using UnityEngine;

namespace br.com.arthurjf.shapewar.Gameplay.Character
{
    // INHERITANCE
    public class PlayerCharacter : CharacterBase
    {
        [SerializeField] private Rigidbody2D m_rigidbody;
        [SerializeField] private float m_maxSpeed = 50f;

        private Vector2 _inputs;

        private void Reset()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();

            if (m_rigidbody != null)
            {
                m_rigidbody.gravityScale = 0f;
            }
        }

        private void Update()
        {
            _inputs.x = Input.GetAxis("Horizontal");
            _inputs.y = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.Space))
            {
                Shoot();
            }
        }

        private void FixedUpdate()
        {
            Move(_inputs.y);

            Rotate(_inputs.x);

            LimitVelocity();
        }

        private void LimitVelocity()
        {
            if (m_rigidbody.velocity.magnitude > m_maxSpeed)
            {
                m_rigidbody.velocity = m_rigidbody.velocity.normalized * m_maxSpeed;
            }
        }

        // POLYMORPHISM
        protected override void Move(float amount)
        {
            m_rigidbody.AddForce(amount * transform.up, ForceMode2D.Force);
        }
    }
}
