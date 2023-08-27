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
                Debug.Log($"Sign In Success");
            }
            catch (AuthenticationException e)
            {
                Debug.Log($"Error: {e}");
                throw;
            }
        }

        public static async Task UpdateNameAsync(string name) => 
            await AuthenticationService.Instance.UpdatePlayerNameAsync(name);
    }
}