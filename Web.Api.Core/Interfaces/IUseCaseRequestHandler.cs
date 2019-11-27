﻿using System.Threading.Tasks;

namespace Web.Api.Core.Interfaces
{
    public interface IUseCaseRequestHandler<in TUseCaseRequest, out TUseCaseResponse> where TUseCaseRequest : IUseCaseRequest<TUseCaseResponse>
    {
        Task<bool> HandleAsync(TUseCaseRequest message, IOutputPort<TUseCaseResponse> outputPort);
    }
}