using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabServer : MonoBehaviour
{
    private string TitleID = "";
    private GetUserDataResult LocalData;

    public void LogIn (string email, string password)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password,
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLogIn, OnError);
    }

    public void Register (string email, string password, string username)
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = email,
            Username = username,
            Password = password,
            RequireBothUsernameAndEmail = true
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegister, OnError);
    }

    public void ResetPassword(string email, string password)
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = email,
            TitleId = TitleID,
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    public void SaveData(string key, string value)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> {{key, value}}
        };

        PlayFabClientAPI.UpdateUserData(request, OnSaveData, OnError);
    }

    public string GetData(string key)
    {
        DownloadData();

        if (LocalData.Data.ContainsKey(key))
        {
            return(LocalData.Data[key].Value);
        }
        else
        {
            Debug.Log("key: " + key + " does not exist");
            return null;
        }
    }

    private void DownloadData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDownloadData, OnError);
    }

    private void OnDownloadData(GetUserDataResult result)
    {
        Debug.Log("OnDownloadData");
        LocalData = result;
    }

    private void OnRegister(RegisterPlayFabUserResult result)
    {
        Debug.Log("OnRegister");
    }

    private void OnLogIn(LoginResult result)
    {
        Debug.Log("OnLogIn");
    }

    private void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        Debug.Log("OnPasswordReset");
    }

    private void OnSaveData(UpdateUserDataResult result)
    {
        Debug.Log("OnSaveData");
    }

    private void OnError(PlayFabError result)
    {
         Debug.Log("OnError");
         Debug.Log(result.GenerateErrorReport());
    }
}
