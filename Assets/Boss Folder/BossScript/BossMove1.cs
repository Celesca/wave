using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;


public class BossMove1 : MonoBehaviour {
    public BossSpecial bossSpecial;
    public GameObject bullet;
    public Transform bulletPos;
    private float timeBeforeAttack = 15f;
    private float timeBeforeUse = 0.6f;
    private float move1AttackRange = 15f;
    private float betweenAtk = 0.8f;
    private Animator animator;
    private bool hasActivated = false;
    private bool hasStartedCoroutine = false;

    private void Start()
    {

    }
    private void Update()
    {
        if (!hasStartedCoroutine)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component not found.");
                return;
            }
            hasActivated = false;
            StartCoroutine(Move1());
            hasStartedCoroutine = true;
        }
    }
    //normalATK stop atk 
    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    public IEnumerator Move1()
    {
        while (true)
        {
            // Check conditions to trigger special state
            bool shouldTriggerSpecial = IsPlayerNearby();

            if (shouldTriggerSpecial && !hasActivated)
            {
                float timer = 0;

                while (timer < 4)
                {
                    timer += Time.deltaTime;
                    yield return null;
                }
                animator.SetTrigger("Move1");
                yield return new WaitForSeconds(timeBeforeUse);
                while (isPlaying(animator, "Move1"))
                {
                    Shoot();
                    yield return new WaitForSeconds(betweenAtk);
                }

                hasActivated = false;
                // Enable the other script
                if (bossSpecial != null)
                {
                    bossSpecial.enabled = true;
                    enabled = false;
                    hasActivated = true;
                }
            }
            hasStartedCoroutine = false;
            yield return null; // Allow the frame to update
        }
        // Check if the player is nearby
        bool IsPlayerNearby()
        {
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            return distanceToPlayer <= move1AttackRange;
        }
        bool isPlaying(Animator anim, string stateName)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                    anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                return true;
            else
                return false;
        }
    }
}
