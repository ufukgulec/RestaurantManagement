using Radzen;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Shared.CustomExceptions;
using RestaurantManagement.Shared.ResponseModels;
using System.Net.Http;
using System.Net.Mail;

namespace RestaurantManagement.UI.Utils.Extensions
{
    public static class HttpClientExtension
    {
        public async static Task<T> GetServiceResponseAsync<T>(this HttpClient httpClient, string Url)
        {

            var response = await httpClient.GetFromJsonAsync<T>(Url);
            if (response == null)
                throw new Exception("Hata");

            return response;
        }
        public async static Task<T> GetServiceResponseAsync<T>(this HttpClient httpClient, Uri uri)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<T>();

        }
        public async static Task<TResult> PostGetServiceResponseAsync<TResult, TValue>(this HttpClient httpClient, string Url, TValue Value, bool ThrowSuccessException = false)
        {
            var httpRes = await httpClient.PostAsJsonAsync(Url, Value);

            if (httpRes.IsSuccessStatusCode)
            {
                var response = await httpRes.Content.ReadFromJsonAsync<ServiceResponse<TResult>>();

                return !response.Success && ThrowSuccessException ? throw new ApiException(response.Message) : response.Result;
            }
            throw new ApiException("Hata");
        }

        public async static Task<BaseResponse> PostGetBaseResponseAsync<TValue>(this HttpClient httpClient, string Url, TValue Value, bool ThrowSuccessException = false)
        {
            var httpRes = await httpClient.PostAsJsonAsync(Url, Value);

            if (httpRes.IsSuccessStatusCode)
            {
                var res = await httpRes.Content.ReadFromJsonAsync<BaseResponse>();

                return !res.Success && ThrowSuccessException ? throw new ApiException(res.Message) : res;
            }

            throw new ApiException("Hata");
        }
    }
}