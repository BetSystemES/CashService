using Google.Protobuf.WellKnownTypes;

namespace CashService.GRPC.Infrastructure.Mappings.Extensions
{
    public static class Converters
    {
        public static Timestamp ToTimestamp(this DateTimeOffset dateTimeOffset)
        {
            return Timestamp.FromDateTimeOffset(dateTimeOffset);
        }
    }
}