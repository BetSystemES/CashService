﻿namespace CashService.GRPC.Infrastructure.Configurations.Jwt
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public double TokenLifetimeInMinutes { get; set; }
        public double RefreshTokenLifetimeInMinutes { get; set; }
        public int RefreshTokenSizeInBytes { get; set; }
    }
}