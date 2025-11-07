using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookMe : MonoBehaviour
{
    public SistemCam _sistemCam;
    [SerializeField] GameObject heroe;
    [SerializeField] private float _focusY = 2f;

    private void Awake()
    {
        heroe = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        if (_sistemCam != null)
        {
            Vector3 relative = (heroe.transform.position + new Vector3(0f, _focusY, 0f)) - transform.position;
            transform.position = _sistemCam.positionCam();
            transform.rotation = Quaternion.LookRotation(relative);
        }
    }
}
