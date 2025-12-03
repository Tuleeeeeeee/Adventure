using UnityEngine;
using Tuleeeeee.CoreComponet;

namespace Tuleeeeee.CoreComponets
{
    public class Movement : CoreComponent
    {
        #region COMPONENTS

        public Rigidbody2D Rb { get; private set; }

        #endregion

        #region PROPERTIES

        public int FacingDirection { get; private set; }
        public Vector2 CurrentVelocity { get; private set; }
        private Vector2 _workspace;
        #endregion

        #region UNITY METHODS

        protected override void Awake()
        {
            base.Awake();
            Rb = GetComponentInParent<Rigidbody2D>();
            FacingDirection = 1;
        }

        public override void LogicsUpdate()
        {
            CurrentVelocity = Rb.velocity;
        }

        #endregion

        #region VELOCITY METHODS

        public void ApplyGravity(float fallAcceleration, float maxFallSpeed, float groundingForce, float jumpEndEarlyModifier,
            bool isGrounded, bool endedJumpEarly)
        {
            if (isGrounded && CurrentVelocity.y <= 0f)
            {
                _workspace.Set(CurrentVelocity.x, groundingForce);
            }
            else
            {
                float appliedGravity = fallAcceleration;
                if (endedJumpEarly && CurrentVelocity.y > 0f)
                    appliedGravity *= jumpEndEarlyModifier;

                _workspace.Set(CurrentVelocity.x, Mathf.MoveTowards(CurrentVelocity.y,
                    -maxFallSpeed, appliedGravity * Time.fixedDeltaTime));
            }
            Rb.velocity = _workspace;
            CurrentVelocity = _workspace;
        }

        public void ApplyPositionOffset(Vector2 offset)
        {
            Rb.position += offset;
            CurrentVelocity = Rb.velocity;
        }

        public void SetVelocityZero()
        {
            Rb.velocity = Vector2.zero;
            CurrentVelocity = Vector2.zero;
        }

        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            _workspace.Set(angle.x * velocity * direction, angle.y * velocity);
            Rb.velocity = _workspace;
            CurrentVelocity = _workspace;
        }

        public void SetVelocity(float velocity, Vector2 direction)
        {
            _workspace = direction * velocity;
            Rb.velocity = _workspace;
            CurrentVelocity = _workspace;
        }

        public void SetVelocityX(float velocity, float acceleration)
        {
            // float newX = Mathf.MoveTowards(CurrentVelocity.x, velocity, acceleration * Time.fixedDeltaTime);
            _workspace.Set(velocity, CurrentVelocity.y);
            Rb.velocity = _workspace;
            CurrentVelocity = _workspace;
        }

        public void SetVelocityY(float velocity)
        {
            _workspace.Set(CurrentVelocity.x, velocity);
            Rb.velocity = _workspace;
            CurrentVelocity = _workspace;
        }

        #endregion

        #region GRAVITY & FREEZE

        public void SetGravityScale(float value)
        {
            Rb.gravityScale = value;
        }

        public void Freeze()
        {
            Rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        public void Unfreeze()
        {
            Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        #endregion

        #region FLIP LOGIC

        public void CheckIfShouldFlip(int xInput)
        {
            if (xInput != 0 && xInput != FacingDirection)
            {
                Flip();
            }
        }

        public void Flip()
        {
            FacingDirection *= -1;
            Vector3 localScale = Rb.transform.localScale;
            localScale.x *= -1f;
            Rb.transform.localScale = localScale;
        }

        #endregion
    }
}