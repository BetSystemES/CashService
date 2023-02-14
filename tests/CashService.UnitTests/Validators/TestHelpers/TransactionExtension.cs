﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashService.GRPC;
using Google.Protobuf.Collections;

namespace CashService.UnitTests.Validators
{
    public static class TransactionExtension
    {
        public static Transaction Init(this Transaction transaction,
            string transactioId,
            CashType cashType,
            double amount
         )
        {
            transaction.Transactionid = transactioId;
            transaction.Cashtype = cashType;
            transaction.Amount = amount;
            return transaction;
        }
    }
}