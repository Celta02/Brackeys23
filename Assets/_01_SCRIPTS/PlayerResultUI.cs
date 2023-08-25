using TMPro;
using UnityEngine;

namespace CeltaGames
{
    public class PlayerResultUI : MonoBehaviour
    {
        [SerializeField] TMP_Text _textMeshPro;

        void Start() => _textMeshPro.text = $"{SaveManager.Instance.CurrentData.MaxDepth:F2} m";
    }
}