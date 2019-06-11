using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour {
    #region Components
    public Button phoneBTN;
    public GameObject phonePanel;

    public GameObject cryptoPanel;
    public GameObject infoPanel;
    #endregion

    #region Attributes
    #endregion

    #region States
    private bool showingPhone;
    #endregion

    void Start () {
        phoneBTN.onClick.AddListener(() => promptPhone());
        phonePanel.SetActive(false);
        cryptoPanel.SetActive(false);
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
            promptPhone();

        if(Input.GetKeyDown(KeyCode.I)) {
            cryptoPanel.SetActive(!cryptoPanel.activeSelf);
            infoPanel.SetActive(!cryptoPanel.activeSelf);         
        }
	}

    private void promptPhone()
    {
        var color = phoneBTN.image.color;
        color.a = !showingPhone ? .5f : 1;
        phoneBTN.image.color = color;

        showingPhone = !showingPhone;
        phonePanel.SetActive(showingPhone);

        if (showingPhone)
            promptCrypto(false);
    }

    private void promptPhone(bool result)
    {
        var color = phoneBTN.image.color;
        color.a = result ? .5f : 1;
        phoneBTN.image.color = color;

        phonePanel.SetActive(result);

        if (result)
            promptCrypto(false);
    }

    private void promptCrypto()
    {
        cryptoPanel.SetActive(!cryptoPanel.activeSelf);
        infoPanel.SetActive(!cryptoPanel.activeSelf);

        if (cryptoPanel.activeSelf)
            promptPhone(false);
    }

    private void promptCrypto(bool result)
    {
        cryptoPanel.SetActive(result);
        infoPanel.SetActive(!result);

        if (result)
            promptPhone(false);
    }
}
