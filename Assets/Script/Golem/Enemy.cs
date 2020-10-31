using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float shootingInterval;
    private float currentTime ;

    public GameObject Sword;
    public GameObject longSword;
    public GameObject gun;
    public Rigidbody2D bullet;
    public Transform muzzle;
    public float bulletSpeed;

    [SerializeField]
    float maxHealth;

    //private
    public float health;

    [SerializeField]
    private float FasterMoveSpeed;
    [SerializeField]
    private float NormalMoveSpeed;
    [SerializeField]
    private float SlowerMoveSpeed;

    //private
    public float mSpeed;

    [SerializeField]
    private float mTime;

    [SerializeField]
    private GolemData.elementType mGolemType;


    //private
    private GolemData.attackType mAttackType;

    //private
    public bool reverse = false;

    //private
    public bool selfGeren = false;

    private void Awake()
    {
        
        currentTime = shootingInterval;
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        initGolemFeature();
    }

    private void Update()
    {
       // if(Input.GetKeyDown(KeyCode.Q))
       // {
       //     Rigidbody2D temp = Instantiate(bullet, muzzle.position, transform.rotation);
       //     if (!reverse)
       //     {
       //         temp.velocity = new Vector2(bulletSpeed, 0);
       //     }
       //     else
       //     {
       //         temp.velocity = new Vector2(-bulletSpeed, 0);
       //     }
       // }

        KillEnemy();
        Move();
        Shoot();
        SelfRegen();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "wall")
        {
            reverse = !reverse;
            Debug.Log("hit the block");
            transform.localScale = new Vector3(-1 * transform.localScale.x, 1, 0); 
        }
        if (coll.gameObject.tag == "Enemy")
        {      
            reverse = !reverse;
            Debug.Log("hit the block");
            transform.localScale = new Vector3(-1 * transform.localScale.x, 1, 0);
        }
    }

    private void initGolemFeature()
    {
        if (mGolemType == GolemData.elementType.Air)
        {
            mAttackType = GolemData.attackType.Ranged;
            Debug.Log("init Air bot attack to ranged");
            mSpeed = NormalMoveSpeed;
            Sword.SetActive(false);
            longSword.SetActive(false);

        }
        else if (mGolemType == GolemData.elementType.Earth)
        {
            mAttackType = GolemData.attackType.Melee;
            Debug.Log("init Earth bot attack to Melee");
            mSpeed = SlowerMoveSpeed;
            health = 2 * maxHealth;
            longSword.SetActive(false);
            gun.SetActive(false);
        }
        else if (mGolemType == GolemData.elementType.Fire)
        {
            mAttackType = GolemData.attackType.Ranged;
            Debug.Log("init Fire bot attack to Ranged");
            mSpeed = NormalMoveSpeed;
            Sword.SetActive(false);
            longSword.SetActive(false);
        }
        else if (mGolemType == GolemData.elementType.Water)
        {
            mAttackType = GolemData.attackType.midRanged;
            Debug.Log("init Water bot attack to midRanged");
            mSpeed = FasterMoveSpeed;
            selfGeren = true;
            Sword.SetActive(false);
            gun.SetActive(false);
        }
    }
    private void Move()
    {
        if (!reverse)
        {
            transform.Translate(new Vector2(mSpeed, 0) * Time.deltaTime);
            //rb.velocity = new Vector2(MoveSpeed, rb.velocity.y);
        }
        else
        {
            transform.Translate(new Vector2(-mSpeed, 0) * Time.deltaTime);
        }
    }

    private void Shoot()
    {
        if(mAttackType == GolemData.attackType.Ranged)
        {
            if (currentTime > 0.0f)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                currentTime = shootingInterval;
                 Rigidbody2D mBullet = Instantiate(bullet, muzzle.position, transform.rotation);
                if (!reverse)
                {
                    mBullet.velocity = new Vector2(bulletSpeed, 0);
                }
                else
                {
                    mBullet.velocity = new Vector2(-bulletSpeed, 0);
                }
               
            }
        }
    }

    private void KillEnemy()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("damage TAKEN");
    }

    public void SelfRegen()
    {
        if (selfGeren)
        {
            if (health < maxHealth && mTime > 1)
            {
                health++;
                if (health > maxHealth)
                {
                    health = maxHealth;
                }
                mTime = 0;
            }
            else
            {
                mTime += Time.deltaTime;
            }
        }
    } 
}
