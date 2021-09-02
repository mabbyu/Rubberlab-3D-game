using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private string enemyTag;

    [SerializeField]
    private float maxFirePower;

    [SerializeField]
    private float firePowerSpeed;

    [SerializeField]
    private RectTransform powerBar;

    private float powerBarStartWidth;

    private float firePower;
    private bool fire;

    public int karet;
    public Text karetText;

    void Start()
    {
        weapon.SetEnemyTag(enemyTag);
        weapon.Reload();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        firePower = 0;
        powerBarStartWidth =powerBar.sizeDelta.x;
    }

    void Update()
    {
        karetText.text = karet.ToString();

        if (Input.GetMouseButtonDown(0) && !fire && karet > 0)
        {
            fire = true;
            karet--;
        }

        if (Input.GetMouseButtonDown(0) && !fire && karet == 0)
        {
            fire = false;
            karet--;
        }
        
        if (fire && firePower < maxFirePower)
        {
            firePower += Time.deltaTime * firePowerSpeed;
        }

        if (fire && Input.GetMouseButtonUp(0))
        {
            weapon.Fire(firePower);
            firePower = 0;
            fire = false;
        }
        UpdateUI();
    }

    void UpdateUI() 
    {
        float percentOutOf = (firePower / maxFirePower) * 100;
        float newWidth = (percentOutOf / 100) * powerBarStartWidth;

        powerBar.sizeDelta = new Vector2(newWidth, powerBar.sizeDelta.y);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Karet"))
        {
            other.gameObject.SetActive(false);
            karet = karet + 1;
        }
    }
}