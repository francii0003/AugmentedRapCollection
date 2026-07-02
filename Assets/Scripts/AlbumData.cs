using UnityEngine;

namespace AugmentedRapCollection
{
    [CreateAssetMenu(fileName = "NewAlbumData", menuName = "AugmentedRapCollection/Album Data")]
    public class AlbumData : ScriptableObject
    {
        [Header("Identification")]
        [Tooltip("Must exactly match the Name field of the corresponding entry in the AR Reference Image Library.")]
        public string referenceImageName;

        [Header("Album Info")]
        public string artist;
        public string albumTitle;
        public int releaseYear;
        public string genre;

        [Header("Content")]
        public string[] keyTracks;

        [TextArea(2, 4)]
        public string funFact;
    }
}