using TextAdventure.Classes.EffectClasses;

namespace TextAdventure.Classes
{
    public enum Note
    { // Very basic scale
        Silent = 37,
        c4 = 261,
        db4 = 277,
        d4 = 293,
        eb4 = 311,
        e4 = 329,
        f4 = 349,
        gb4 = 370,
        g4 = 392,
        ab4 = 415,
        a4 = 440,
        bb4 = 466,
        b4 = 494,
        c5 = 523,
        db5 = 554,
        d5 = 587,
        eb5 = 622,
        e5 = 659,
        f5 = 698,
        gb5 = 740,
        g5 = 784,
        ab5 = 830,
        a5 = 880,
        bb5 = 932,
        b5 = 988,
        c6 = 1046
    }

    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public static class PlayNote
    {
        public static void Play(Note note, int duration = 800)
        {
            SoundEffect newNote = new((int)note, duration);
            newNote.DoEffect();
        }
    }
}
