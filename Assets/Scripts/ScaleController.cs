using UnityEngine;
using UnityEngine.UI;
//Written by Filip
public class ScaleController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    public float scaleFactor = 1.0f; 
    public Text scaleFactorText;
    public Text playerScaleText;
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
        
    }

    private void Start()
    {
        UpdateScaleFactorText(); // Initialize UI text on start
    }

    public void UpdateScaleFactorText()
    {
           
        scaleFactorText.text = "Scale Factor: " + scaleFactor.ToString("F2"); // F2 formats to 2 decimal places
        playerScaleText.text = "Player Scale: " + player.transform.localScale[0].ToString("F2"); // F2 formats to 2 decimal places
    }
}
