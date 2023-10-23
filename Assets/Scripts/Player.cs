using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    public GameObject splashEffect;
    private MeshRenderer meshRenderer;
    [SerializeField] private float _moveSpeed = 500f;
    private float _speedLimit = 5f;
    [SerializeField] private float _bounceSpeed = 250f;

    public PlayerState playerState = PlayerState.Prepare;

    private int currentBrokenPlatforms, totalPlatforms;

    public GameObject _fireEffect;
    public GameObject winEffect;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        currentBrokenPlatforms = 0;
    }

    void Start()
    {
        totalPlatforms = FindObjectsOfType<PlatformController>().Length;
        Events.FireEffect += FireEffect;
        Events.Color += ColorSet;
    }
    private void OnDestroy()
    {
        Events.FireEffect -= FireEffect;
        Events.Color -= ColorSet;
    }
    void Update()
    {
        if (playerState == PlayerState.Play)
        {
            ClickCheck();
            Events.OverPowerFill?.Invoke();
        }
    }

    void FixedUpdate()
    {
        BallMovement();
    }
    void ColorSet(Color color) => meshRenderer.material.color = color;
    private void BallMovement()
    {
        if (playerState == PlayerState.Play)
        {
            if (Input.GetMouseButton(0) && GameManager.Instance._isClicked == true)
            {
                _rb.velocity = new Vector3(0, -_moveSpeed * Time.fixedDeltaTime, 0);
            }
        }

        if (_rb.velocity.y > _speedLimit)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _speedLimit, _rb.velocity.z);
        }
    }

    public void ClickCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance._isClicked = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            GameManager.Instance._isClicked = false;
        }
    }

    public void IncreaseScore()
    {
        currentBrokenPlatforms++;
        if (!GameManager.Instance._isOverPowered) Events.Score?.Invoke(1);
        else Events.Score?.Invoke(2);
    }

    void OnCollisionEnter(Collision target)
    {
        if (!GameManager.Instance._isClicked)
        {
            _rb.velocity = new Vector3(0, _bounceSpeed * Time.deltaTime, 0);
            if (!target.gameObject.CompareTag("Finish"))
            {
                GameObject splash = Instantiate(splashEffect);
                splash.transform.SetParent(target.transform);
                splash.transform.localEulerAngles= new Vector3(90, Random.Range(0, 359), 0);
                float randomScale = Random.Range(0.18f, 0.25f);
                splash.transform.localScale = new Vector3(randomScale, randomScale, 1);
                splash.transform.position =
                    new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z);
                splash.GetComponent<SpriteRenderer>().color = GetComponent<MeshRenderer>().material.color;
            }
        }
        else
        {
            if (GameManager.Instance._isOverPowered)
            {
                if (target.gameObject.tag == "GoodPlatform" || target.gameObject.tag == "BadPlatform")
                {
                    IncreaseScore();
                    target.transform.parent.GetComponent<PlatformController>().BreakAllPlatforms();
                }
            }
            else
            {
                if (target.gameObject.tag == "GoodPlatform")
                {
                    IncreaseScore();
                    target.transform.parent.GetComponent<PlatformController>().BreakAllPlatforms();
                }

                if (target.gameObject.tag == "BadPlatform")
                {
                    _rb.isKinematic = true;
                    transform.GetChild(0).gameObject.SetActive(false);
                    Events.GameFinish?.Invoke(false);
                }
            }
        }
        Events.SliderFill?.Invoke(currentBrokenPlatforms / (float) totalPlatforms);

        if (target.gameObject.CompareTag("Finish") && playerState == PlayerState.Play)
        {
            playerState = PlayerState.Finish;
            Events.GameFinish?.Invoke(true);
            GameObject win = Instantiate(winEffect);
            win.transform.SetParent(Camera.main.transform);
            win.transform.localPosition = Vector3.up * 1.5f;
            win.transform.eulerAngles = Vector3.zero;
        }
    }

    void OnCollisionStay(Collision target)
    {
        if (!GameManager.Instance._isClicked || target.gameObject.CompareTag("Finish"))
            _rb.velocity = new Vector3(0, _bounceSpeed * Time.deltaTime, 0);
    }
    void FireEffect(bool temp)=> _fireEffect.SetActive(temp);
   

    
}
