using System;
using UnityEngine;

namespace TAMKShooter
{
    class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private int _health; // SerializeField = voi tallentaa tiedostoon
        public int CurrentHealth
        {
            get { return _health; }

            set
            {
                _health = Mathf.Clamp(value, 0, value);
                if(HealthChanged != null)
                {
                    HealthChanged(this, new HealthChangedEventArgs(_health));
                }
            }
        }

        public event HealthChangedDelegate HealthChanged;

        /// <summary>
        /// Applies damage. Returns true if health is reduced to zero.
        /// </summary>
        /// <param name="damage"> Amount of damage applied </param>
        /// <returns></returns>
        public bool TakeDamage(int damage)
        {
            CurrentHealth -= damage; // currentHealth = currentHealth - damage;
            return CurrentHealth == 0;
        }
    }
}
