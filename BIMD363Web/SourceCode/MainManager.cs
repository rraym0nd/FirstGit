using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    private GameObject player;
    public Button replayBtn, startBTN;
    private Vector3 spawnPoint;
    public GameObject startPanel;
    public Text cryptoCount;

    private int count;

    void Start()
    {
        Instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        spawnPoint = player.transform.position;

        startBTN.onClick.AddListener(() => startGame());
        replayBtn.onClick.AddListener(() => restart());
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Alpha1))
            SceneManager.LoadScene(0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SceneManager.LoadScene(1);*/

        if (Input.GetKeyDown(KeyCode.R))
            restart();

        if (startPanel.activeSelf)
            player.SetActive(false);

        replayBtn.gameObject.SetActive(!player.activeSelf && !startPanel.activeSelf);

        //cryptoCount.text = count.ToString();
    }

    private void restart()
    {
        player.transform.position = spawnPoint;
        player.SetActive(true);
        count = 0;
    }


    public void startGame()
    {
        player.SetActive(true);
    }

    public void addToCount(int amount)
    {
        count += amount;
    }
}
