using AutoMapper;
using Explora.BusinessLogic.Interfaces;
using Explora.DataLayer.Repository.Interfaces;
using Explora.DataLayer.UnitOfWork;
using Explora.DataTransferObjects;
using Explora.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Explora.BusinessLogic
{
    public abstract class ServiceBase : IServiceBase 
    {
        protected readonly IMapper _mapper;
        protected readonly IRepository _repository;

        public ServiceBase(IRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
    }
}
