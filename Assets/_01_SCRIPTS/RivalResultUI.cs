using TMPro;
using UnityEngine;

namespace CeltaGames
{
    public class RivalResultUI : MonoBehaviour
    {
        [SerializeField] SaveManager _saveManager;
        [SerializeField] TMP_Text _textMeshPro;

        void Start() => _textMeshPro.text = $"{_saveManager.Load().RivalMaxDepth:F2} m";
    }
}