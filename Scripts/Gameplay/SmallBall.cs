using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class SmallBall : MonoBehaviour
{
    [SerializeField] private float _initialSpeed = 10f;
    [SerializeField] private float _speedIncrease = 0.00002f;

    private int _hitCount = 1;
    private Rigidbody2D _rigibody;

    private void Start()
    {
        _rigibody = GetComponent<Rigidbody2D>();
        StartBall();
    }
    private void FixedUpdate()
    {
        _rigibody.velocity = new Vector2(Mathf.Clamp(_rigibody.velocity.x, -8f, 8f), Mathf.Clamp(_rigibody.velocity.y, -8f, 8f));
    }
    private void StartBall()
    {
        _rigibody.velocity = new Vector2(Random.Range(-5, 5), Random.Range(2, 5)).normalized * (_initialSpeed * _speedIncrease * _hitCount);
    }
    private void PlayerBouce(Transform transform)
    {
        _hitCount++;

        Vector2 ballPos = transform.position;
        Vector2 playerPos = transform.position;

        float xDirection, yDirection;
        if (transform.position.x > 0)
        {
            xDirection = -1;
        }
        else
        {
            xDirection = 1;        
        }
        yDirection = (ballPos.y - playerPos.y) / transform.GetComponent<Collider2D>().bounds.size.y;
        if (yDirection == 0)
            yDirection = 0.25f;
        _rigibody.velocity = new Vector2(xDirection, yDirection) * (_initialSpeed * _speedIncrease * _hitCount);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.BallSound, Random.Range(0.9f, 1.1f));
            //PlayerBouce(collision.transform);
            _hitCount++;
            _rigibody.velocity = new Vector2(_rigibody.velocity.x, _rigibody.velocity.y) * 1.01f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SmallFinish"))
        {
            if (GameState.Instance.CurrentState == GameState.State.InGame)
            {
                GameState.Instance.FinishGame();
            }
        }
    }
}
