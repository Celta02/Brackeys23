using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace CeltaGames
{
    public class AuthManager : MonoBehaviour
    {
        void Start() => GamePlayManager.Instance.CheckRegistration += Register;

        async Task Register()
        {
            Debug.Log($"Registering...");
            await UnityServices.InitializeAsync();
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                
                Debug.Log($"Sign In Success");
            }
            catch (AuthenticationException e)
            {
                Debug.Log($"Error: {e}");
                throw;
            }
        }
    }
}