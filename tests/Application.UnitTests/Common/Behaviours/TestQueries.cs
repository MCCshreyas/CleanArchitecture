using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.TodoLists.Queries.GetTodos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UnitTests.Common.Behaviours
{
    public class GetCachedQuery : IRequest<TodosVm>, ICache
    {

        public CacheOptions SetCacheOptions() => new CacheOptions
        {
            CacheKey = $"CustomKey",
            ExpirationRelativeToNow = TimeSpan.FromMilliseconds(30000)
        };
    }

    public class GetCachedQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCachedQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            return new TodosVm
            {

                Lists = new List<TodoListDto>()
            };
        }
    }

    public class GetCachedWithParametersQuery : IRequest<TodosVm>, ICache
    {
        public int Id { get; set; }
    }

    public class GetCachedWithParametersQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCachedWithParametersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            return new TodosVm
            {

                Lists = new List<TodoListDto>()
            };
        }
    }

    public class GetCachedNoCacheOptionsQuery : IRequest<TodosVm>, ICache
    {
    }

    public class GetCachedNoCacheOptionsQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCachedNoCacheOptionsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            return new TodosVm
            {

                Lists = new List<TodoListDto>()
            };
        }
    }

    public class GetNonCachedQuery : IRequest<TodosVm>
    {

        public CacheOptions SetCacheOptions() => new CacheOptions
        {
            CacheKey = $"CustomKey",
            ExpirationRelativeToNow = TimeSpan.FromMilliseconds(60000)
        };
    }

    public class GetNonCachedQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetNonCachedQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            return new TodosVm
            {

                Lists = new List<TodoListDto>()
            };
        }
    }
}
