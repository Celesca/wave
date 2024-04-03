using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private float timer;
    public Animator anim;
    private GameObject player;
    private GameObject enemy;
    private Vector3 initScale;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        initScale = transform.localScale;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("RangeEnemy");
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);
        if(distance < 15)
        {
            timer += Time.deltaTime;
            if (timer > 1.5)
            {
                timer = 0;
                Shoot();
            }
            if (GameObject.FindWithTag("Player") != null) 
            {
                if (player.transform.position.x > enemy.transform.position.x)
                    enemy.transform.localScale = new Vector3(Mathf.Abs(initScale.x) * 1,
                    initScale.y, initScale.z);
                else
                    enemy.transform.localScale = new Vector3(Mathf.Abs(initScale.x) * -1,
                    initScale.y, initScale.z);
            }
            
        }
        if (enemyPatrol != null) 
        {
            enemyPatrol.enabled = distance > 15;
        }


    }

    void Shoot()
    {
        anim.SetTrigger("rangedAttack");
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
