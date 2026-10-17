
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class PlayerController : MonoBehaviour
{
    public float thrustForce = 1f;

    Rigidbody2D rb;

    public GameObject boosterFlame;

    public float maxSpeed = 5f;

    private float elapsedTime = 0f;

    private float score = 0f;

    private float bestScore = 0f;

    public float scoreMultiplier = 10f;

    public UIDocument uiDocument;

    private Label scoreText;

    private Label bestScoreText;

    public GameObject explosionEffect;

    private Button restartButton;

    public GameObject Borders;

    private Vector2 moveDirection;
    private bool isPressed;

    void Start()

    {
       
        rb = GetComponent<Rigidbody2D>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        bestScoreText = uiDocument.rootVisualElement.Q<Label>("BestScoreLabel");
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.clicked += ReloadScene;
        restartButton.style.display = DisplayStyle.None;
        bestScore = PlayerPrefs.GetFloat("BestScore", 0);
        if (YG2.lang == "en") restartButton.text = "RESTART"; 
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
       
        if(YG2.lang == "ru") scoreText.text = "Счет: " + score;
        else scoreText.text = "Score: " + score;


        if (score > bestScore)
        {
            PlayerPrefs.SetFloat("BestScore", score);
            PlayerPrefs.Save();
        }

        if(YG2.lang == "ru") bestScoreText.text = "Рекорд: " + PlayerPrefs.GetFloat("BestScore", 0);
        else bestScoreText.text = "Best score: " + PlayerPrefs.GetFloat("BestScore", 0);

        Vector2 pointerPosition = Vector2.zero;
        isPressed = false;
        bool pressedThisFrame = false;
        bool releasedThisFrame = false;

        // Телефон
        if (Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;

            if (touch.press.isPressed)
            {
                pointerPosition = touch.position.ReadValue();
                isPressed = true;
            }

            pressedThisFrame = touch.press.wasPressedThisFrame;
            releasedThisFrame = touch.press.wasReleasedThisFrame;
        }

        // Компьютер
        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.isPressed)
            {
                pointerPosition = Mouse.current.position.ReadValue();
                isPressed = true;
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
                pressedThisFrame = true;

            if (Mouse.current.leftButton.wasReleasedThisFrame)
                releasedThisFrame = true;
        }

        // Рассчитываем направление в Update
        if (isPressed && Time.timeScale == 1f)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(pointerPosition);
            moveDirection = (mousePos - transform.position).normalized;
        }

        if (pressedThisFrame)
        {
            boosterFlame.SetActive(true);
        }
        else if (releasedThisFrame)
        {
            boosterFlame.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (isPressed && Time.timeScale == 1f)
        {
            transform.up = moveDirection;

            rb.AddForce(moveDirection * thrustForce);

            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (score > bestScore)
        {
            YG2.SetLeaderboard("SpriteFlightLeaderBoard", (int)score);
        }

        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(explosion, 2f);

        Destroy(gameObject);

        Destroy(Borders);

        restartButton.style.display = DisplayStyle.Flex;
        //reklama
        MusicManager.DeathCounter();
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}