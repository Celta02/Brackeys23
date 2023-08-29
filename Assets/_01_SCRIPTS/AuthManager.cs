using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace CeltaGames
{
    public static class AuthManager
    {
        public static async Task RegisterAsync()
        {
            Debug.Log($"Registering...");
            await UnityServices.InitializeAsync();
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                GamePlayManager.SavePlayerId(AuthenticationService.Instance.PlayerId);
                GamePlayManager.SaveAccessToken(AuthenticationService.Instance.AccessToken);

                Debug.Log($"Sign In Success");
            }
            catch (Exception e)
            {
                Debug.Log($"Error: {e}");
                throw;
            }

        }

        public static async Task UpdateNameAsync(string name) => await AuthenticationService.Instance.UpdatePlayerNameAsync(name);
    }
}