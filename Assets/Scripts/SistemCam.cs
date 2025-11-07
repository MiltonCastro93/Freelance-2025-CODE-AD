using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemCam : MonoBehaviour
{
    [SerializeField] GameObject _pivoteMaster;
    [SerializeField] GameObject _ejeX, _ejeZ;
    Vector3 _positions = Vector3.zero;
    BoxCollider _cajaEvento;
    bool _inLeft = false;
    private GameObject _player;
    [SerializeField] private float _heightCam = 2f;
    private float _distancePivote = 0f;

    void Start()
    {
        if (_pivoteMaster == null || _ejeX == null || _ejeZ == null)
        {
            Debug.LogError("⚠️ SistemCam: Faltan referencias en el Inspector.");
            return;
        }

        _positions.x = (_ejeX.transform.localPosition.x - _pivoteMaster.transform.localPosition.x) / 2f;
        _positions.z = (_ejeZ.transform.localPosition.z - _pivoteMaster.transform.localPosition.z) / 2f;

        if (_cajaEvento == null)
        {
            _cajaEvento = gameObject.AddComponent<BoxCollider>();
            _cajaEvento.center = _positions + new Vector3(0f, 0.5f, 0f);
            _cajaEvento.size = new Vector3(_ejeX.transform.localPosition.x, 1f, _ejeZ.transform.localPosition.z);
            _cajaEvento.isTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_player != null)
        {
            _distancePivote = Vector3.Distance(_player.transform.position, _pivoteMaster.transform.position);
            _inLeft = _distancePivote <= _positions.x;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.gameObject;

            Transform _cameraCurrent = Camera.main.transform;
            CameraLookMe camScript = _cameraCurrent.GetComponent<CameraLookMe>();
            if (camScript != null)
            {
                camScript._sistemCam = this;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = null;
        }
    }

    public Vector3 positionCam()
    {
        if (_inLeft)
        {
            return transform.TransformPoint(_ejeZ.transform.localPosition + new Vector3(0, _heightCam, 0));
        }
        else
        {
            return transform.TransformPoint((_ejeX.transform.localPosition + _ejeZ.transform.localPosition) + new Vector3(0, _heightCam, 0));
        }
    }

    private void OnDrawGizmos()
    {
        if (_pivoteMaster == null || _ejeX == null || _ejeZ == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.TransformPoint(_positions), 0.5f);
        Gizmos.DrawWireCube(transform.TransformPoint(_positions + new Vector3(0f, 0.5f, 0f)),
            new Vector3(_ejeX.transform.localPosition.x, 1f, _ejeZ.transform.localPosition.z));
    }
}


