using TaechIdeas.Core.Core.Common.Dto;

namespace TaechIdeas.Core.Core.Common
{
    public interface IMySecurityManager
    {
        string GenerateSha1Hash(string textToHash);
        string GenerateMd5FileHash(string filePath);
        PasswordScore CheckPasswordStrength(string password, string username);
    }
}