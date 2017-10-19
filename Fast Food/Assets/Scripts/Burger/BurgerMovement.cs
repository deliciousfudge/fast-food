using UnityEngine;

public class BurgerMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public float m_StartingSpeed = 24f;
    public float m_TurnSpeed = 90f;
    //public AudioSource m_MovementAudio;
    public AudioClip m_EngineIdling;
    public AudioClip m_EngineDriving;
    public float m_PitchRange = 0.2f;
    public float m_BoostDuration = 40.0f;
    public static string[] m_PickupItems = { "Fry", "Avo", "Ket", "Mush", "Soda" };
    public GameObject FryLauncher;
    public GameObject Fry;
    public GameObject AvoLauncher;
    public GameObject AvoPip;
    public GameObject KetchupBottle;
    public GameObject Mushroom;
    public GameObject Soda;
    public GameObject[] Wheels;

    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValue;
    private float m_OriginalPitch;
    private float m_CurrentSpeed;
    private string m_PickupEquipped = null;

    GameObject fryLauncher;
    GameObject fryOne;
    GameObject fryTwo;
    GameObject fryThree;
    GameObject avoLauncher;
    GameObject avoPip;
    GameObject ketchupBottle;
    GameObject mushroom1;
    GameObject mushroom2;
    GameObject mushroom3;
    private int m_MushroomCount = 0;
    GameObject soda;
    float Idlex = 0.01f;
    bool[] bShoot = { false, false, false };
    int iSelectedFry = 1;
    private string m_ActivateAxisName;


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        SetCurrentSpeed(m_StartingSpeed); 
    }


    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;
        m_ActivateAxisName = "Activate" + m_PlayerNumber;
    }

    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

        if (avoLauncher)
        {
            foreach (Transform child in transform)
            {
                if (child.tag == "AvoLauncher")
                {
                    child.transform.localRotation = Quaternion.Euler(320f + Mathf.Sin(Time.time * 10f) * 45f, 180f, child.transform.localRotation.z);
                }
            }
        }
        if (m_MushroomCount > 0)
        {
            foreach(Transform child in transform)
            {
                if (child.tag == "Mushroom")
                {
                    Debug.Log("Rotation time");
                    Debug.Log(child.transform.localRotation);
                    child.transform.RotateAround(transform.position, new Vector3(0, 1, 0), 90f * Time.deltaTime);
                    Debug.Log(child.transform.localRotation);
                }
            }
        }

        //shooting
        switch (m_PickupEquipped)
        {
            case "Fry":
                {
                    if (Input.GetButtonDown(m_ActivateAxisName))
                    {
                        switch (iSelectedFry)
                        {
                            case 1:
                                {
                                    fryOne.transform.SetParent(null);
                                    bShoot[0] = true;
                                    iSelectedFry = 2;
                                    break;
                                }
                            case 2:
                                {
                                    fryTwo.transform.SetParent(null);
                                    bShoot[1] = true;
                                    iSelectedFry = 3;
                                    break;
                                }
                            case 3:
                                {
                                    fryThree.transform.SetParent(null);
                                    fryLauncher.transform.SetParent(null);
                                    m_PickupEquipped = null;
                                    bShoot[2] = true;
                                    break;
                                }
                            default: break;
                        }
                    }

                    break;
                }
            default: break;
        }
        Shoot();

        // Idle animation
        Idlex += 0.5f;
        foreach (Transform child in transform)
        {
            if (child.tag != "Wheel")
            {
                child.transform.Translate(new Vector3(0, Mathf.Sin(Idlex) / 60, 0));
            }
        }
    }

    private void Shoot()
    {
        if (bShoot[0] == true)
        {
            fryOne.transform.Translate(new Vector3(0, 0, 25) * Time.deltaTime);
        }

        if (bShoot[1] == true)
        {
            fryTwo.transform.Translate(new Vector3(0, 0, 25) * Time.deltaTime);
        }

        if (bShoot[2] == true)
        {
            fryThree.transform.Translate(new Vector3(0, 0, 25) * Time.deltaTime);
        }
    }

    private void SetCurrentSpeed(float _iSpeed)
    {
        m_CurrentSpeed = _iSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Add features later
        if (collision.gameObject.tag == "SpeedPad")
        {
            m_BoostDuration = 50.0f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
     

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup") && m_PickupEquipped == null)
        {
            other.gameObject.SetActive(false);
            int iRand = (int)Random.Range(0, 5);
            m_PickupEquipped = m_PickupItems[iRand];
            Debug.Log("You equipped " + m_PickupEquipped);

            switch (m_PickupEquipped)
            {
                case "Fry":
                {
                    // Reset fry shooting.
                    bShoot[0] = false;
                    bShoot[1] = false;
                    bShoot[2] = false;
                    iSelectedFry = 1;

                    fryLauncher = Instantiate(FryLauncher) as GameObject;
                    fryOne = Instantiate(Fry) as GameObject;
                    fryTwo = Instantiate(Fry) as GameObject;
                    fryThree = Instantiate(Fry) as GameObject;

                    fryLauncher.transform.SetParent(gameObject.transform);
                    fryLauncher.transform.localPosition = new Vector3(-1.5f, 1.5f, 0f);
                    fryLauncher.transform.localRotation = Quaternion.Euler(180f, 0f, 90f);
                    
                    fryOne.transform.SetParent(gameObject.transform);
                    fryOne.transform.localPosition = new Vector3(-1.55f, 1.3f, 0.1f);
                    fryOne.transform.localRotation = Quaternion.Euler(180f, 0f, 90f);

                    fryTwo.transform.SetParent(gameObject.transform);
                    fryTwo.transform.localPosition = new Vector3(-1.55f, 1.5f, 0f);
                    fryTwo.transform.localRotation = Quaternion.Euler(180f, 0f, 90f);

                    fryThree.transform.SetParent(gameObject.transform);
                    fryThree.transform.localPosition = new Vector3(-1.55f, 1.7f, 0.2f);
                    fryThree.transform.localRotation = Quaternion.Euler(180f, 0f, 90f);

                    break;
                }

                case "Avo":
                {
                    avoLauncher = Instantiate(AvoLauncher) as GameObject;
                    avoPip = Instantiate(AvoPip) as GameObject;

                    avoLauncher.transform.SetParent(gameObject.transform);
                    avoLauncher.transform.localPosition = new Vector3(-1.9f, 2f, -0.75f);
                    avoLauncher.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                    avoLauncher.tag = "AvoLauncher";

                    avoPip.transform.SetParent(gameObject.transform);
                    avoPip.transform.localPosition = new Vector3(-1.9f, 2.5f, 0.1f);
                    avoPip.transform.localRotation = Quaternion.Euler(180f, 0f, 90f);

                    break;
                }

                case "Ket":
                {
                    ketchupBottle = Instantiate(KetchupBottle) as GameObject;
                    ketchupBottle.transform.SetParent(gameObject.transform);
                    ketchupBottle.transform.localPosition = new Vector3(-2f, 1.5f, 0.5f);
                    ketchupBottle.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

                    break;
                }

                case "Mush":
                {
                    Debug.Log("Add dem mushies");
                    mushroom1 = Instantiate(Mushroom) as GameObject;
                    mushroom2 = Instantiate(Mushroom) as GameObject;
                    mushroom3 = Instantiate(Mushroom) as GameObject;

                    mushroom1.transform.SetParent(gameObject.transform);
                    mushroom1.transform.localPosition = new Vector3(-2.5f, 1.5f, 0f);
                    mushroom1.transform.localRotation = Quaternion.Euler(180f, 0f, 180f);
                    mushroom1.tag = "Mushroom";

                    mushroom2.transform.SetParent(gameObject.transform);
                    mushroom2.transform.localPosition = new Vector3(2.5f, 1.5f, 0f);
                    mushroom2.transform.localRotation = Quaternion.Euler(180f, 180f, 180f);
                    mushroom2.tag = "Mushroom";

                    mushroom3.transform.SetParent(gameObject.transform);
                    mushroom3.transform.localPosition = new Vector3(0f, 1.5f, 2.5f);
                    mushroom3.transform.localRotation = Quaternion.Euler(180f, 90f, 180f);
                    mushroom3.tag = "Mushroom";

                    m_MushroomCount = 3;
                    break;
                }

                case "Soda":
                {
                    soda = Instantiate(Soda) as GameObject;

                    soda.transform.SetParent(gameObject.transform);
                    soda.transform.localPosition = new Vector3(-1.5f, 1.5f, 0f);
                    soda.transform.localRotation = Quaternion.Euler(180f, 0f, 90f);
                    break;
                }

                default:break;
            }
        }
    }

    private void FixedUpdate()
    {
        // Move and turn the cart.
        Move();
        Turn();
    }


    private void Move()
    {
        if (m_BoostDuration > 0)
        {
            m_CurrentSpeed = m_StartingSpeed * Mathf.Log(m_BoostDuration);
            m_BoostDuration--;
        }
        else
        {
            m_CurrentSpeed = m_StartingSpeed;
        }

        // Adjust the position of the burger based on the player's input.
        Vector3 movement = transform.forward * m_MovementInputValue * m_CurrentSpeed * Time.deltaTime;

        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

        foreach (GameObject wheel in Wheels)
        {
            wheel.transform.Rotate(new Vector3(0, 0, (m_MovementInputValue * m_CurrentSpeed * 20)) * Time.deltaTime);
        }
    }


    private void Turn()
    {
        // Adjust the rotation of the burger based on the player's input.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}