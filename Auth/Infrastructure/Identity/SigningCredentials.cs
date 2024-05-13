using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Infrastructure.Identity;

public static class SigningCredential
{
    public const string KeyPath = ".ES256_Key";

    public static void GenerateSigningCredentials()
    {
        if (!File.Exists(KeyPath))
        {
            using (File.Create(KeyPath)) { }

            // Generate a new ES256 key pair
            using var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            byte[] privateKeyBytes = ecdsa.ExportECPrivateKey();
            byte[] publicKeyBytes = ecdsa.ExportSubjectPublicKeyInfo();

            // Write the private key to a file in the current directory
            File.WriteAllBytes(KeyPath, privateKeyBytes);
        }
    }

    public static ECDsaSecurityKey GetSigningCredentials()
    {
        if (!File.Exists(KeyPath)) GenerateSigningCredentials();

        // Get ES256 key pair
        var key = File.ReadAllBytes(KeyPath);
        var ecdsa = ECDsa.Create();
        ecdsa.ImportECPrivateKey(key, out _);

        return new ECDsaSecurityKey(ecdsa) { KeyId = "a34cf706785f4445acb5e5ab39f09f7d" };
    }
}
