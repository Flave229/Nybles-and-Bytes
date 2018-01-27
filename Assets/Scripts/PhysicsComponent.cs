using UnityEngine;

namespace Assets.Scripts
{
    public class PhysicsComponent : MonoBehaviour
    {
        [SerializeField]
        private float _mMoveForce;
        private Rigidbody _mRigidBody;
        private bool _mIsControlledByUser = true;

        void Start()
        {
            _mRigidBody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            // artificial gravity stronger than regular gravity
            _mRigidBody.AddForce(Vector3.down * 20.0f * _mRigidBody.mass);
            if (!_mIsControlledByUser) return;

            float leftRight = Input.GetAxis("Horizontal");
            if (leftRight != 0)
                _mRigidBody.MovePosition(this.transform.position + (new Vector3(leftRight * _mMoveForce, 0.0f, 0.0f) * Time.deltaTime));
        }
    }
}