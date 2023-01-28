using ApplicationService.Core;
using Domain.DTO;
using Domain.Repositories;
using MediatR;

namespace ApplicationService.Users
{
    public class List
    {
        public record Query : IRequest<Result<IEnumerable<UserDto>>>;

        public class Handler : IRequestHandler<Query, Result<IEnumerable<UserDto>>>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }
            public async Task<Result<IEnumerable<UserDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _userRepository.GetUsers();
                if (result is not null)
                {
                    return Result<IEnumerable<UserDto>>.Success(result);
                }
                return Result<IEnumerable<UserDto>>.Failure("List of users could not be found");
            }
        }
    }
}
