using UnityEngine;

public class ExchangePortal : Interactable
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip portalSound;
    private MenuManager menuManager;

    void Start() 
    {
        menuManager = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();    
    }

    public override bool Interact()
    {
        source.PlayOneShot(portalSound);
        
        menuManager.OpenExchangeUI();

        return true;
    }


}
