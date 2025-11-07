using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimaiton : MonoBehaviour
{
    private Animator m_Animator;

    void Start() {
        m_Animator = GetComponent<Animator>();
    }

    public void MoveEnemi(float move) {
        m_Animator.SetBool("IsRunning", move >= Mathf.Epsilon);
    }

}
