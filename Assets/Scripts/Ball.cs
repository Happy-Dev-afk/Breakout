using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public float ballSpeed = 6f;
    [SerializeField] private Rigidbody2D rigidBody2D;
    private int numberOfHits = 0;
    private int maxHits = 2;
    private float incSpeed = 0.1f;
    [SerializeField] private TextMeshProUGUI scoreText;
    private float score = 0f;
    private float lifeRemaining = 3f;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject continueText;


    private void Start()
    {
        Launch();
        livesText.text = lifeRemaining.ToString();

    }
    private void Launch()
    {
        transform.position = new Vector2(0, -4);
        int randomX = Random.Range(0, 2) == 0 ? 1 : -1;
        int y = 1;
        rigidBody2D.linearVelocity = new Vector2(randomX * ballSpeed, y * ballSpeed);

    }
    private void Update()
    {
        
        if (lifeRemaining > 0 && Time.timeScale == 0f)
        {
            if (Input.anyKeyDown)
            {
                Time.timeScale = 1f;
                continueText.SetActive(false);
                Launch();
            }
        }
    }
    private void FixedUpdate()
    {
        
        Vector2 currentVelocity = rigidBody2D.linearVelocity;
        if (Mathf.Abs(currentVelocity.x) <= 0.1f)
        {
            currentVelocity.x = currentVelocity.x < 0 ? -0.5f : 0.5f;
        }
        if (Mathf.Abs(currentVelocity.y) <= 0.1f)
        {
            currentVelocity.y = currentVelocity.y < 0 ? -0.5f : 0.5f;
        }
        rigidBody2D.linearVelocity = currentVelocity;
        
    }

    

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bricks"))
        {
            collision.gameObject.SetActive(false);
            IncSpeedAndScore(); 
            
            
        }

        if (collision.collider.CompareTag("Bricks2"))
        {
            numberOfHits++;
            if (numberOfHits == maxHits)
            {
                collision.gameObject.SetActive(false);
                numberOfHits = 0;
                IncSpeedAndScore();
            }
        }

        if (collision.collider.CompareTag("KillBar"))
        {
            continueText.SetActive(true);
            Time.timeScale = 0f;
            lifeRemaining = lifeRemaining - 1f;
            livesText.text = lifeRemaining.ToString();
            

            if (lifeRemaining <= 0)
            {
                continueText.SetActive(false);
                Time.timeScale = 0f;
                gameOverScreen.SetActive(true);
                
            }
        }
    }

    private void IncSpeedAndScore()
    {
        ballSpeed = incSpeed + ballSpeed;
        score = score + 100f;
        scoreText.text = score.ToString();
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting.");
    }

}
