using UnityEngine.EventSystems;
using UnityEngine;

// Comment
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus;

    // Layer that is walkable
    public LayerMask movementMask;

    Camera cam;
    PlayerMotor motor;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        // Comment
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButton(0))
        {
            // Getting the point the user clicks on through the camera
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            // RaycastHit is wherever the last raycast hit
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                // Get the transform of the point that was hit by the raycast on the movement mask (ground)
                motor.MoveToPoint(hit.point);

                RemoveFocus();
            }
        }

        if (Input.GetMouseButton(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                // Get the interactable component for the collider that was hit
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                // Make sure that it has an interactable compoenent
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }


    }

    void SetFocus(Interactable newFocus)
    {
        // Make sure that the focus is not already in newFocus
        if (newFocus != focus)
        {
            
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
            // Follow the focused interactable
            motor.FollowTarget(newFocus);
        }
        
        newFocus.OnFocused(transform);
    }

   void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        motor.StopFollowingTarget();
    }

}
