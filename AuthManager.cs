using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;

public class AuthManager : MonoBehaviour
{
    public PlayFabServer PlayFabScript;
    public TMP_InputField LogInEmailField, LogInPasswordField;
    public TMP_InputField RegisterEmailField, RegisterUsernameField, RegisterPasswordField;

    bool isUpper(char c)
    {
        if (c >= 'A' && c <= 'Z') return true;
        else return false;
    }

    bool isLower(char c)
    {
        if (c >= 'a' && c <= 'z') return true;
        else return false;
    }

    bool isDigit(char c)
    {
        if (c >= '0' && c <= '9') return true;
        else return false;
    }

    public void LogInButton()
    {
        if (ValidEmail(LogInEmailField.text) && ValidPassword(LogInPasswordField.text))
            PlayFabScript.LogIn(LogInEmailField.text, LogInPasswordField.text);
    }

    public void RegisterButton()
    {
        if (ValidEmail(RegisterEmailField.text) && ValidUsername(RegisterUsernameField.text) && ValidPassword(RegisterPasswordField.text))
            PlayFabScript.Register(LogInEmailField.text, RegisterUsernameField.text, LogInPasswordField.text);
    }

    public bool ValidEmail(string email)
    {
        if (email.Length < 7)
            return false;

        if (!email.Contains("@") && !email.Contains("."))
            return false;
        
        return true;
        
    }

    // 6 characters & contains a letter
    public bool ValidUsername(string username)
    {
        if (username.Length < 7)
            return false;

        bool hasLetter = false;
        for (int i = 0; i < username.Length; i++)
            if (Char.IsLetter(username[i]))
                hasLetter = true;
            
        if (hasLetter == false)
            return false;
        
        return true;
    }

    // 6 characters & contains a letter and digit
    public bool ValidPassword(string password)
    {
        if (password.Length < 7)
            return false;

        bool hasDigit = false;
        bool hasLetter = false;
        for (int i = 0; i < password.Length; i++)
        {
            if (Char.IsDigit(password[i]))
                hasDigit = true;
            else
                hasLetter = true;
        }
            
        if (hasLetter == false || hasDigit == false)
            return false;


        return true;
    }

    // String cuando este listo
    public void PasswordRecommendation (String password)
    {
        int passlength =  UnityEngine.Random.Range(10, 16);

        for (int i = 0; i < passlength; i++)
        {
            String randomChar =  UnityEngine.Random.Range(0, 10).ToString();
            password.Insert(password.Length, randomChar);
        }
            // faltan letras todavia, solo hay numeros
            Debug.Log("The FAILED randomly generated default password is:" + password);
    }
}
