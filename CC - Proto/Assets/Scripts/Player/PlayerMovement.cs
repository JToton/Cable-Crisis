using UnityEngine;
[RequireComponent(typeof(LineRenderer))]

public class PlayerMovement : MonoBehaviour
{
    public float PlayerSpeed = 6f;            // The PlayerSpeed that the player will move at.
    public float PlayerSprint = 8f;

    public GameObject startObject;  //e.g. the player
    public GameObject endObject;    //e.g. the battery

    private LineRenderer tether;
    private LineRenderer tether2;
    Color c1 = Color.white;
    Color c2 = new Color(1, 1, 1, 0);


    GameObject battery;
    GameObject battery2;
    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator animate;                      // Reference to the animator component.
    Rigidbody PlayerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.

    public bool batteryInRange;

    public bool tetherOn;
    public bool tether2On;

    public bool controller;

    void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        animate = GetComponent<Animator>();
        PlayerRigidbody = GetComponent<Rigidbody>();

        battery = GameObject.FindGameObjectWithTag("battery");
        battery2 = GameObject.FindGameObjectWithTag("battery2");

        tether = GetComponent<LineRenderer>();
        tether.material = new Material(Shader.Find("Sprites/Default"));
        tether.SetColors(c1, c2);

        tether2 = GetComponent<LineRenderer>();
        tether2.material = new Material(Shader.Find("Sprites/Default"));
        tether2.SetColors(c1, c2);
    }

    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == battery)  //check what to attack -- make sure its the player --
        {
            batteryInRange = true;  //set in range to attack
            createTether();
        }
    }
    */

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == battery)
        {
            batteryInRange = true;  //set to no longer in range to attack
            createTether();
        }
        if (other.gameObject == battery2)
        {
            batteryInRange = true;  //set to no longer in range to attack
            createTether2();
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == battery)
        {
            batteryInRange = false;  //set to no longer in range to attack
            disableTether();
        }
        if (other.gameObject == battery2)
        {
            batteryInRange = false;  //set to no longer in range to attack
            disableTether2();
        }
    }

    void FixedUpdate()
    {
        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float rh = Input.GetAxisRaw("RStick X");
        float rv = Input.GetAxisRaw("RStick Y");

        // Move the player around the scene.
        Move(h, v);

        // Turn the player to face the mouse cursor.
        Turning(rh, rv);

        // Animate the player.
        Animating(h, v);

        if (tetherOn == true)
        {
            tether.SetPosition(1, PlayerRigidbody.transform.position);
            tether.SetPosition(0, battery.transform.position);
        }
        if (tether2On == true)
        {
            tether2.SetPosition(0, PlayerRigidbody.transform.position);
            tether2.SetPosition(1, battery2.transform.position);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Sprint(h,v);
        }
    }

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the PlayerSpeed per second.
        movement = movement.normalized * PlayerSpeed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        PlayerRigidbody.MovePosition(transform.position + movement);
    }

    void Sprint(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the PlayerSpeed per second.
        movement = movement.normalized * PlayerSprint * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        PlayerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning(float h, float v)
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            if (controller)
            {
                Vector3 dir = Vector3.right * h + Vector3.forward * v;
                if (dir.sqrMagnitude > 0f)
                {
                    PlayerRigidbody.MoveRotation(Quaternion.LookRotation(dir, Vector3.up));
                }
            }
            else
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = floorHit.point - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

                // Set the player's rotation to this new rotation.
                PlayerRigidbody.MoveRotation(newRotation);
            }
        }
    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        animate.SetBool("IsWalking", walking);
    }

    void createTether()
    {
        tether.enabled = true;
        tetherOn = true;
        tether.SetPosition(1, PlayerRigidbody.transform.position);
        tether.SetPosition(0, battery.transform.position);
        //SetPosition(0, transform.position);
    }

    public void disableTether()
    {
        tether.enabled = false;
        tetherOn = false;
    }

    void createTether2()
    {
        tether2.enabled = true;
        tether2On = true;
        tether2.SetPosition(0, PlayerRigidbody.transform.position);
        tether2.SetPosition(1, battery2.transform.position);
        //SetPosition(0, transform.position);
    }

    public void disableTether2()
    {
        tether2.enabled = false;
        tether2On = false;
    }
}
