/**
 *  Enemy Statue Shoot AI
 *      - Statue should stay Static and shoot in a direction if a player goes in the Line of Sight
 *
 *  USED TUTORIALS:
 *  - https://www.youtube.com/watch?v=xppompv1DBg&ab_channel=Brackeys
 */

using UnityEngine;

public class Statue_Shoot : MonoBehaviour {
    public float Range;
    public float FireRate;
    public GameObject EyePosition;

    public Material DeathMaterial;


    [Header("Projectile")]
    public GameObject Projectile;
    // public float ProjectileSpeed;
    // public float ProjectileRange;

    private Transform _player;
    private float _fireTick = 0;

    private bool _alive = true;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerManager.instance != null){
            _player = PlayerManager.instance.player.transform; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Every Frame run a Raycast
        if (_player != null){
            if(_fireTick <= 0){
                if ( Vector3.Distance(_player.position, EyePosition.transform.position) <= Range   
                    && _alive
                    && (
                            RayView(EyePosition.transform.forward)
                    //  ||  RayView(-EyePosition.transform.forward)
                    //  ||  RayView(EyePosition.transform.right)
                    //  ||  RayView(-EyePosition.transform.right)
                        )
                    ){
                        _fireTick = FireRate;
                        // Debug.Log("RAY FOUND");
                }
            }
            else{
                _fireTick = _fireTick - 1 * Time.deltaTime;
            }
        }
    }
    bool RayView(Vector3 direction){
            Vector3 location = EyePosition.transform.position;
            for(int j = 0; j < Range; j++){
                if (Vector3.Distance(location, _player.position) < 2.6) {
                    CreateProjectile(direction);
                    return true;
                }
                location += direction;
            }
        return false;
    }

    void CreateProjectile(Vector3 direction){
        GameObject projectile = Instantiate(Projectile, EyePosition.transform.position + (EyePosition.transform.forward * 1.2f), Quaternion.identity);

        Quaternion rotateY = transform.localRotation;
        // if (direction == -EyePosition.transform.forward){
        //     rotateY = transform.;
        // }
        projectile.transform.localRotation = rotateY;
        
    }



    void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(EyePosition.transform.position, EyePosition.transform.forward * Range);
        // Gizmos.DrawRay(EyePosition.transform.position, EyePosition.transform.right * Range);
        // Gizmos.DrawRay(EyePosition.transform.position, -EyePosition.transform.right * Range);
        // Gizmos.DrawRay(EyePosition.transform.position, -EyePosition.transform.forward * Range);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(EyePosition.transform.position, Range);
    }

    public void Kill(){
        _alive = false;
        this.GetComponent<MeshRenderer>().material = DeathMaterial;
    }

    private void OnTriggerEnter (Collider collider) {
        // Debug.Log("TRIGGERED");
        if (collider.transform.tag == "Player_Bullet") {
            Kill();
        }
    }
}
