using UnityEngine;

public class PlayerSpawner1 : MonoBehaviour
{
     public GameObject[] avatars; // Assign same order as in selection scene

    void Start()
    {
        int index = AvatarSelector.selectedAvatarIndex;
        Instantiate(avatars[index], Vector3.zero, Quaternion.identity);
    }
}
