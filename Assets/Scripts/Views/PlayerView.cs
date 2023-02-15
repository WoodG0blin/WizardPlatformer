using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace WizardsPlatformer
{
    public class PlayerView : BasicView
    {
        public void SetVelocity(float newVelocityX)
        {
            rigidbody.velocity = new Vector2(newVelocityX, rigidbody.velocity.y);
        }

        public void Jump(float jumpForce)
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            ChangeAnimation(ActionState.Jump, true);
        }

        protected override void OnUpdate()
        {
            if (Mathf.Abs(rigidbody.velocity.x) < 0.1f)
            {
                if(animationState != ActionState.Idle) ChangeAnimation(ActionState.Idle);
            }
            else if (Mathf.Abs(rigidbody.velocity.x) < 1f)
            {
                if (animationState != ActionState.Walk) ChangeAnimation(ActionState.Walk);
            }
            else
            {
                if (animationState != ActionState.Run) ChangeAnimation(ActionState.Run);
            }
        }

        public Vector3 Position { get => transform.position; }
    }
}
