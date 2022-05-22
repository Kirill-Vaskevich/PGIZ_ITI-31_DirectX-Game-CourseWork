using SharpDX.Multimedia;
using SharpDX.XAudio2;
using System.IO;
using System.Collections.Generic;

namespace DirectLib.Audio
{
    public class DXAudio
    {
        private XAudio2 _device;
        private MasteringVoice _masteringVoice;

        private List<Sound> _sounds;

        SoundType Sound { get; set; }

        public DXAudio()
        {
            _device = new XAudio2();
            _masteringVoice = new MasteringVoice(_device);
            _sounds = new List<Sound>();
            _sounds.Add(LoadSound(SoundType.Bonus));
            _sounds.Add(LoadSound(SoundType.Collide));
            _sounds.Add(LoadSound(SoundType.Win));
        }

        private Sound LoadSound(SoundType soundType)
        {
            string fileName = FileName(soundType);
            var stream = new SoundStream(File.OpenRead(fileName));

            uint[] info = stream.DecodedPacketsInfo;
            var buffer = new AudioBuffer
            {
                Stream = stream.ToDataStream(),
                AudioBytes = (int)stream.Length,
                Flags = BufferFlags.EndOfStream
            };
            stream.Close();

            Sound sound = new Sound()
            {
                audioBuffer = buffer,
                decodedInfo = info,
                format = stream.Format
            };

            return sound;
        }

        public void PlaySound(SoundType soundType)
        {
            int index = (int)soundType;
            var sourceVoice = new SourceVoice(_device, _sounds[index].format, false);
            sourceVoice.SubmitSourceBuffer(_sounds[index].audioBuffer, _sounds[index].decodedInfo);
            sourceVoice.Start();
        }

        private string FileName(SoundType soundType)
        {
            string fileName = null;
            switch (soundType)
            {
                case SoundType.Bonus:
                    fileName = @"Externals\Sounds\bonus.wav";
                    break;
                case SoundType.Collide:
                    fileName = @"Externals\Sounds\collide.wav";
                    break;
                case SoundType.Win:
                    fileName = @"Externals\Sounds\win.wav";
                    break;
            }
            return fileName;
        }
    }
}
