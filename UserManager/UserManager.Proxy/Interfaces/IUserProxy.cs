using Domain.Users;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager.Proxy.Interfaces;

public interface IUserProxy
{
    Task<HandlerRequestResult<IEnumerable<UserDto>>> GetAllUsersAsync();
    Task<HandlerRequestResult> CreateUserAsync(UserDto dto);
    Task<HandlerRequestResult> UpdateUserAsync(UserDto dto);
    Task<HandlerRequestResult> DeactivateUserAsync(int id);
}
