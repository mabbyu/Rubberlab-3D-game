using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : MonoBehaviour
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

    private bool isDead;

    //score
    public GameObject scoreText;
    public float currScore;

    public GameObject alive;
    public GameObject death;

    void Start()
    {
        //meshRenderer = GetComponent<MeshRenderer>();
        currentHealth = maxHealth;
        healthBarStartWidth = healthBar.sizeDelta.x;
        UpdateUI();
    }

    public void ApplyDamage(float damage)
    {
        if (isDead) return;

        currScore += damage;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            meshRenderer.SetActive(false);
            healthPanel.SetActive(false);

            StartCoroutine(RespawnAfterTime());
            var t = Instantiate(scoreText, transform.position, transform.rotation);
            t.GetComponent<TextMesh>().text = currScore.ToString("N0");
            GameController.instance.score += currScore;
            Destroy(gameObject);

        } 
        UpdateUI();
    }

    private IEnumerator RespawnAfterTime()
    {
        yield return new WaitForSeconds(respawnTime);
        //ResetHealth();
    }

    private void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
        meshRenderer.SetActive(true);
        healthPanel.SetActive(true);
        UpdateUI();
    }

    private void UpdateUI()
    {
        float percentOutOf = (currentHealth / maxHealth) * 100;
        float newWidth = (percentOutOf / 100) * healthBarStartWidth;

        healthBar.sizeDelta = new Vector2(newWidth, healthBar.sizeDelta.y);
    }
}