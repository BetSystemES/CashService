using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashService.BusinessLogic.Extensions
{
    public static class SumExt
    {
        public static void SumCashAmountForProfileEntity(this ProfileEntity profile, ProfileEntity depWithProfile, CashType cashType)
        {
            profile.CashAmount += depWithProfile.Transactions
                .Where(x => x.CashType == cashType)
                .Sum(x => x.Amount);
        }
    }
}
