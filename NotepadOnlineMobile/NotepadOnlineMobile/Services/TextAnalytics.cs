using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Rest;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public static class TextAnalytics
    {
        class AnalyticsServiceClientCredentials : ServiceClientCredentials
        {
            public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                request.Headers.Add("Ocp-Apim-Subscription-Key", "f42769a504004f089d278301e7068ae0");
                return base.ProcessHttpRequestAsync(request, cancellationToken);
            }
        }

        public static async Task<string> GetDescriptionAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "Empty file";
            
            var client = new TextAnalyticsClient(new AnalyticsServiceClientCredentials());
            client.Endpoint = "https://northeurope.api.cognitive.microsoft.com";
            
            var language = await client.DetectLanguageAsync(new BatchInput(
                new List<Input>()
                {
                    new Input("1", text)
                }));

            var keys = await client.KeyPhrasesAsync(new MultiLanguageBatchInput(
            new List<MultiLanguageInput>()
                {
                    new MultiLanguageInput(language.Documents[0].DetectedLanguages[0].Iso6391Name, "1", text)
                }));

            var desc = string.Join(", ", keys.Documents[0].KeyPhrases);

            if (desc.Length <= 60)
                return desc;
            else
                return desc.Substring(0, 60) + "...";
        }
    }
}
