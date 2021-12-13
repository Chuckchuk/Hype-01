using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Projectile : MonoBehaviour {
    public float ProjectileSpeed = 2f;
    public float ProjectileRange = 5f;

    // public Vector3 Direction;
    // public Quaternion Rotation;

    private float _lifespan = 5f;

    void Awake(){
        // transform.rotation = Rotation;
        _lifespan = ProjectileRange;
    }

    void Update(){
        transform.position += transform.forward * (ProjectileSpeed * Time.deltaTime);

        if (_lifespan <= 0) {
            Destroy(gameObject);
        }

        _lifespan -= (1 * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision){
        // if (collision.transform.tag == "Enemy") {
        //     collision.gameObject.GetComponent<Statue_Shoot>().Kill();
        // }
        Debug.Log("Hit " + collision.ToString());
    }
}
 