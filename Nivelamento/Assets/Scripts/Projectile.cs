using UnityEngine;

public class Projectile : MonoBehaviour
{
    /*public float speed;
    public int damage;

    GameObject target;

    void Start()
    {
        speed = 5;
        damage = 3;
    }
    
    void Update()
    {
        //transform.Translate(0, 0, speed * Time.deltaTime);
        transform.LookAt(target.transform);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        //PlayerController player = other.GetComponent<PlayerController>();

        if (target != null && other == target)
        {
            Debug.Log(target.name);
            target.GetComponent<PlayerController>().Hurt(damage);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        //PlayerController player = other.GetComponent<PlayerController>();

        if (target != null && other == target)
        {
            Debug.Log(target.name);
            target.GetComponent<PlayerController>().Hurt(damage);
            Destroy(this.gameObject);
        }
    }

    public void ShootAt(GameObject target)
    {
        this.target = target; 
    }*/
    private Transform target;

    private float speed = 5;
    public int damage = 3;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        target.GetComponent<PlayerController>().Hurt(damage);
        Destroy(gameObject);
    }
}
