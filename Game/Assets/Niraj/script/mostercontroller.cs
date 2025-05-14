using UnityEngine;

public class MonsterChase : MonoBehaviour
{
    public Rigidbody monsRigid;
    public Transform monsTrans, playTrans;
    public float monSpeed = 5f; // Use float for smoother speed control

    void FixedUpdate()
    {
        // Direction already updated in Update(), move forward at constant speed
        monsRigid.linearVelocity = monsTrans.forward * monSpeed;
    }

    void Update()
    {
        if (playTrans != null) // Safety check
        {
            monsTrans.LookAt(playTrans);
        }
    }
}