using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class KeyPad : MonoBehaviour
{
    public GameObject player;
    public GameObject Keypad;
    public GameObject hud;
    public GameObject inv;
    public GameObject Key;
    

    public TMP_Text textOB;
    public string answer = "0000";
    public AudioSource button;
    public AudioSource correct;
    public AudioSource wrong;

   public GameObject _lock;

    public float openAngle = 90f;
    public float rotationSpeed = 2f;
   
    private Quaternion openRoation;
    private bool isLocked;

    void Start()
    {
        Keypad.SetActive(false);
        Key.SetActive(false);
        isLocked = false;
        openRoation = Quaternion.Euler(_lock.transform.eulerAngles + new Vector3(0, openAngle, 0)) ;
    }

    public void Number(int number)
    {
        if (textOB.text == "Unlocked" || textOB.text == "Wrong") return;
        if (textOB.text.Length < 4)
        {
            textOB.text += number.ToString();
            button.Play();
        }
    }

    public void Enter()
    {
        if (textOB.text == answer)
        {
            correct.Play();
            textOB.text = "Unlocked";
            Invoke("Exit", 1.5f);
            isLocked = true;
            Key.SetActive(true);
            
        }
        else
        {
            wrong.Play();
            textOB.text = "Wrong";
        }
    }

    public void Clear()
    {
        textOB.text = "";
        button.Play();
    }

    public void Exit()
    {
        Keypad.SetActive(false);
        
        inv.SetActive(true);
        hud.SetActive(true);
        player.GetComponent<SimpleFPS>().enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Keypad.activeInHierarchy)
        {
            hud.SetActive(false);
            inv.SetActive(false);
            player.GetComponent<SimpleFPS>().enabled = false;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (isLocked)
        {
            _lock.transform.rotation = Quaternion.Slerp(_lock.transform.rotation, openRoation, Time.deltaTime * rotationSpeed);
            
        }
    }
}