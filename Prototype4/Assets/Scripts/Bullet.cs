using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    public Vector3 direction = new Vector3(0, 0, -1);
    public float speed = 2f;

    public Vector3 velocity;

    public ParticleSystem destroyParticle;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction * speed;
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.position;

        pos += velocity * Time.fixedDeltaTime;

        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if (other.gameObject.tag == "Enemy")
        {
            Instantiate(destroyParticle.gameObject, transform.position, transform.rotation);
            CameraShake.Invoke();
            Destroy(other.gameObject); // this destroys the enemy
            Destroy(gameObject); // this destroys the bullet
        }
    }

}
