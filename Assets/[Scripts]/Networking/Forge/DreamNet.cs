using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.SimpleJSON;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using System.Net;
using System.IO;
using System;
using System.Text;

public class DreamNet : DreamNetBehavior
{
    public static DreamNet instance;


    [Header("Server Stuff")]
    [SerializeField] string masterServerIP = "127.0.0.1";
    [SerializeField] ushort masterServerPort = 15940;
    [Space]
    [SerializeField] string serverIP = "127.0.0.1";
    [SerializeField] ushort serverPort = 15937;
    [Space]
    [SerializeField] string natServerIP = "127.0.0.1";
    [SerializeField] ushort natServerPort = 15941;
    [Space]
    [SerializeField] string serverID = "Gooseberries";
    [SerializeField] string serverName = "DEV: Gooseberries";
    [SerializeField] string type = "whatever";
    [SerializeField] string mode = "ffa";
    [SerializeField] string comment = "Nice comment";

    public bool useNAT;
    bool useElo = false;
    int eloRequired = 0;
    bool loggedIn;

    [Header("Objects")]
    [SerializeField] NetworkManager networkManager;
    private HttpWebResponse response;
    LoginInfo loginInfo;

    [Header("Interface")]
    [SerializeField] TMPro.TMP_InputField emailField;
    [SerializeField] TMPro.TMP_InputField passField;
    [SerializeField] Button startButton;

    private void Awake()
    {
        if (networkManager == null)
            networkManager = GetComponent<NetworkManager>();

        if (instance == null || instance != this)
            instance = this;

        UDPServer netWorker = new UDPServer(20);
        networkManager.Initialize(netWorker);
        Rpc.MainThreadRunner = MainThreadManager.Instance;

        InvokeRepeating("LogStatus", 0, 0.25f);
    }

    void LogStatus()
    {
        if(startButton != null)
        {
            if (!loggedIn)
                startButton.interactable = false;
            else
                startButton.interactable = true;
        }
        else
        {
            CancelInvoke("Logstatus");
        }
    }

    public void OnHostClicked()
    {
        UDPServer server = new UDPServer(20);

        if (useNAT)
            server.Connect("127.0.0.1", serverPort, natServerIP, natServerPort);
        else if (!useNAT)
            server.Connect("127.0.0.1", serverPort);


        JSONNode masterServerData = networkManager.MasterServerRegisterData(server, serverID, serverName, type, mode, comment, useElo, eloRequired);
        networkManager.Initialize(server, masterServerIP, masterServerPort, masterServerData);

        if (networkManager.Networker.IsConnected)
            Debug.Log("Hosted a server");

        else if (!networkManager.Networker.IsConnected)
            Debug.Log("Hosting failed");

        networkManager.Networker.Me.Name = loginInfo.username;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //NetworkManager.networkSceneLoaded unreliable
        SceneManager.activeSceneChanged += (Scene arg0, Scene arg1) =>
        {
            Debug.Log("Switching scene");
            PlayerBehavior player = NetworkManager.Instance.InstantiatePlayer(0, null, null, true);
            player.GetComponent<Player>().playerInfo = loginInfo;

        };
    }

    public void OnJoinClicked()
    {
        UDPClient client = new UDPClient();

        if (useNAT)
            client.Connect(serverIP, serverPort, natServerIP, natServerPort);
        else if (!useNAT)
            client.Connect(serverIP, serverPort);


        networkManager.Initialize(client);

        networkManager.Networker.serverAccepted += (sender) =>
        {
            MainThreadManager.Run(() =>
            {
                Debug.Log("Connected to server");
                NetworkObject.Flush(sender);

                //networkManager.Networker.Me.Name = loginInfo.username;

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                SceneManager.activeSceneChanged += (Scene arg0, Scene arg1) =>
                {
                    Debug.Log("Switching scene");
                    PlayerBehavior player = NetworkManager.Instance.InstantiatePlayer(0, null, null, true);
                    player.GetComponent<Player>().playerInfo = loginInfo;
                };
            });
        };
    }

    public void Login()
    {
        if (emailField.text != null && passField.text != null)
        {
            try
            {
                HttpWebRequest req;
                req = (HttpWebRequest)WebRequest.Create("http://blueparrotgames.com/DreamCore/Login.php");
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";

                byte[] postBytes = Encoding.ASCII.GetBytes("email=" + emailField.text + "&password=" + passField.text);
                req.ContentLength = postBytes.Length;

                Stream reqStream = req.GetRequestStream();
                reqStream.Write(postBytes, 0, postBytes.Length);
                reqStream.Close();

                response = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                loginInfo = LoginInfo.CreateFromJson(reader.ReadToEnd());

                if (loginInfo != null)
                {
                    PlayerPrefs.SetString("PlayerName", loginInfo.username);
                    Debug.Log(loginInfo.username);
                    loggedIn = true;
                }
                else
                {
                    Debug.Log("Username or password is incorrect!");
                }

                reader.Close();
            }
            catch (WebException e)
            {
                Debug.Log("ERROR: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("You need to enter login info.");
        }
    }

}

[Serializable]
public class LoginInfo
{
    public int id;
    public string username;
    public string email;
    public string password;

    public static LoginInfo CreateFromJson(string jsonString)
    {
        try
        {
            return JsonUtility.FromJson<LoginInfo>(jsonString);

        }
        catch (Exception)
        {
            return null;
        }
    }
}