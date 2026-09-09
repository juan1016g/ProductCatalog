using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id) 
                ?? throw new KeyNotFoundException($"El producto con ID {request.Id} no fue encontrado.");

            await _repository.DeleteAsync(product);
        }
    }
}
