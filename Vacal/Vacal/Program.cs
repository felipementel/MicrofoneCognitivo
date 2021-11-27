﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Translation;

namespace Vacal
{
    class Program
    {
        static void Main(string[] args)
        {
            //SpeechToText().Wait();
            SpeechToEng().Wait();
        }

        static async Task SpeechToEng()
        {
            Console.WriteLine("Fale Algo");
            var speechConfig = SpeechTranslationConfig.FromSubscription("6b383acf5bcf42a28367858bbcdddc18"
                , "northcentralus");
            speechConfig.SpeechRecognitionLanguage = "pt-BR";

            //speechConfig.AddTargetLanguage("en-US");
            var langs = new List<string> { "en-US", "fr-FR", "it-IT" };
            langs.ForEach(speechConfig.AddTargetLanguage);
            
            var recognizer = new TranslationRecognizer(speechConfig);
            var result = await recognizer.RecognizeOnceAsync();
            if (result.Reason == ResultReason.TranslatedSpeech)
            {
                Console.WriteLine(result.Text);
                foreach (var lang in result.Translations)
                {
                    Console.WriteLine($"{lang.Key} - {lang.Value}");
                }
            }

            Console.ReadLine();
        }

            static async Task SpeechToText()
        {
            var speechConfig = SpeechConfig.FromSubscription("6b383acf5bcf42a28367858bbcdddc18"
                , "northcentralus");
            var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            var recognizer = new SpeechRecognizer(speechConfig, "pt-BR", audioConfig);
            Console.WriteLine("Falai alguma coisa:");

            while (true)
            {
                var result = await recognizer.RecognizeOnceAsync();
                var texto = result.Text;
                Console.WriteLine($"Reconhecido: {texto} ");
                if(texto.ToLower().Contains("sair."))
                {
                    break;
                }
            }
            Console.ReadLine();
        }

    }
}
