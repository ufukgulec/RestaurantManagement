using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels.ResponseModels;
using OpenAI.GPT3.ObjectModels.ResponseModels.ImageResponseModel;
using RestaurantManagement.Application.Services;

namespace RestaurantManagement.Infrastructure.Services
{
    public class OpenAI : IOpenAI
    {
        readonly IOpenAIService openAIService;

        public OpenAI(IOpenAIService openAIService)
        {
            this.openAIService = openAIService;
        }
        public async Task<string> CompletionAsync(string question)
        {
            CompletionCreateResponse response = await openAIService.Completions.CreateCompletion(new CompletionCreateRequest
            {
                Prompt = question,
                MaxTokens = 500,

            }, Models.TextDavinciV3);
            return response.Choices[0].Text;
        }

        public async Task<List<string>> ImageGeneratorAsync(string text)
        {
            ImageCreateResponse response = await openAIService.Image.CreateImage(new ImageCreateRequest()
            {
                Prompt = text,
                N = 4,
                Size = StaticValues.ImageStatics.Size.Size512,
                ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
                User = "test"
            });

            return response.Results.Select(r => r.Url).ToList();

        }
    }
}
