using UnityEngine;

public class MonsterAppear : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject monster;
    [SerializeField] private Collider triggerCollider;

    [Header("Settings")]
    [Tooltip("Prevents repeated triggering")]
    [SerializeField] private bool disableAfterActivation = true;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (monster != null)
        {
            monster.SetActive(true);
        }

        if (triggerCollider != null && disableAfterActivation)
        {
            triggerCollider.enabled = false;
        }
    }
}