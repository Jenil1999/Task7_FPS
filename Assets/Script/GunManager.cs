using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GunManager : MonoBehaviour
{

    public AudioSource Audio;
    public Camera FpsCamera;
    public AudioClip FireClip;
    public AudioClip ReloadClip1;
    public AudioClip ReloadClip2;
    public float Volume;
    public Animator GunAnimator;
    public bool IsGunHide = false;
    public ParticleSystem MuzzleFlash;
    public ParticleSystem ImpactOnShoot;
    public float Damage = 10f;
    public float range = 100f;
    public float ImpactForce = 10f;
    public float FireRate = 15f;

    private float NextShootTime = 0f;

    int i;

    int MaxAmmo = 6;
    int CurrentAmmo;

    public Image[] Bullets;
    public Sprite Bullet;

    GameObject ImpactGO;

    private void Start()
    {
        CurrentAmmo = MaxAmmo;
    }

    private void Update()
    {
        Fire();
        Reload();
        HideGun();

        for(i = 0; i < Bullets.Length; i++ )
        {
            if(i < CurrentAmmo)
            {
                Bullets[i].enabled = true;
            }
            else
            {
                Bullets[i].enabled = false;
            }
        }
    
    }

         public void Fire()
  
    {
        if(CurrentAmmo <= 0)
        {
            GunAnimator.SetTrigger("Reload");

            Invoke("AmmoAssignForDelay", 0.3f);
        }
        if (GunAnimator.GetCurrentAnimatorStateInfo(0).IsName("Reload") || IsGunHide)
        {
            return;
        }
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetMouseButton(0) && Time.time >= NextShootTime)
        {
            NextShootTime = Time.time + 1f / FireRate;
            CurrentAmmo--;
            Debug.Log(CurrentAmmo);
            shoot();
            GunAnimator.SetTrigger("Fire");
            MuzzleFlash.Play();
            Audio.PlayOneShot(FireClip, Volume);
        }
    }

    void AmmoAssignForDelay()
    {
        CurrentAmmo = MaxAmmo;
    }

    void shoot()
    {

        RaycastHit hit;
       if(Physics.Raycast(FpsCamera.transform.position, FpsCamera.transform.forward, out hit, range))
        {
           // Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.TakeDamage(Damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * ImpactForce);
            }

           ImpactGO = Instantiate(ImpactOnShoot.gameObject, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(ImpactGO, 0.5f); 
        }
    }

    public void HideGun()
    {
        if(!IsGunHide)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                GunAnimator.SetTrigger("Hide");
                IsGunHide = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                GunAnimator.SetTrigger("Hide");
                IsGunHide = false;
            }
        }
           
    }

    public void Reload()
    {
        if(!IsGunHide)
        {
            if (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(1))
            {
                CurrentAmmo = MaxAmmo;
                GunAnimator.SetTrigger("Reload");
            }
        }
      
    }

    public void EventReloadSound()
    {
        Audio.PlayOneShot(ReloadClip1, Volume);

    }
    public void EventReloadSound1()
    {
        Audio.PlayOneShot(ReloadClip2, Volume);

    }
}
