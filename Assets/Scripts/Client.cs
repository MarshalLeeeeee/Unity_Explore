using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using TMPro;

public class Client : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI loginInfo;

    private float waitLoginTime;
    private float waitLogoutTime;
    private float waitReadTime;

    private GameObject[] uiNotLogin;
    private GameObject[] uiHasLogin;
    private GameObject[] uiWarning;
    

    private void Start()
    {
        uiNotLogin = GameObject.FindGameObjectsWithTag("UINotLogin");
        uiHasLogin = GameObject.FindGameObjectsWithTag("UIHasLogin");
        uiWarning = GameObject.FindGameObjectsWithTag("UIWarning");
    }

    private void OnEnable()
    {
        NetworkUtil.updateLogin = true;
    }

    private void Update()
    {
        if (NetworkUtil.toShowLoginError)
        {
            StartCoroutine(showLoginError(1.0f));
        }
        if (NetworkUtil.updateLogin)
        {
            readFromServer();
            updateLoginInfo();
            for (var i = 0; i < uiHasLogin.Length; i++) uiHasLogin[i].SetActive(NetworkUtil.isLogin);
            for (var i = 0; i < uiNotLogin.Length; i++) uiNotLogin[i].SetActive(!NetworkUtil.isLogin);
            for (var i = 0; i < uiWarning.Length; i++) uiWarning[i].SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if (NetworkUtil.toShowLoginError) NetworkUtil.toShowLoginError = false;
        if (NetworkUtil.updateLogin) NetworkUtil.updateLogin = false;
    }

    private void OnApplicationQuit()
    {
        NetworkUtil.disconnect();
    }

    public void login()
    {
        NetworkUtil.login(usernameInput.text, passwordInput.text);
    }

    public void logout()
    {
        NetworkUtil.logout();
    }

    private void updateLoginInfo()
    {
        if (NetworkUtil.isLogin) loginInfo.text = "Login as  " + NetworkUtil.username + "  ";
    }

    private void readFromServer()
    {
        if (NetworkUtil.isLogin)
        {
            NetworkUtil.waitRead = true;
            string msg = "read";
            NetworkUtil.sendMsg(msg);
        }
    }

    IEnumerator showLoginError(float timeDelay)
    {
        for (var i = 0; i < uiWarning.Length; i++) uiWarning[i].SetActive(true);
        yield return new WaitForSeconds(timeDelay);
        for (var i = 0; i < uiWarning.Length; i++) uiWarning[i].SetActive(false);
        yield return null;
    }

}
