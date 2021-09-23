using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControlGuru : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;

    private float currentHealth;

    [SerializeField]
    private float respawnTime;

    [SerializeField]
    private GameObject healthPanel;

    [SerializeField]
    private RectTransform healthBar;

    private float healthBarStartWidth;

    public GameObject meshRenderer;

    //public GameObject deadObject;

    //public float scoreGame;

    private bool isDead;

    public AudioClip getHit;

    void Start()
    {
        //meshRenderer = GetComponent<MeshRenderer>();
        currentHealth = maxHealth;
        healthBarStartWidth = healthBar.sizeDelta.x;
        UpdateUI();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            meshRenderer.SetActive(true);
            healthPanel.SetActive(false);

            StartCoroutine(RespawnAfterTime());
        }

        UpdateUI();
    }

    public void ApplyDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

       

        GetComponent<AudioSource>().PlayOneShot(getHit);
    }

    private IEnumerator RespawnAfterTime()
    {
        yield return new WaitForSeconds(respawnTime);
        ResetHealth();
    }

    private void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
        meshRenderer.SetActive(true);
        healthPanel.SetActive(true);
    }

    private void UpdateUI()
    {
        float percentOutOf = (currentHealth / maxHealth) * 100;
        float newWidth = (percentOutOf / 100) * healthBarStartWidth;

        healthBar.sizeDelta = new Vector2(newWidth, healthBar.sizeDelta.y);
    }
}