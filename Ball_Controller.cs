using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce;

    [Header("UI")]
    public TMP_Text coinText;
    public TMP_Text levelText;

    [Header("Private Variables")]
    private int coin;
    private Rigidbody rb;
    private float xInput;
    private float zInput;
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coin = PlayerPrefs.GetInt("Coin");
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        Move();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.SetInt("Coin", coin = 0);
            coinText.text = "0";
            PlayerPrefs.DeleteAll();
        }
    }

    public void Move()
    {
        rb.AddForce(new Vector3(xInput, 0, zInput) * speed);

        if (Input.GetKey(KeyCode.B))
        {
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void Setting()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator SlowDown()
    {
        speed = 2f;
        Debug.Log("Speed effect started");

        yield return new WaitForSeconds(5);

        speed = 5f;
        Debug.Log("Speed effect ended");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Graound"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            coin++;

            PlayerPrefs.SetInt("Coin", coin);
            coinText.text = coin.ToString();
        }

        if (other.gameObject.CompareTag("Red_Coin"))
        {
            PlayerPrefs.SetInt("Coin", coin = 0);
            coinText.text = "0";

            Debug.Log("Coin reset");
        }

        if (other.gameObject.CompareTag("-Speed"))
        {
            StartCoroutine(SlowDown());
        }
    }
}