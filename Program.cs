using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace speech_synthesizer
{
    class Program
    {
        static async Task Main()
        {
            await SynthesisToSpeakerAsync();
        }

        public static async Task SynthesisToSpeakerAsync()
        {
            var config = SpeechConfig.FromSubscription("04d78025d6c14834ba9888b8d307843c", "eastus");

            config.SpeechSynthesisVoiceName = "en-IN-Heera";

            using (var synthesizer = new SpeechSynthesizer(config))
            {
                Console.WriteLine("Type some text you want to speak...");
                Console.Write("> ");

                string text = Console.ReadLine();

                using (var result = await synthesizer.SpeakTextAsync(text))
                {
                    if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                    {
                        Console.WriteLine($"Speech synthesized to speaker for text [{text}]");
                    }
                    else if (result.Reason == ResultReason.Canceled)
                    {
                        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                        Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                        if (cancellation.Reason == CancellationReason.Error)
                        {
                            Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                            Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                            Console.WriteLine($"CANCELED: Did you update the subscription info?");
                        }
                    }
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }

        }

    }
}
