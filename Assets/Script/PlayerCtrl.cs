using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerCtrl : MonoBehaviour
{
    /******* Activity *******/
    public GameObject mainCam;
    private float moveSpeed = 35.0f;
    private float runSpeed = 4.0f;
    private float positionX = 0.0f;
    private float accel = 0.5f;

    /******* UI *******/
    public TMP_Text GameName;
    public Text notice;
    public GameObject End;
    public int countMap;
    private float runtime = 0.0f;
    public Transform buttonScale;
    private Vector3 defaultScale;

    /******* Flag *******/
    private bool reStartFlag = false;
    private bool jumpItemFlag = false;
    private bool shieldItemFlag = false;
    private bool attackItemFlag = false;
    private bool decelerationItemFlag = false;
    private bool startFlag = false;

    /******* Cursor *******/
    public Image CursorGaugeImage;
    private Vector3 ScreenCenter;
    private float GaugeTimer;
    bool isTriggered = false;

    /******* Audio *******/
    private AudioSource audioSource;
    public AudioClip jumpItemAudioClip;
    public AudioClip attackItemAudioClip;
    public AudioClip shieldItemAudioClip;

    void Start()
    {
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        audioSource = this.gameObject.GetComponent<AudioSource>();
        defaultScale = buttonScale.localScale;
        StartCoroutine(SpeedUp());
    }

    void Update()
    {
        Gauge();
        if (startFlag)
        {
            runtime += Time.deltaTime;
            UIUpdate(); // UI 업데이트
            UsingItem(); // 아이템 사용
            GameStart(); // 시작, 재시작
        }
    }

    void Gauge()
    {
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
        RaycastHit hit;
        CursorGaugeImage.fillAmount = GaugeTimer;

        isTriggered = Input.GetMouseButtonDown(0);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {

            if (hit.collider.CompareTag("StartButton"))
            {
                hit.transform.localScale = defaultScale * 1.2f;

                GaugeTimer += 0.33f * Time.deltaTime;
                if (GaugeTimer >= 1.0f || isTriggered)
                {
                    startFlag = true;
                    hit.transform.gameObject.SetActive(false);
                    CursorGaugeImage.gameObject.SetActive(false);

                    GaugeTimer = 0.0f;
                    isTriggered = false;
                }
            } 
            else if (hit.collider.CompareTag("BGMButton"))
            {
                hit.transform.localScale = defaultScale * 1.2f;
                GaugeTimer += 0.33f * Time.deltaTime;
                if (GaugeTimer >= 1.0f || isTriggered)
                {
                    audioSource.clip = hit.transform.GetComponent<AudioSource>().clip;
                    audioSource.Play();

                    GaugeTimer = 0.0f;
                    isTriggered = false;
                }
            }
            else
            {
                if (!startFlag)
                {
                    GameObject.FindGameObjectWithTag("StartButton").transform.localScale = defaultScale;
                    GameObject[] BGMButtons = GameObject.FindGameObjectsWithTag("BGMButton");
                    for (int i = 0; i < BGMButtons.Length; i++)
                    {
                        BGMButtons[i].transform.localScale = defaultScale;
                    }
                }

                GaugeTimer = 0.0f;
                isTriggered = false;
            }
        }
        else
        {
            if (!startFlag)
            {
                GameObject.FindGameObjectWithTag("StartButton").transform.localScale = defaultScale;
                GameObject[] BGMButtons = GameObject.FindGameObjectsWithTag("BGMButton");
                for (int i = 0; i < BGMButtons.Length; i++)
                {
                    BGMButtons[i].transform.localScale = defaultScale;
                }
            }

            GaugeTimer = 0.0f;
            isTriggered = false;
        }
    }

    void UIUpdate() // UI 업데이트
    {
        GameName.transform.gameObject.SetActive(false);
        notice.transform.gameObject.SetActive(true);
        notice.text = "플레이 시간 : " + (int)runtime + "\n" + "지나간 마을 : " + countMap + "\n" + "현재 속도 : " + runSpeed + "\n";
    }

    void UsingItem() // 아이템 사용
    {
        if (jumpItemFlag && Input.GetMouseButtonDown(0))
        {
            this.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * 400);
            audioSource.PlayOneShot(jumpItemAudioClip);
            jumpItemFlag = false;
        }
        else if (attackItemFlag && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.CompareTag("Car"))
                {
                    audioSource.PlayOneShot(attackItemAudioClip);
                    Destroy(hit.transform.gameObject);
                }
            }

            attackItemFlag = false;
        } 
        else if (decelerationItemFlag)
        {
            runSpeed = 4.0f;
            decelerationItemFlag = false;
        }
    }

    void GameStart() // 재시작
    {
        if (!reStartFlag && startFlag)
        {
            Moving();
            Run();
        }
        else
        {
            End.SetActive(true);
        }
    }

    void Run()
    {
        this.transform.position += this.transform.forward * runSpeed * Time.deltaTime;
    }

    void Moving()
    {
        positionX = this.transform.position.x - mainCam.transform.rotation.z * moveSpeed / 90.0f;

        if (positionX < -5.0f)
        {
            positionX = -5.0f;
        }
        else if (positionX > 5.0f)
        {
            positionX = 5.0f;
        }

        this.transform.position = new Vector3(positionX, this.transform.position.y, this.transform.position.z);
    }

    IEnumerator delay()
    {
        audioSource.PlayOneShot(shieldItemAudioClip);
        yield return new WaitForSeconds(0.3f);
        shieldItemFlag = false;
    }

    IEnumerator SpeedUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);

            if (startFlag)
            {
                if (runSpeed < 10.0f)
                {
                    runSpeed += accel;
                }

                if (runSpeed >= 10.0f)
                {
                    runSpeed = 10.0f;
                }
                else if (reStartFlag)
                {
                    runSpeed = 0.0f;
                    break;
                }
            }
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("SampleScene");
    }

    void OnCollisionEnter(Collision other)
    {
        if (!shieldItemFlag && other.transform.CompareTag("Car"))
        {
            reStartFlag = true;
            StartCoroutine(GameOver());
        } 
        else if (shieldItemFlag && other.transform.CompareTag("Car"))
        {
            StartCoroutine(delay());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("JumpItem"))
        {
            jumpItemFlag = true;
        } 
        else if (other.transform.CompareTag("ShieldItem"))
        {
            shieldItemFlag = true;
        }
        else if (other.transform.CompareTag("AttackItem"))
        {
            attackItemFlag = true;
        } 
        else if (other.transform.CompareTag("DecelerationItem"))
        {
            decelerationItemFlag = true;
        }
    }
}
