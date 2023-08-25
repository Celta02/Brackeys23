using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace CeltaGames
{
    public class AuthManager : MonoBehaviour
    {
        async void Start()
        {
            await UnityServices.InitializeAsync();
            await SignInAnonymous();
        }

        async Task SignInAnonymous()
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                
                Debug.Log($"Sign In Success");
                Debug.Log($"Player ID: {AuthenticationService.Instance.PlayerId}");
                // SaveManager.Instance.PlayerId = AuthenticationService.Instance.PlayerId;
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}