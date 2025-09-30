using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace SelfFinanceWeb.Services
{
    public class AuthMessageHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;
        private readonly ILogger<AuthMessageHandler> _logger;

        public AuthMessageHandler(ILocalStorageService localStorage, ILogger<AuthMessageHandler> logger)
        {
            _localStorage = localStorage;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>("authToken");
                if (!string.IsNullOrWhiteSpace(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to retrieve token from localStorage");
            }

            var response = await base.SendAsync(request, cancellationToken);

            return response;
        }
    }
}
