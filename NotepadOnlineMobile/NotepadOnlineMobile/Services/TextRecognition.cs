using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.IO;
using System.Threading.Tasks;

namespace Services
{
    public static class TextRecognition
    {
        public static async Task<string[]> ExtractTextAsync(string imagePath, TextRecognitionMode recognitionMode)
        {
            var computerVision = new ComputerVisionClient(new ApiKeyServiceClientCredentials("e84b8e5786bc441d93818cb2a55d265a"), new System.Net.Http.DelegatingHandler[] { });
            computerVision.Endpoint = "https://northeurope.api.cognitive.microsoft.com/";
            var ex = File.Exists(imagePath);
            using (Stream imageStream = File.OpenRead(imagePath))
            {
                RecognizeTextInStreamHeaders textHeaders = await computerVision.RecognizeTextInStreamAsync(imageStream, recognitionMode);
                return await GetTextAsync(computerVision, textHeaders.OperationLocation);
            }
        }

        private static async Task<string[]> GetTextAsync(ComputerVisionClient computerVision, string operationLocation)
        {
            string operationId = operationLocation.Substring(operationLocation.Length - 36);

            TextOperationResult result = await computerVision.GetTextOperationResultAsync(operationId);

            int i = 0;
            while ((result.Status == TextOperationStatusCodes.Running || result.Status == TextOperationStatusCodes.NotStarted) && i++ < 20)
            {
                await Task.Delay(1000);
                result = await computerVision.GetTextOperationResultAsync(operationId);
            }
            
            var lines = result.RecognitionResult.Lines;
            var text = new string[lines.Count];

            for (i = 0; i < lines.Count; i++)
                text[i] = lines[i].Text;

            return text;
        }
    }
}