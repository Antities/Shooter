using UnityEngine;


namespace TAMKShooter
{
    public class Projectile : MonoBehaviour
    {

        public enum ProjectileType
        {
            none = 0,
            Laser = 1,
            Explosive = 2,
            Missile = 3
        }

        #region Unity fields
        [SerializeField] private float _ShootingForce;
        [SerializeField] private int _Damage;
        [SerializeField] private ProjectileType _projectileType;
        #endregion

        private Rigidbody _rigidbody;
        public ProjectileType Type { get { return _projectileType; } }

        #region Unity messages
        protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
        protected void OnCollisionEnter(Collision collision)
        {
            IHealth damageReceiver = collision.gameObject.GetComponentInChildren<IHealth>(); // etsii componentin joka peri IHealthin tässä tapauksessa (health)
            if (damageReceiver != null)
            {
                //Colliding object can take damage
                damageReceiver.TakeDamage( _Damage );

                //TODO: Instatiate explosive effect ba-boom
                //TODO: Instatiate sound effects (also boom)
                Destroy(gameObject);
            }
        }
        protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Destroyer"))
            {
                Destroy(gameObject);
            }
        }
        
        #endregion

        public void Shoot(Vector3 direction)
    {
            _rigidbody.AddForce(direction * _ShootingForce, ForceMode.Impulse);
    }

        
}
}