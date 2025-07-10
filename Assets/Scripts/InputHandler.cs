using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private InputAction mouseClick;
    [SerializeField] private InputAction mousePosition;

    private LayerMask currentLayer;
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }
    private void OnEnable()
    {
        mouseClick.started += OnClick;
        mouseClick.Enable();
        mousePosition.Enable();
    }

    private void OnDisable()
    {
        mouseClick.started -= OnClick;
        mouseClick.Disable();
        mousePosition.Disable();
    }
    public void SetLayer(int layerNumber)
    {
        string layerName = LayerMask.LayerToName(layerNumber);
        currentLayer = LayerMask.GetMask(layerName);
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        Vector2 screenPos = mousePosition.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(screenPos);
        var rayHit = Physics2D.GetRayIntersection(ray,Mathf.Infinity,currentLayer);

        if (rayHit.collider != null)
        {
            // // IClickable clickable = rayHit.collider.GetComponent<IClickable>();

            // if (clickable == null) return;
            // clickable.OnClickable();
        }
    }
}