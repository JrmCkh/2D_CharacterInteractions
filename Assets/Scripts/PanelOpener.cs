using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    [SerializeField]
    private GameObject Panel;
    private bool isActive = true;
    
    public void OpenPanel()
    {
        if (this.Panel != null)
        {
            // To 'hide' the game object without disabling it, allowing it to be updated.
            this.Panel.GetComponent<Canvas>().enabled = this.isActive;
            this.isActive = !this.isActive;

            // To disable game object completely.
            // Disabled game object cannot be updated.
            /*
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);    
            */
        }
    }
}