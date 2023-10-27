namespace TextAdventure.Classes
{
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

        public static void PlayMario()
        {
            List<(Note, int)> songNotes = new()
            {
                (Note.e5,400),(Note.e5,400),(Note.e5,400),(Note.c5,400),(Note.e5,400),(Note.g5,400),(Note.g4,400),
                (Note.c5,400),(Note.g4,400),(Note.e4,400),(Note.a4,400),(Note.b4,400),(Note.bb4,400),(Note.a4,400),
                (Note.g4,400),(Note.e5,400),(Note.g5,400),(Note.a5,400),(Note.f5,400),(Note.g5,400),(Note.e5,400),
                (Note.c5,400),(Note.d5,400),(Note.b4,400),(Note.c5,400),(Note.g4,400),(Note.e4,400),(Note.a4,400),
                (Note.b4,400),(Note.bb4,400),(Note.a4,400),(Note.g4,400),(Note.e5,400),(Note.g5,400),(Note.a5,400),
                (Note.f5,400),(Note.g5,400),(Note.e5,400),(Note.c5,400),(Note.d5,400),(Note.b4,400),(Note.g5,400),
                (Note.gb5,400),(Note.f5,400),(Note.d5,400),(Note.e5,400),(Note.g4,400),(Note.a4,400),(Note.c5,400),
                (Note.a4,400),(Note.c5,400),(Note.d5,400),(Note.g5,400),(Note.gb5,400),(Note.f5,400),(Note.d5,400),
                (Note.e5,400),(Note.c6,400),(Note.c6,400),(Note.c6,400),(Note.g5,400),(Note.gb5,400),(Note.f5,400),
                (Note.d5,400),(Note.e5,400),(Note.g4,400),(Note.a4,400),(Note.c5,400),(Note.a4,400),(Note.c5,400),
                (Note.d5,400),(Note.eb5,400),(Note.d5,400),(Note.c5,400),(Note.c5,400),(Note.c5,400),(Note.c5,400),
                (Note.c5,400),(Note.d5,400),(Note.e5,400),(Note.c5,400),(Note.a4,400),(Note.g4,400),(Note.c5,400),
                (Note.c5,400),(Note.c5,400),(Note.c5,400),(Note.d5,400),(Note.e5,400),(Note.c5,400),(Note.c5,400),
                (Note.c5,400),(Note.c5,400),(Note.d5,400),(Note.e5,400),(Note.c5,400),(Note.a4,400),(Note.g4,400),
                (Note.e5,400),(Note.e5,400),(Note.e5,400),(Note.c5,400),(Note.e5,400),(Note.g5,400),(Note.g4,400),
                (Note.c5,400),(Note.g4,400),(Note.e4,400),(Note.a4,400),(Note.b4,400),(Note.bb4,400),(Note.a4,400),
                (Note.g4,400),(Note.e5,400),(Note.g5,400),(Note.a5,400),(Note.f5,400),(Note.g5,400),(Note.e5,400),
                (Note.c5,400),(Note.d5,400),(Note.b4,400),(Note.c5,400),(Note.g4,400),(Note.e4,400),(Note.a4,400),
                (Note.b4,400),(Note.bb4,400),(Note.a4,400),(Note.g4,400),(Note.e5,400),(Note.g5,400),(Note.a5,400),
                (Note.f5,400),(Note.g5,400),(Note.e5,400),(Note.c5,400),(Note.d5,400),(Note.b4,400),(Note.e5,400),
                (Note.c5,400),(Note.g4,400),(Note.g4,400),(Note.a4,400),(Note.f5,400),(Note.f5,400),(Note.a4,400),
                (Note.b4,400),(Note.a5,400),(Note.a5,400),(Note.a5,400),(Note.g5,400),(Note.f5,400),(Note.e5,400),
                (Note.c5,400),(Note.a4,400),(Note.g4,400),(Note.e5,400),(Note.c5,400),(Note.g4,400),(Note.g4,400),
                (Note.a4,400),(Note.f5,400),(Note.f5,400),(Note.a4,400),(Note.b4,400),(Note.f5,400),(Note.f5,400),
                (Note.f5,400),(Note.e5,400),(Note.d5,400),(Note.c5,400),(Note.g4,400),(Note.e4,400),(Note.c4,400),
                (Note.c5,400),(Note.g4,400),(Note.e4,400),(Note.a4,400),(Note.b4,400),(Note.a4,400),(Note.ab4,400),
                (Note.bb4,400),(Note.ab4,400),(Note.g4,400),(Note.gb4,400),(Note.g4,400)
            };
            Song worstMarioEver = new(songNotes);
            worstMarioEver.PlaySong();
        }
    }
}
