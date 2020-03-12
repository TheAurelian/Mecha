using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class PlayerFly : MonoBehaviour
    {
        // TODO Comments, headers, tooltips
        [SerializeField] private float jumpSpeed = 2f;
        [SerializeField] private float jumpLerpSpeed = 0.5f;
        [SerializeField] private float maxJumpHeight = 10f;
        [SerializeField] private float floatTime = 3f;

        private CharacterController playerCharController;
        private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpController;

        private bool isJumping = false;
        private float currentJumpHeight = 0;
        private float initialFloatTime;

        public static event System.Action<bool> OnPlayerFly;

        // Start is called before the first frame update
        void Start()
        {
            initialFloatTime = floatTime;
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
                    OnPlayerFly(true);
                    currentJumpHeight = transform.position.y + maxJumpHeight;
                }
                if (transform.position.y < currentJumpHeight && floatTime > 0)
                {                    
                    float newJumpSpeed = Mathf.Lerp(0, jumpSpeed, jumpLerpSpeed * Time.fixedDeltaTime);
                    playerCharController.Move(Vector3.up * newJumpSpeed);
                }
                else
                {                                        
                    if (floatTime <= 0)
                    {
                        OnPlayerFly(false);
                    }
                    else
                    {
                        floatTime -= Time.fixedDeltaTime;
                        
                    }
                }                
            }
            else
            {
                if (floatTime < initialFloatTime)
                {
                    floatTime += Time.fixedDeltaTime;
                }
                if (!playerCharController.isGrounded)
                {
                    OnPlayerFly(false);
                }
            }
        }
    }
}
