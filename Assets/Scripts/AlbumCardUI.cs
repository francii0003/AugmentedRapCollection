using TMPro;
using UnityEngine;

namespace AugmentedRapCollection
{
    public class AlbumCardUI : MonoBehaviour
    {
        [Header("Text Fields")]
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text yearGenreText;
        [SerializeField] private TMP_Text tracksText;
        [SerializeField] private TMP_Text funFactText;

        public void SetData(AlbumData data)
        {
            if (data == null)
            {
                Debug.LogWarning("[AlbumCardUI] Tried to set null AlbumData.");
                return;
            }

            titleText.text = $"{data.artist} - {data.albumTitle}";
            yearGenreText.text = $"{data.releaseYear} | {data.genre}";
            tracksText.text = "Key tracks: " + string.Join(", ", data.keyTracks);
            funFactText.text = data.funFact;
        }
    }
}