using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;

    [Header("Footsteps")]
    [SerializeField] private AudioClip[] GrassClips;
    [SerializeField] private AudioClip[] MetalClips;
    private float timer = 0;

    // GLOBAL
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ProcessFootstep();
    }

    private void ProcessFootstep()
    {
        if (config.FootstepsEnabled && PlayerMovementController.isGrounded && PlayerMovementController.isMoving &&
            Physics.Raycast(gameObject.transform.position, Vector3.down, out RaycastHit hit, 2f))
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                hit.transform.gameObject.TryGetComponent(out ISurface surface);
                switch (surface?.SurfaceName)
                {
                    case SurfaceType.Grass:
                        audioSource.PlayOneShot(GrassClips[Random.Range(0, GrassClips.Length)]);
                        break;
                    case SurfaceType.Metal:
                        audioSource.PlayOneShot(MetalClips[Random.Range(0, MetalClips.Length)]);
                        break;
                    default:
                        audioSource.PlayOneShot(MetalClips[Random.Range(0, MetalClips.Length)]);
                        break;
                }
                if (PlayerMovementController.isSprinting)
                {
                    timer = config.SprintFootstepInterval;
                }
                else if (PlayerMovementController.isCrouching)
                {
                    timer = config.CrouchFootstepInterval;
                }
                else
                {
                    timer = config.WalkFootstepInterval;
                }
            }
        }
    }
}
