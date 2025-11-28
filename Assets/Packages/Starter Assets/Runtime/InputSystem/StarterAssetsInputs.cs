using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sneak;
		public bool attack;
		public bool weapon1;
		public bool weapon2;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSneak(InputValue value)
		{
			SneakInput(value.isPressed);
		}

        public void OnAttack(InputValue value)
        {
            AttackInput(value.isPressed);
        }

        public void OnWeapon1(InputValue value)
        {
            Weapon1Input(value.isPressed);
        }

        public void OnWeapon2(InputValue value)
        {
            Weapon2Input(value.isPressed);
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SneakInput(bool newSneakState)
		{
			sneak = newSneakState;
		}

        public void AttackInput(bool newAttackState)
        {
            attack = newAttackState;
        }

        public void Weapon1Input(bool newWeapon1State)
        {
            weapon1 = newWeapon1State;
        }

        public void Weapon2Input(bool newWeapon2State)
        {
            weapon2 = newWeapon2State;
        }

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}