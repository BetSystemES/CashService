using System.ComponentModel;

namespace CashService.BusinessLogic.Models.Enums
{
    public enum AuthRole
    {
        [Description("admin")]
        Admin,

        [Description("user")]
        User,
    }
}