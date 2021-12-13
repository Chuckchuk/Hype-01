using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// USING TUTORIAL:
// https://www.youtube.com/watch?v=xenW67bXTgM&t=732s
public class Projectile01 : MonoBehaviour
{
    public float ProjectileSpeed = 1f;
    public float ProjectileRange = 5f;

    public Vector3 Direction;
    public Quaternion Rotation;

    private float _lifespan = 5f;

    void Awake(){
        transform.rotation = Rotation;
        _lifespan = ProjectileRange;
    }

    void Update(){
        transform.position += transform.forward * (ProjectileSpeed * Time.deltaTime);

        if (_lifespan <= 0) {
            Destroy(gameObject);
        }

        _lifespan -= (1 * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider){
        Debug.Log("TRIGGERED");
        if(collider.transform.tag == "Player"){
            SceneManager.LoadScene("GameOver");
        }
    }
}
