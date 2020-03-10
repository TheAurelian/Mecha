using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class PlayerFly : MonoBehaviour
    {
        // TODO Comments, headers, tooltips
        [SerializeField] private float jumpSpeed = 2f;
        [SerializeField] private float maxJumpHeight = 10f;
        [SerializeField] private float floatTime = 3f;

        private CharacterController playerCharController;
        private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpController;

        private bool isJumping = false;
        private float currentJumpHeight = 0;

        public static event System.Action<bool> OnPlayerFly;

        // Start is called before the first frame update
        void Start()
        {
            fpController = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
            playerCharController = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            isJumping = Input.GetButton("Jump");
        }

        private void FixedUpdate() // TODO This code is soooo bad but it's purely for prototype purposes
        {
            if (isJumping)
            {
                if (playerCharController.isGrounded)
                {
                    OnPlayerFly?.Invoke(true);
                    currentJumpHeight = transform.position.y + maxJumpHeight;
                    playerCharController.SimpleMove(Vector3.up);
                }
                else
                {
                    if (transform.position.y <= currentJumpHeight)
                    {
                        playerCharController.SimpleMove(Vector3.up * jumpSpeed);
                    }
                    else
                    {
                        OnPlayerFly?.Invoke(false);
                    }
                }
            }
        }
    }
}
