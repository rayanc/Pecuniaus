using NAudio.Lame;
using NAudio.Wave;

namespace Pecuniaus.AudioConvertor
{
    public class AudioHelper
    {
        public static void ConvertToMP3(string inFile, string outFile, int bitRate = 64)
        {

            using (var reader = new AudioFileReader(inFile))
            {
                using (var writer = new LameMP3FileWriter(outFile, reader.WaveFormat, bitRate))
                    reader.CopyTo(writer);
            }
        }

    
    }
}
