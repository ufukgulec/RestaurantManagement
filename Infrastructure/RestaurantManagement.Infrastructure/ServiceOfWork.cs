using OpenAI.GPT3.Interfaces;
using RestaurantManagement.Application;
using RestaurantManagement.Application.Services;
using RestaurantManagement.Infrastructure.Services;

namespace RestaurantManagement.Infrastructure
{
    public class ServiceOfWork : IServiceOfWork
    {
        private bool disposedValue;
        IOpenAIService _AIService;

        public ServiceOfWork(IOpenAIService aIService)
        {
            _AIService = aIService;
        }

        private IOpenAI? _OpenAI;

        public IOpenAI OpenAI => _OpenAI ?? (_OpenAI = new RestaurantManagement.Infrastructure.Services.OpenAI(_AIService));

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: yönetilen durumu (yönetilen nesneleri) atın
                }

                // TODO: yönetilmeyen kaynakları (yönetilmeyen nesneleri) serbest bırakın ve sonlandırıcıyı geçersiz kılın
                // TODO: büyük alanları null olarak ayarlayın
                disposedValue = true;
            }
        }

        // // TODO: sonlandırıcıyı yalnızca 'Dispose(bool disposing)' içinde yönetilmeyen kaynakları serbest bırakacak kod varsa geçersiz kılın
        // ~ServiceOfWork()
        // {
        //     // Bu kodu değiştirmeyin. Temizleme kodunu 'Dispose(bool disposing)' metodunun içine yerleştirin.
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Bu kodu değiştirmeyin. Temizleme kodunu 'Dispose(bool disposing)' metodunun içine yerleştirin.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
