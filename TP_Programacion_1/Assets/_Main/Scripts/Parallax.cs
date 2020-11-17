using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
        [SerializeField, Range(0f,1f)] private float parallaxEffectMultiplier = 0;

        private Transform cameraTransform;
        private Vector3 lastCameraPosition;
        private float width;
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            cameraTransform = Camera.main.transform;
            lastCameraPosition = cameraTransform.position;
            spriteRenderer = GetComponent<SpriteRenderer>();
            width = spriteRenderer.bounds.size.x;
            //bounds son los limites que tiene el sprite renderer
            
        }
        private void Update()
        {
            Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
            transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier,0f,0f);
            //Cuanto se movio la camara x el multiplicador de la camara
            lastCameraPosition = cameraTransform.position;

            float distanceWithCamera = cameraTransform.position.x - transform.position.x;

            if(Mathf.Abs(distanceWithCamera) >= width)
            {
                float movement = distanceWithCamera > 0 ? width * 2f : width * -2f;
                /*
                float movement = 0;

                if(distanceWithCamera > 0)
                {
                   movement = width * 2f;
                }
                else if (distanceWithCamera < 0)
                {
                    movement = width * -2f;
                }*/
                transform.position += new Vector3 (movement,0f,0f);

            }
        }
}

