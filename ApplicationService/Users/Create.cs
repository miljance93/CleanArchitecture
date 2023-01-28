using ApplicationService.Core;
using Domain.DTO;
using Domain.Repositories;
using MediatR;

namespace ApplicationService.Users
{
    public class Create
    {
        public record Command(UserDto User) : IRequest<Result<Unit>>;

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.CreateUser(request.User);
                if (user)
                {
                    return Result<Unit>.Success(Unit.Value);
                }

                return Result<Unit>.Failure("User is not created");
            }
        }
    }
}
