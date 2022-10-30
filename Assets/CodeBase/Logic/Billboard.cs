using UnityEngine;

namespace Logic
{
    public class Billboard : MonoBehaviour
    {
        private Transform _camera;

        private void Awake()
        {
            _camera = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.rotation  = Quaternion.LookRotation(_camera.transform.position, Vector3.up);
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }
    }
}
