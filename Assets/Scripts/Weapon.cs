using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public TextMeshProUGUI bulletNumberLabel;
    public GameObject trailPrefab;
    public Transform firingPosition;
    public GameObject particlePrefab;

    public int bullet;
    public int totalBullet;
    public int maxBulletmagazine;

    public float damage;

    Animator animator;

    public AudioClip gunShot;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && bullet > 0)
        {
            if (animator != null)
            {
                animator.SetTrigger("Shot");
            }
            bullet--;
            Fire();
        }
        if (Input.GetButtonDown("Remove"))
        {
            if (animator != null)
            {
                animator.SetTrigger("Remove");
            }

            if (bullet >= maxBulletmagazine - bullet)
            {
                totalBullet -= maxBulletmagazine - bullet;
                bullet = maxBulletmagazine;
            }
            else
            {
                bullet += totalBullet;
                totalBullet = 0;
            }
        }

        bulletNumberLabel.text = bullet + "/" + totalBullet;
    }


    virtual protected void Fire()
    {
        RaycastFire();
    }

    void RaycastFire()
    {
        GetComponent<AudioSource>().PlayOneShot(gunShot);

        Camera cam = Camera.main;

        RaycastHit hit;
        Ray r = cam.ViewportPointToRay(Vector3.one / 2);

        Vector3 hitPosition = r.origin + r.direction * 200;
        if (Physics.Raycast(r, out hit, 1000))
        {
            hitPosition = hit.point;

            GameObject particle = Instantiate(particlePrefab);
            particle.transform.position = hitPosition;
            particle.transform.forward = hit.normal;
            if(hit.collider.tag=="Enemy")
            {
                hit.collider.GetComponent<Health>().Damage(damage);
            }
        }

        if (trailPrefab != null)
        {
            GameObject obj = Instantiate(trailPrefab);
            Vector3[] pos = new Vector3[] { firingPosition.position, hitPosition };
            obj.GetComponent<LineRenderer>().SetPositions(pos);

            StartCoroutine(RemoveTrail(obj));

        }

    }
    IEnumerator RemoveTrail(GameObject obj)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(obj);

    }
}