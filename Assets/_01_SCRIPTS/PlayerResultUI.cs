using TMPro;
using UnityEngine;

namespace CeltaGames
{
    public class PlayerResultUI : MonoBehaviour
    {
        [SerializeField] SaveManager _saveManager;
        [SerializeField] TMP_Text _textMeshPro;

        void Start() => _textMeshPro.text = $"{_saveManager.Load().MaxDepth:F2} m";
    }
}