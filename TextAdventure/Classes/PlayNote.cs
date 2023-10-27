using TextAdventure.Classes.EffectClasses;

namespace TextAdventure.Classes
{
    public enum Note
    { // Very basic scale
        Silent = 37,
        C = 257,
        D = 288,
        E = 323,
        F = 343,
        G = 385,
        A = 432,
        B = 484
    }

    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public static class PlayNote
    {
        public static void Play(Note note, int duration = 500)
        {
            SoundEffect newNote = new((int)note, duration);
            newNote.DoEffect();
        }
    }

    public class Song
    {
        public List<(Note, int)> MySong { get; set; }
        public Song()
        {
            MySong = new List<(Note, int)>();
        }
        public Song(List<(Note, int)> song)
        {
            MySong = song;
        }

        public void PlaySong()
        {
            for (int i = 0; i < MySong.Count; i++)
            {
                PlayNote.Play(MySong[i].Item1, MySong[i].Item2);
            }
        }
    }
}
