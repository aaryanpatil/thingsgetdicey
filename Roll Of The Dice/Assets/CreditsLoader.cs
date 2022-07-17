using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class CreditsLoader : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI thankYou;
    [SerializeField] TextMeshProUGUI github;
    [SerializeField] TextMeshProUGUI zapsplat;
    Animator animator;
    int tracker = 0;

    private void Awake() 
    {     
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        animator.SetTrigger("Thank You");
        tracker = 1;
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0) && tracker == 1)
        {
            animator.SetTrigger("Github");
            thankYou.enabled = false;
            github.enabled = true;
            tracker++;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0) && tracker == 2)
        {
            animator.SetTrigger("Zapsplat");
            github.enabled = false;
            zapsplat.enabled = true;
            tracker++;
        }
    }
}
