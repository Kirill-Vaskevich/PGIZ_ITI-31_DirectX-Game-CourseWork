using SharpDX.Multimedia;
using SharpDX.XAudio2;

namespace DirectLib.Audio
{
    public class Sound
    {
        public AudioBuffer audioBuffer;
        public uint[] decodedInfo;
        public WaveFormat format;
    }
}
