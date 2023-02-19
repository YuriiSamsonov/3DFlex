using UnityEngine;

namespace Game.Scripts.Objects
{
    public class GrabableObject : MonoBehaviour
    {
        [field: SerializeField] 
        private Rigidbody rBody;

        public Rigidbody RBody => rBody;

        private Transform _objInHandTransform;
        private float lerpSpeed = 20f;

        private void Reset()
        {
            rBody = GetComponent<Rigidbody>();
        }

        public void Grab(Transform handTransform)
        {
            _objInHandTransform = handTransform;
            rBody.drag = 5f;
            rBody.useGravity = false;
            _objInHandTransform.localRotation = new Quaternion(0,0,0,0);
        }

        public void Drop()
        {
            _objInHandTransform = null;
            rBody.drag = 0f;
            rBody.useGravity = true;
        }

        private void Update()
        {
            if (_objInHandTransform != null)
            {
                var moveLerp = Vector3.Lerp(transform.position, _objInHandTransform.position, Time.deltaTime * lerpSpeed);
                rBody.MovePosition(moveLerp);
                
            } 
        }
    }
}