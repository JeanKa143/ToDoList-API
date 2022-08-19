using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ToDoList_BAL.Exceptions;
using ToDoList_BAL.Models.AppUser;
using ToDoLIst_DAL.Contracts;
using ToDoLIst_DAL.Entities;

namespace ToDoList_BAL.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IdentityError>> AddAsync(CreateUserDTO createUserDto)
        {
            if (createUserDto.Password != createUserDto.ConfirmPassword)
            {
                throw new BadRequestException("Passwords do not match");
            }

            var newUser = _mapper.Map<AppUser>(createUserDto);
            var errors = await _userRepository.AddAsync(newUser, createUserDto.Password);

            return errors;
        }

        public async Task<UserDTO> GetByIdAsync(string id)
        {
            var user = await GetUserById(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<IEnumerable<IdentityError>> UpdateAsync(UpdateUserDTO updateUserDto)
        {
            var user = await GetUserById(updateUserDto.Id);
            user = _mapper.Map(updateUserDto, user);

            var errors = await _userRepository.UpdateAsync(user);
            return errors;
        }

        public async Task<IEnumerable<IdentityError>> UpdatePasswordAsync(UpdatePasswordDTO updatePasswordDto)
        {
            var user = await GetUserById(updatePasswordDto.UserId);
            var errors = await _userRepository.UpdatePasswordAsync(user, updatePasswordDto.OldPassword, updatePasswordDto.NewPassword);

            return errors;
        }

        public async Task<IEnumerable<IdentityError>> DeleteAsync(DeleteUserDTO deleteUserDto)
        {
            var user = await GetUserById(deleteUserDto.Id);
            var isValidPassword = await _userRepository.CheckPasswordAsync(user, deleteUserDto.Password);

            if (!isValidPassword)
            {
                throw new BadRequestException("Invalid password");
            }

            var errors = await _userRepository.DeleteAsync(user);
            return errors;
        }

        private async Task<AppUser> GetUserById(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                throw new NotFoundException("User", id);
            }

            return user;
        }
    }
}
