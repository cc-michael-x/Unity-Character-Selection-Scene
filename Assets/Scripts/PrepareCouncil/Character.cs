using UnityEngine;

namespace PrepareCouncil
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Character Selection/Character")]
    public class Character : ScriptableObject
    {
        [SerializeField] private string characterName;
        [SerializeField] public GameObject previewCharacterPrefab;
        [SerializeField] private GameObject gameplayCharacterPrefab;

        public string CharacterName => characterName;
        public GameObject PreviewCharacterPrefab => previewCharacterPrefab;
        public GameObject GameplayCharacterPrefab => gameplayCharacterPrefab;
    }
}
