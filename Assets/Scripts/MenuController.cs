using UnityEngine;
using StarterAssets;

public class MenuController : MonoBehaviour
{
    public StarterAssetsInputs inputs;
    public GameObject menuParent;
    
    void Update()
    {
        if (inputs.pause)
        {
            inputs.pause = false;
            ToggleTheTarget();  
        }
    }

	void ToggleTheTarget()
	{
        if (!menuParent.activeSelf)
        {
            inputs.cameraLocked = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Time.timeScale = 0.0f;
		}
        else
        {
            inputs.cameraLocked = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            Time.timeScale = 1.0f;
		}
        
		menuParent.SetActive(!menuParent.activeSelf);
	}
}
