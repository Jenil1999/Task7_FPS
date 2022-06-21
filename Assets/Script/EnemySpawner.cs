using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject EnemyPrefab;
    public GameObject BossPrefab;

    float Xpos;
    float Zpos;

    float BossRadiousX;
    float BossRadiousY;

    public int EnemyCount;

    public int BossEnemy;

    public AudioSource audio1;
    public AudioClip JumpscareSound;
    [Range(0, 5)] public float scareVolume = 1f;


    bool IsTriggered = false;
    private void Update()
    {
        EnemySpawner1();
        
    }

    
    
    void EnemySpawner1()
    {
        if (EnemyCount < 10)
        {
            Xpos = Random.Range(9, -29);
            Zpos = Random.Range(-11, -29);

            Instantiate(EnemyPrefab, new Vector3(Xpos, 0, Zpos), Quaternion.identity);
            
            EnemyCount += 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

            if (other.gameObject.CompareTag("Player") && !IsTriggered)
            {
                audio1.PlayOneShot(JumpscareSound, scareVolume);

            for (BossEnemy = 0; BossEnemy<5;BossEnemy++)
                {
                    BossRadiousX = Random.Range(-9, 9);
                    BossRadiousY = Random.Range(-9, 9);

                    Instantiate(BossPrefab, new Vector3(BossRadiousX, 0, BossRadiousY), Quaternion.identity);
                }
            IsTriggered = true;
            }
        
           
    }

}
