using System;
using System.Security.Cryptography;

namespace Punk.Services.Helpers;

public interface IPasswordService
{
    byte[] GenerateSalt(int size = 16);
    string HashPassword(string password, byte[] salt, int iterations = 10000, int hashSize = 20);
    bool VerifyPassword(string password, string hashedPassword, byte[] salt, int iterations = 10000, int hashSize = 20);
}

public class PasswordService : IPasswordService
{
    public byte[] GenerateSalt(int size = 16)
    {
        using var rng = new RNGCryptoServiceProvider();
        var salt = new byte[size];
        rng.GetBytes(salt);
        return salt;
    }

    public string HashPassword(string password, byte[] salt, int iterations = 10000, int hashSize = 20)
    {
        using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, iterations);
        return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(hashSize));
    }

    public bool VerifyPassword(string password, string hashedPassword, byte[] salt, int iterations = 10000,
        int hashSize = 20)
    {
        var hashedPasswordToCompare = HashPassword(password, salt, iterations, hashSize);
        return hashedPasswordToCompare == hashedPassword;
    }
}