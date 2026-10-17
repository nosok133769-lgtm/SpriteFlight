
using UnityEngine.InputSystem;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Размер")]
    public float minSize = 0.5f;
    public float maxSize = 2.0f;

    [Header("Начальная скорость")]
    public float minSpeed = 50f;
    public float maxSpeed = 150f;

    [Header("Ограничение скорости")]
    private float maxVelocity = 5f;

    [Header("Вращение")]
    public float maxSpinSpeed = 10f;

    [Header("Эффекты")]
    public GameObject bounceEffectPrefab;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1);

        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize;
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed);

        rb.AddTorque(randomTorque);
        rb.AddForce(randomDirection * randomSpeed);
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > maxVelocity)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxVelocity;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (bounceEffectPrefab != null)
        {
            Vector2 contactPoint = collision.GetContact(0).point;

            GameObject bounceEffect = Instantiate(
                bounceEffectPrefab,
                contactPoint,
                Quaternion.identity
            );

            Destroy(bounceEffect, 1f);
        }
    }
}
