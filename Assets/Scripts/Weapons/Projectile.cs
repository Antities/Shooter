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
        private IShooter _shooter;

        public Rigidbody RigidBody { get; private set; }

        public ProjectileType Type { get { return _projectileType; } }

        #region Unity messages
        protected virtual void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
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
                _shooter.ProjectileHit(this);
            }
        }
        protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Destroyer"))
            {
                _shooter.ProjectileHit(this);
            }
        }
        
        #endregion

        public void Shoot(IShooter shooter, Vector3 direction)
    {
            _shooter = shooter;
            RigidBody.AddForce(direction * _ShootingForce, ForceMode.Impulse);
    }

        
}
}