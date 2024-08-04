using UnityEngine;
using UnityEngine.UI;
//Written by Filip
public class ScaleController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    public float scaleFactor = 1f; 
    public Text scaleFactorText;
    public Text playerScaleText;

    private PickupObjectController poc;
    private CameraMove cm;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket)) // Decrement scaleFactor on [ press
        {
            scaleFactor = Mathf.Max(0.25f, scaleFactor - 0.25f);
            UpdateScaleFactorText();
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket)) // Increment scaleFactor on ] press
        {
            scaleFactor += 0.25f;
            UpdateScaleFactorText();
        }
        else if (Input.GetKeyDown(KeyCode.R)) // Increment scaleFactor on ] press
        {
            ResetPlayerScale();
        }

    }

    private void Start()
    {
        UpdateScaleFactorText(); // Initialize UI text on start
        poc = FindObjectOfType<PickupObjectController>();
        cm = FindObjectOfType<CameraMove>();
    }

    public void UpdateScaleFactorText()
    {

        scaleFactorText.text = "Scale Factor: " + scaleFactor.ToString("F2"); // F2 formats to 2 decimal places
        playerScaleText.text = "Player Scale: " + player.transform.localScale[0].ToString("F2"); // F2 formats to 2 decimal places
    }


    public float GetPlayerScale()
    {
        return player.transform.localScale[0];
    }

    private void ResetPlayerScale()
    {
        player.transform.localScale = new Vector3(1, 1, 1);
        UpdateScaleFactorText();
        poc.UpdateParamsOnScale(1);
        cm.moveSpeed = 5.0f;
        cm.jumpHeight = 1.5f;
    }
}
