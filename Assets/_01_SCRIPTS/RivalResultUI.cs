using TMPro;
using UnityEngine;

namespace CeltaGames
{
    public class RivalResultUI : MonoBehaviour
    {
        [SerializeField] TMP_Text _textMeshPro;

        void Start() => _textMeshPro.text = $"{SaveManager.CurrentData.RivalMaxDepth:F2} m";
    }
}