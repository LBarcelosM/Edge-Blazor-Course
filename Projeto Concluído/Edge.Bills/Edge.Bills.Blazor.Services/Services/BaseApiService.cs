using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Edge.Bills.Blazor.Services.Services
{
    public class BaseApiService
    {
        protected readonly HttpClient HttpClient;
        protected string ServiceBaseUrl { get; private set; }

        public BaseApiService(
            string baseServiceUrl,
            HttpClient httpClient)
        {
            ServiceBaseUrl = baseServiceUrl;
            HttpClient = httpClient;
        }

        public virtual async Task<TResult?> Post<TParameter, TResult>(string requestUri, TParameter parameter)
        {
            try
            {
                var httpResponse = await HttpClient.PostAsJsonAsync(requestUri, parameter);
                if (IsValidResponse(httpResponse))
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    return await httpResponse.Content.ReadFromJsonAsync<TResult>(options);
                }
            }
            catch (Exception ex)
            {
                return default;
            }
            return default;
        }

        public virtual async Task<TResult?> PostNoBody<TResult>(string requestUri)
        {
            try
            {
                var stringContent = new StringContent(string.Empty);
                var httpResponse = await HttpClient.PostAsJsonAsync(requestUri, stringContent);
                if (IsValidResponse(httpResponse))
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    return await httpResponse.Content.ReadFromJsonAsync<TResult>(options);
                }
            }
            catch (Exception ex)
            {
                return default;
            }
            return default;
        }

        public virtual async Task<TResult?> Put<TParameter, TResult>(string requestUri, TParameter parameter)
        {
            try
            {
                var httpResponse = await HttpClient.PutAsJsonAsync(requestUri, parameter);
                if (IsValidResponse(httpResponse))
                {
                    return await httpResponse.Content.ReadFromJsonAsync<TResult>();
                }
            }
            catch (Exception ex)
            {
                return default;
            }
            return default;
        }

        public virtual async Task<TResult?> Get<TResult>(string requestUri)
        {
            try
            {
                var httpResponse = await HttpClient.GetAsync(requestUri);
                if (IsValidResponse(httpResponse))
                {
                    return await httpResponse.Content.ReadFromJsonAsync<TResult>();
                }
            }
            catch (Exception ex)
            {
                return default;
            }
            return default;
        }

        public virtual async Task<TResult?> Patch<TParameter, TResult>(string requestUri, TParameter parameter)
        {
            try
            {
                var json = JsonSerializer.Serialize(parameter);
                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    var httpResponse = await HttpClient.PatchAsync(requestUri, stringContent);
                    if (IsValidResponse(httpResponse))
                    {
                        return await httpResponse.Content.ReadFromJsonAsync<TResult>();
                    }
                }
            }
            catch (Exception ex)
            {
                return default;
            }
            return default;
        }

        public virtual async Task<TResult?> Delete<TResult>(string requestUri)
        {
            try
            {
                var httpResponse = await HttpClient.DeleteAsync(requestUri);
                if (IsValidResponse(httpResponse))
                {
                    return await httpResponse.Content.ReadFromJsonAsync<TResult>();
                }
            }
            catch (Exception ex)
            {
                return default;
            }
            return default;
        }

        protected virtual string ServiceUrl(string? requestUri = null, params Tuple<string, string>[]? routeParams)
        {
            var url = string.IsNullOrWhiteSpace(requestUri) ? ServiceBaseUrl : $"{ServiceBaseUrl}/{requestUri}";
            return GetRouteParamsUrl(url, routeParams);
        }

        protected virtual string ServiceUrlWithQueryParams(string? requestUri = null, params Tuple<string, string>[]? queryParams)
        {
            var url = string.IsNullOrWhiteSpace(requestUri) ? ServiceBaseUrl : $"{ServiceBaseUrl}/{requestUri}";
            var queryParamsUrl = GetQueryParamsUrl(url, queryParams);
            return url + queryParamsUrl;
        }

        protected virtual string GetRouteParamsUrl(string url, params Tuple<string, string>[]? queryParams)
        {
            if ((queryParams?.Any()).GetValueOrDefault())
            {
                foreach (var queryParam in queryParams)
                {
                    url = url.Replace(queryParam.Item1, $"{queryParam.Item2}");
                }
            }
            return url;
        }

        protected virtual string GetQueryParamsUrl(string url, Tuple<string, string>[]? queryParams)
        {
            if ((queryParams?.Any()).GetValueOrDefault())
            {
                var first = true;
                var queryParamsUrl = "";
                foreach (var queryParam in queryParams)
                {
                    var queryOperator = first ? "?" : "&";
                    queryParamsUrl += $"{queryOperator}{queryParam.Item1}={queryParam.Item2}";
                    first = false;
                }
                return queryParamsUrl;
            }
            return "";
        }

        protected virtual bool IsValidResponse(HttpResponseMessage httpResponse)
        {
            return httpResponse.StatusCode == HttpStatusCode.OK;
        }
    }
}
