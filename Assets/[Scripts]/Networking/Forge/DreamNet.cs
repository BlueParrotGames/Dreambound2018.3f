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

public class DreamNet : DreamboundNetBehavior
{
    string masterServerIP = "127.0.0.1";
    ushort masterServerPort = 15940;

    string serverIP = "127.0.0.1";
    ushort serverPort = 15937;
    string serverID = "Gooseberries";
    string serverName = "DEV: Gooseberries";
    string type = "whatever";
    string mode = "ffa";
    string comment = "Nice comment";
    bool useElo = false;
    int eloRequired = 0;

    bool loggedIn;


    [Header("Objects")]
    [SerializeField] NetworkManager networkManager;
    private HttpWebResponse response;

    [Header("Interface")]
    [SerializeField] InputField nameField;
    [SerializeField] InputField passField;

    private void Awake()
    {
        if(networkManager == null)
        {
            networkManager = GetComponent<NetworkManager>();
        }

        UDPServer netWorker = new UDPServer(20);
        networkManager.Initialize(netWorker);

        Login();
    }

    public void OnHostClicked()
    {
        UDPServer server = new UDPServer(20);
        server.Connect(serverIP, serverPort);

        JSONNode masterServerData = networkManager.MasterServerRegisterData(server, serverID, serverName, type, mode, comment, useElo, eloRequired);
        networkManager.Initialize(server, masterServerIP, masterServerPort, masterServerData);

        if (networkManager.Networker.IsConnected)
            Debug.Log("Hosted a server");

        else if (!networkManager.Networker.IsConnected)
            Debug.Log("Hosting failed");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        networkManager.networkSceneLoaded += (Scene arg0, LoadSceneMode loadSceneMode) => 
        {
            Debug.Log("Switching scene");
            NetworkManager.Instance.InstantiatePlayer(0, null, null, true);
        };
    }

    public void OnJoinClicked()
    {
        UDPClient client = new UDPClient();
        client.Connect(serverIP, serverPort);

        networkManager.Initialize(client);

        networkManager.Networker.serverAccepted += (sender) =>
        {
            MainThreadManager.Run(() =>
            {
                Debug.Log("Connected to server");
                NetworkObject.Flush(sender);

                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                //SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
            });
        };
    }

    public void Login()
    {
        try
        {
            HttpWebRequest req;
            req = (HttpWebRequest)WebRequest.Create("http://blueparrotgames.com/DreamCore/Login.php");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            byte[] postBytes = Encoding.ASCII.GetBytes("username=" + nameField.text + "&password=" + passField.text);
            req.ContentLength = postBytes.Length;

            Stream reqStream = req.GetRequestStream();
            reqStream.Write(postBytes, 0, postBytes.Length);
            reqStream.Close();

            response = (HttpWebResponse)req.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            Debug.Log(reader.ReadToEnd());

            reader.Close();

            if(reader.ReadToEnd().Contains("SUCCESS"))
                loggedIn = true;
        }
        catch (WebException e)
        {
            Debug.Log("ERROR: " + e.Message);
        }
    }
}
