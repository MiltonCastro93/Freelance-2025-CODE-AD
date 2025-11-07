
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update() {
        Vector3 direccion = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        transform.rotation = Quaternion.LookRotation(-direccion);
        animator.SetBool("IsRunning", direccion != Vector3.zero);

    }
}
