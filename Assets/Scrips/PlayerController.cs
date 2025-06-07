using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Stats")]
    [SerializeField] private float bulletSpeed = 15f;
    [SerializeField] private float fireRate = 0.3f;

    private float nextFireTime = 0f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (mainCamera == null)
            mainCamera = Camera.main;

      
        gameObject.tag = "Player";
    }

    private void Update()
    {
        RotateTowardsMouse();

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void RotateTowardsMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, LayerMask.GetMask("Ground")))
        {
            Vector3 lookPoint = hitInfo.point;
            Vector3 direction = (lookPoint - transform.position);
            direction.y = 0f;
            if (direction.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }

    private void Shoot()
    {
       
        GameObject b = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
        Rigidbody bRb = b.GetComponent<Rigidbody>();
        bRb.velocity = transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            
            GameManager.Instance.OnPlayerDeath();
            Destroy(gameObject);
        }
    }
}
