using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrashpileController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float fadeoutDuration = 1;

    [Header("Dependancies")]
    [SerializeField] private LocalTabletManager tabletManager;
    [SerializeField] private List<GameObject> modelList;

    private readonly List<GameObject> models = new List<GameObject>(); 
    private int totalModels;
    private bool gameEnded = false;

    private SphereCollider coll;
    private AudioSource audiosource;
    private Image image;

    void Start()
    {
        InitData(modelList);
        coll = GetComponent<SphereCollider>();
        audiosource = GetComponent<AudioSource>();
        image = GetComponentInChildren<Image>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Collect") && tabletManager.ShipPartsCollected == totalModels)
        {
            Collider[] contactList = Physics.OverlapSphere(coll.bounds.center, coll.radius);

            foreach (var contact in contactList)
            {
                if (contact.gameObject.CompareTag("Player") && !gameEnded) StartCoroutine(EndGameAnimation());
            }
        }
    }

    private IEnumerator EndGameAnimation()
    {
        gameEnded = true;

        float timer = 0;
        while (timer < fadeoutDuration)
        {
            timer += Time.deltaTime;
            image.color = new Color(0,0,0, timer/fadeoutDuration);
            yield return new WaitForEndOfFrame();
        }

        audiosource.Play();
        yield return new WaitForSeconds(audiosource.clip.length);

        SceneManager.LoadScene("Credits");
    }

    private void InitData(List<GameObject> modelListLocal)
    {
        totalModels = modelListLocal.Count;

        for (var i = 0; i < totalModels; i++)
        {
            models.Add(modelListLocal[i]);
            models[i].SetActive(false);
        }
    }

    public void AddModel() {

        if (tabletManager.ShipPartsCollected != totalModels)
        {
            models[tabletManager.ShipPartsCollected - 1].SetActive(true);
        }
        else
        {
            // totalModels - 2 because the last model is going to be used
            for (int i = 0; i < totalModels - 1; i++) models[i].SetActive(false);

            // Set the last model (spaceship) active
            models[totalModels - 1].SetActive(true);
        }
    }
}
