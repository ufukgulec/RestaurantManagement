﻿namespace RestaurantManagement.Shared.ResponseModels
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
        }

        public bool Success { get; set; }
        public string Message { get; set; }

        public void SetException(Exception exception)
        {
            Success = false;
            Message = exception.Message;
        }
    }
}
