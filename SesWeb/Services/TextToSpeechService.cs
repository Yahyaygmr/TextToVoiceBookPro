using System;
using System.IO;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using NAudio.Wave;
using NAudio.Lame;

namespace SesWeb.Services
{
    public interface ITextToSpeechService
    {
        Task<string> GenerateMp3Async(string text, string fileName = null);
    }

    public class TextToSpeechService : ITextToSpeechService
    {
        private readonly IWebHostEnvironment _environment;

        public TextToSpeechService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> GenerateMp3Async(string text, string fileName = null)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Metin boş olamaz.");

            var audioDir = Path.Combine(_environment.WebRootPath, "audio");
            if (!Directory.Exists(audioDir))
                Directory.CreateDirectory(audioDir);

            fileName ??= $"audio_{Guid.NewGuid()}";
            var wavPath = Path.Combine(audioDir, fileName + ".wav");
            var mp3Path = Path.Combine(audioDir, fileName + ".mp3");

            // 1. WAV dosyasını oluştur
            using (var synth = new SpeechSynthesizer())
            {
                synth.SetOutputToWaveFile(wavPath);
                synth.Speak(text);
            }

            // 2. WAV'dan MP3'e dönüştür
            await Task.Run(() =>
            {
                using (var reader = new AudioFileReader(wavPath))
                using (var writer = new LameMP3FileWriter(mp3Path, reader.WaveFormat, LAMEPreset.VBR_90))
                {
                    reader.CopyTo(writer);
                }
            });

            // 3. WAV dosyasını sil (isteğe bağlı)
            if (File.Exists(wavPath))
                File.Delete(wavPath);

            // 4. MP3 yolunu döndür
            return $"audio/{fileName}.mp3";
        }
    }
} 