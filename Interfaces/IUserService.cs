using AppNum5.Models;
using AppNum5.Services;
public interface IUserService
{
    Task<UserDTO?> GetByIdAsync(int id);
    Task<List<UserDTO>> GetAllAsync();
    Task<UserDTO> CreateAsync(PostUserDTO dto);
    Task<bool> UpdateAsync(int id, PutUserDTO dto);
    Task<bool> DeleteAsync(int id);
}