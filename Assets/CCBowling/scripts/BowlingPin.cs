using UnityEngine;

namespace CCBowling
{

    // Lectures 7 and 8.
    // Goes on the pin prefab, so all ten get it.
    public class BowlingPin : MonoBehaviour
    {
        // How far it has to tilt before we call it fallen. 30 is a good start.
        public float fallenAngle = 30f;

        private Vector3 startPosition;
        private Quaternion startRotation;
        private Rigidbody body;
        private MeshRenderer mesh;
        private CapsuleCollider myCollider;

        private bool isPinActive = true;

        void Start()
        {
            // Measured, not typed, so this works on any lane layout.
            startPosition = transform.position;
            startRotation = transform.rotation;

            TryGetComponent<Rigidbody>(out body);
            TryGetComponent<MeshRenderer>(out mesh);
            TryGetComponent<CapsuleCollider>(out myCollider);
        }

        // A pin that fell over is rotated compared to where it started.
        public bool HasFallen()
        {
            // Lecture 8. A pin already taken off the lane cannot fall again.
            // Without this line it stays "fallen" forever and gets counted on
            // every throw, so the score climbs on its own.
            if (!isPinActive)
            {
                return false;
            }

            float tilt = Quaternion.Angle(startRotation, transform.rotation);
            return tilt > fallenAngle;
        }

        // Lecture 8. Counted, so get off the lane. SetActive(false) hides the
        // object AND stops its physics, so the next ball rolls straight past.
        public void Knockdown()
        {
            isPinActive = false;
            if (mesh != null) mesh.enabled = false;
            if (myCollider != null) myCollider.enabled = false;
        }

        public void ResetPin()
        {
            isPinActive = true;
            transform.position = startPosition;
            transform.rotation = startRotation;

            // Without this the pin teleports home and immediately falls over
            // again, because it kept the speed it had when it was knocked down.
            // Unity 6 name: velocity is now linearVelocity.
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;

            if (mesh != null) mesh.enabled = true;
            if (mesh != null) myCollider.enabled = true;
        }

    }
}