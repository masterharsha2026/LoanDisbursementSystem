using LoanDisbursementSystem.DTO;
using LoanDisbursementSystem.Helpers;
using LoanDisbursementSystem.Repositories;

namespace LoanDisbursementSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUserRepository userRepository, JwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto reqest)
        {
            // 1. get the user using repository layer 
            var user = await _userRepository.GetUserAsync(reqest.UserName, reqest.Password);

            // 2. if user is null return null
            if(user == null )
            {
                return null;
            }


            // 3. Generate JWT TOKEN and return 
            var token = _jwtTokenGenerator.GenerateToken(user);


            // LoginResponseDTO  TOKEN , USERNAME , ROLE
            return new LoginResponseDto
            {
                Token = token,
                UserName = user.UserName,
                Role = user.Role
            };


        }
    }
}
