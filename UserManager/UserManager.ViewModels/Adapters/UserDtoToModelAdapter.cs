using Domain.Users;
using UserManager.ViewModels.Models;
using System.Collections.Generic;
using System.Linq;

namespace UserManager.ViewModels.Adapters;

internal static class UserDtoToModelAdapter
{
    public static UserModel ToModel(this UserDto dto)
    {
        return new UserModel
        {
            Id = dto.Id,
            UserName = dto.UserName,
            Surnames = dto.Surnames,
            Email = dto.Email,
            Password = dto.Password,
            IsActive = dto.IsActive,
        };
    }

    public static List<UserModel> ToModelList(this IEnumerable<UserDto> dtoList)
    {
        List<UserModel> users = new();

        if (dtoList != null)
        {
            users = dtoList.Select(dto => dto.ToModel()).ToList();
        }

        return users;
    }

    public static UserDto ToDto(this UserModel model)
    {
        return new UserDto
        {
            Id = model.Id,
            UserName = model.UserName,
            Surnames = model.Surnames,
            Email = model.Email,
            Password = model.Password,
            IsActive = model.IsActive,
        };
    }
}
