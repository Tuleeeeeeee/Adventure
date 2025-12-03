using Tuleeeeee.CoreComponet;
using UnityEngine;

namespace Tuleeeeee.CoreComponets
{
    public class CollisionSenses : CoreComponent
    {
        private Movement Movement
        {
            get => _movement ?? Core.GetCoreComponent(ref _movement);
        }

        private Movement _movement;

        #region Check Transforms

        public Transform GroundCheck
        {
            get =>
                GenericNotImplementedError<Transform>.TryGet(
                    groundCheck,
                    Core.transform.parent.name
                );
            private set => groundCheck = value;
        }

        public Transform WallCheck
        {
            get =>
                GenericNotImplementedError<Transform>.TryGet(wallCheck, Core.transform.parent.name);
            private set => wallCheck = value;
        }

        /*public Transform LedgeCheckHorizontal
        {
            get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name);
            private set => ledgeCheckHorizontal = value;
        }*/
        public Transform LedgeCheckVertical
        {
            get =>
                GenericNotImplementedError<Transform>.TryGet(
                    ledgeCheckVertical,
                    Core.transform.parent.name
                );
            private set => ledgeCheckVertical = value;
        }

        public Vector2 GroundCheckSize
        {
            get => groundCheckSize;
            set => groundCheckSize = value;
        }

        public float WallCheckDistance
        {
            get => wallCheckDistance;
            set => wallCheckDistance = value;
        }

        public float LedgeCheckDistance
        {
            get => ledgeCheckDistance;
            set => ledgeCheckDistance = value;
        }

        public LayerMask WhatIsGround
        {
            get => whatIsGround;
            set => whatIsGround = value;
        }

        [SerializeField]
        private Transform groundCheck;

        [SerializeField]
        private Transform wallCheck;

        [SerializeField]
        private Transform ledgeCheckVertical;

        [SerializeField]
        private Vector2 groundCheckSize;

        [SerializeField]
        private float wallCheckDistance;

        [SerializeField]
        private float ledgeCheckDistance;

        [SerializeField]
        public float platformCheckDistance;

        [SerializeField]
        private LayerMask whatIsGround;

        [SerializeField]
        private LayerMask whatIsPlatform;

        private bool _isOnPlatform;
        private Rigidbody2D _currentPlatformRBody;
        private Vector2 _lastPlatformPosition;

        #endregion

        /*   public bool Ceiling
           {
               get => Physics2D.OverlapCircle(ceilingCheck.position, groundCheckRadius, whatIsGround);
           }
       */
        public bool Ground
        {
            get => Physics2D.OverlapBox(GroundCheck.position, groundCheckSize, 0f, whatIsGround);
        }

        public bool WallFront
        {
            get =>
                Physics2D.Raycast(
                    WallCheck.position,
                    Vector2.right * Movement.FacingDirection,
                    wallCheckDistance,
                    whatIsGround
                );
        }

        /*public bool LedgeHorizontal
        {
            get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
        }*/

        public bool LedgeVertical
        {
            get =>
                Physics2D.Raycast(
                    LedgeCheckVertical.position,
                    Vector2.down,
                    ledgeCheckDistance,
                    whatIsGround
                );
        }

        public bool WallBack
        {
            get =>
                Physics2D.Raycast(
                    WallCheck.position,
                    Vector2.right * -Movement.FacingDirection,
                    wallCheckDistance,
                    whatIsGround
                );
        }

        public bool Platform(Collider2D collider2D)
        {
            Vector2 boxCenter = collider2D.bounds.center;
            Vector2 boxSize = collider2D.bounds.size;

            RaycastHit2D hit = Physics2D.BoxCast(
                boxCenter,
                boxSize,
                0f,
                Vector2.down,
                platformCheckDistance,
                whatIsPlatform
            );

            Rigidbody2D hitBody = hit.collider ? hit.collider.attachedRigidbody : null;

            // Standing on a platform
            if (hitBody != null)
            {
                if (!_isOnPlatform)
                    _lastPlatformPosition = hitBody.position; // First frame touching platform

                _isOnPlatform = true;
                _currentPlatformRBody = hitBody;

                return true;
            }

            // Not standing on any platform
            if (_isOnPlatform)
            {
                _isOnPlatform = false;
                _currentPlatformRBody = null;
            }

            return false;
        }

        public void OpTopPlatformMovement()
        {
            if (_isOnPlatform && _currentPlatformRBody != null)
            {
                Vector2 platformMovement = _currentPlatformRBody.position - _lastPlatformPosition;
                Movement.ApplyPositionOffset(platformMovement);
                _lastPlatformPosition = _currentPlatformRBody.position;
            }
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                return;

            if (GroundCheck)
            {
                Gizmos.color = Ground ? Color.green : Color.red;
                Gizmos.DrawCube(GroundCheck.position, groundCheckSize);
            }

            if (LedgeCheckVertical)
            {
                Gizmos.color = LedgeVertical ? Color.green : Color.red;
                Gizmos.DrawLine(
                    LedgeCheckVertical.position,
                    LedgeCheckVertical.position + (Vector3)(Vector2.down * ledgeCheckDistance)
                );
            }

            if (WallCheck)
            {
                Gizmos.color = WallFront ? Color.green : Color.red;
                Gizmos.DrawLine(
                    WallCheck.position,
                    WallCheck.position
                        + (Vector3)(Vector2.right * Movement.FacingDirection * wallCheckDistance)
                );

                Gizmos.color = WallBack ? Color.green : Color.red;
                Gizmos.DrawLine(
                    WallCheck.position,
                    WallCheck.position
                        + (Vector3)(Vector2.right * -Movement.FacingDirection * wallCheckDistance)
                );
            }
        }
    }
}
