using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour, IActorTemplate
{
    int travelSpeed;
    int health;
    int hitPower;
    GameObject actor;
    GameObject fire;

    float camTravelSpeed;
    public float CamTravelSpeed
    {
        get { return camTravelSpeed; }
        set { camTravelSpeed = value; }
    }

    float movingScreen;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public GameObject Fire
    {
        get { return fire; }
        set { fire = value; }
    }

    GameObject _Player;
    float width;
    float height;

    // Start is called before the first frame update
    void Start()
    {
        height = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).y - .5f);
        width = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).x - .5f);
        _Player = GameObject.Find("_Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            Movement();
            Attack();
        }
    }

    public void ActorStats(SOActorModel actorModel)
    {
        health = actorModel.health;
        travelSpeed = actorModel.speed;
        hitPower = actorModel.hitPower;
        fire = actorModel.actorBullets;

    }

    public void Die()
    {
        Destroy(this.gameObject);
        GameManager.Instance.LifeLost();
    }

    public int SendDamage()
    {
        return hitPower;
    }

    public void TakeDamage(int incomingDamage)
    {
        health -= incomingDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (health >= 1)
            {
                //shield that the player can buy
                //if player has a shield, destroy it
                if (transform.Find("Armor Piece(Clone)"))
                {
                    Destroy(transform.Find("Armor Piece(Clone)").gameObject);   
                    health -= other.GetComponent<IActorTemplate>().SendDamage();
                }
                else
                {
                    health -= 1;
                }
            }
            if (health <= 0)
            {
                Die();
               
            }
        }
    }

    void Movement()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (transform.localPosition.x < width + width / 0.9f)
            {
                transform.localPosition += new Vector3(Input.GetAxisRaw("Horizontal")
                    * Time.deltaTime * travelSpeed, 0, 0);
            }
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (transform.localPosition.x > width + width / 6)
            {
                transform.localPosition += new Vector3(Input.GetAxisRaw("Horizontal")
                    * Time.deltaTime * travelSpeed, 0, 0);
            }
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            if (transform.localPosition.y > -height / 3f)
            {
                transform.localPosition += new Vector3(0, Input.GetAxisRaw("Vertical") * Time.deltaTime * travelSpeed, 0);
            }
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            if (transform.localPosition.y < height / 2.5f)
            {
                transform.localPosition += new Vector3(0, Input.GetAxisRaw("Vertical") * Time.deltaTime * travelSpeed, 0);
            }
        }
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = GameObject.Instantiate(fire,transform.position, Quaternion.Euler
                (new Vector3(0, 0, 0))) as GameObject;
            bullet.transform.SetParent(_Player.transform);
            bullet.transform.localScale = new Vector3(20, 20, 20);
            Debug.Log("SHOOT");
        }
    }

}

