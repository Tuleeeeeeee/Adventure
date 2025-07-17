using UnityEngine;
using Tuleeeeee.CoreComponet;

namespace Tuleeeeee.CoreComponets
{
    public class Movement : CoreComponent
    {
        public Rigidbody2D Rb { get; private set; }

        public int FacingDirection { get; private set; }

        public Vector2 CurrentVelocity { get; private set; }

        private Vector2 workspace;

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

        #region Set Functions
        public void ApplyPositionOffset(Vector2 direction)
        {
            workspace += direction;
            Rb.velocity = workspace;
            CurrentVelocity = workspace;
        }

        public void SetVelocityZero()
        {
            Rb.velocity = Vector2.zero;
            CurrentVelocity = Vector2.zero;
        }

        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            workspace.Set(angle.x * velocity * direction, angle.y * velocity);
            Rb.velocity = workspace;
            CurrentVelocity = workspace;
        }

        public void SetVelocity(float velocity, Vector2 direction)
        {
            workspace = direction * velocity;
            Rb.velocity = workspace;
            CurrentVelocity = workspace;
        }

        public void SetVelocityX(float velocity)
        {
            workspace.Set(velocity, CurrentVelocity.y);
            Rb.velocity = workspace;
            CurrentVelocity = workspace;
        }

        public void SetVelocityY(float velocity)
        {
            workspace.Set(CurrentVelocity.x, velocity);
            Rb.velocity = workspace;
            CurrentVelocity = workspace;
        }

        public void SetGravityScale(float value)
        {
            Rb.gravityScale = value;
        }

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