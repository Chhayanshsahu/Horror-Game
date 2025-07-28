using UnityEngine;
using UnityEngine.UI;

public class PlayerButtonActionController : MonoBehaviour
{
    public Button flashlightBtn, reloadBtn, pickupBtn, interactBtn;
    private FlashLight flashlightScript;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        flashlightScript = GameObject.FindGameObjectWithTag("Flashlight").GetComponent<FlashLight>();

        flashlightBtn.onClick.AddListener(ToggleFlashlight);
        reloadBtn.onClick.AddListener(ReloadBattery);
        pickupBtn.onClick.AddListener(PickupBattery);
        interactBtn.onClick.AddListener(InteractWithObject);
    }

    void ToggleFlashlight()
    {
        flashlightScript.ToggleFlashlightByUI();
    }

    void ReloadBattery()
    {
        flashlightScript.ReloadBatteryByUI();
    }

    void PickupBattery()
    {
        Collider[] hits = Physics.OverlapSphere(player.transform.position, 2f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Battery"))
            {
                flashlightScript.batteries++;
                Destroy(hit.gameObject);
                break;
            }
        }
    }

    void InteractWithObject()
    {
        Collider[] hits = Physics.OverlapSphere(player.transform.position, 3f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Door") || hit.CompareTag("Drawer"))
            {
                Animator anim = hit.GetComponent<Animator>();
                if (anim != null)
                {
                    anim.SetTrigger("Toggle");
                }
            }
        }
    }
}