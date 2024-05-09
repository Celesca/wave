using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class BossSpecial : MonoBehaviour
{
    public GameObject dangerSignPrefab;
    public GameObject waterfallPrefab;
    private float timeBeforeAttack = 1.2f;
    private float timeBeforeWaterfallDisappear = 0.2f;
    private float specialAttackRange = 15f;
    private float BetweenAtk = 0.7f;
    private float timeBeforeUse = 5f;
    private Animator animator;
    private bool hasActivated = false;
    public BossMove1 bossMove1;
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
            StartCoroutine(SpecialLoop());
            hasStartedCoroutine = true;
        }
    }


    public IEnumerator SpecialLoop()
    {
        while (true)
        {
            // Check conditions to trigger special state
            bool shouldTriggerSpecial = IsPlayerNearby();

            if (shouldTriggerSpecial && !hasActivated)
            {
                float timer = timeBeforeUse;

                while (timer > 0)
                {
                    timer -= Time.deltaTime;
                    yield return null;
                }
                animator.SetTrigger("Move2");
                // Set the danger sign at the player's position but initially inactive
                Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                Vector3 dangerSpawnPosition = playerTransform.position;
                dangerSpawnPosition.y = -4;
                GameObject dangerSign = Instantiate(dangerSignPrefab, dangerSpawnPosition, Quaternion.identity);
                dangerSign.SetActive(true);

                // Wait for the specified duration before setting danger sign inactive and initiating the attack
                yield return new WaitForSeconds(timeBeforeAttack);

                // Deactivate the danger sign
                Vector3 waterSpawnPosition = dangerSign.transform.position;
                
                waterSpawnPosition.y = 0.15f;
                dangerSign.SetActive(false);
                // Instantiate or create the waterfall prefab at the player's position
                GameObject waterfall = Instantiate(waterfallPrefab, waterSpawnPosition, Quaternion.identity);
                // Move the waterfall object to the danger sign's position

                // Activate the waterfall object
                waterfall.SetActive(true);
                while (waterfall.activeSelf)
                {
                    // Update the waterfall position to match the danger sign's position
                    waterSpawnPosition = dangerSign.transform.position;
                    waterSpawnPosition.y = 0.15f;
                    waterfall.transform.position = waterSpawnPosition;
                    yield return new WaitForSeconds(timeBeforeWaterfallDisappear);
                    waterfall.SetActive(false);
                }
                // Wait for the specified duration before deactivating the waterfall


                dangerSign.SetActive(true);
                
                yield return new WaitForSeconds(BetweenAtk);
                dangerSign.SetActive(false);


                waterfall.SetActive(true);
                while (waterfall.activeSelf)
                {
                    // Update the waterfall position to match the danger sign's position
                    waterSpawnPosition = dangerSign.transform.position;
                    waterSpawnPosition.y = 0.15f;
                    waterfall.transform.position = waterSpawnPosition;
                    yield return new WaitForSeconds(timeBeforeWaterfallDisappear);
                    waterfall.SetActive(false);
                }

                dangerSign.SetActive(true);
                yield return new WaitForSeconds(BetweenAtk);
                dangerSign.SetActive(false);

                waterfall.SetActive(true);
                while (waterfall.activeSelf)
                {
                    // Update the waterfall position to match the danger sign's position
                    waterSpawnPosition = dangerSign.transform.position;
                    waterSpawnPosition.y = 0.15f;
                    waterfall.transform.position = waterSpawnPosition;
                    
                    yield return new WaitForSeconds(timeBeforeWaterfallDisappear);
                    waterfall.SetActive(false);
                }


                hasActivated = false;
                

                // Enable the other script
                /*if (bossMove1 != null)
                {
                    bossMove1.enabled = true;
                    enabled = false;
                    hasActivated = true;
                }*/
            }
            hasStartedCoroutine = false;
            yield return null; // Allow the frame to update
        }
    }
    // Check if the player is nearby
    bool IsPlayerNearby()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        return distanceToPlayer <= specialAttackRange;
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