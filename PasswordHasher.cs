using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

public class PasswordHasher
{

    public Hash Encrypt(string password)
    {
        // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
        byte[] saltbyte = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltbyte);
        }

        string Salt=Convert.ToBase64String(saltbyte);

        Console.WriteLine($"Salt: {Salt}");

        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        

        var hashed = ComputeHash(Salt, password);
       
        Console.WriteLine($"Hashed: {hashed}");

        return new Hash{Salt=Salt, Sha=hashed};
    }

    public string ComputeHash(string Salt, string password){
        using var sha256=SHA256.Create();

        string hashed=Convert.ToBase64String(sha256.ComputeHash(Encoding.ASCII.GetBytes(Salt+password)));
        return hashed;
    }
    public bool Verify(string candidatepw, Hash hash)
    {
        var checksum=ComputeHash(hash.Salt, candidatepw);
        Console.WriteLine(checksum);
        Console.WriteLine(hash.Sha);
        if(checksum==hash.Sha)
        {
            return true;
        }else
        {
            return false;
        }

    }

}