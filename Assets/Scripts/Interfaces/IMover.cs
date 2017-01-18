using UnityEngine;

namespace TAMKShooter
{
    public interface IMover
    {
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
        float Speed { get; }

        void MoveTowardPosition(Vector3 targetPosition);
        void MoveToDirection(Vector3 direction);
        void RotateTowardPosition(Vector3 targetPosition);
    }
}
