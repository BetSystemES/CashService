# CashService

CashService Methods

| Method       | Description    |
| ------------- |-------------|
| GetBalance     | Balance receipt function |
| Deposit      | Function for Deposit cash into the account     |
| Withdraw | Function to Withdraw cash from account  |
| DepositRange     | Function to Deposit cash to different accounts |
| WithdrawRange     | Function to Withdraw cash from different accounts      |


Business Models
```
message TransactionModel
{
	string profileid = 1;
	repeated Transaction transactions = 2;
}
```
```
message Transaction
{
	string transactionid = 1; 
	CashType cashtype = 2;
	double amount = 3;
}
```
```
enum CashType
{
	CASH_TYPE_UNSPECIFIED = 0;
	CASH_TYPE_CASH=1;
	CASH_TYPE_BONUS=2;
}
```


Description of Methons of GRPC Service
```
  rpc GetBalance(GetBalanceRequest) returns (GetBalanceResponce);
  rpc Deposit (DepositRequest) returns (DepositResponce);
  rpc Withdraw (WithdrawRequest) returns (WithdrawResponce);
  rpc DepositRange(DepositRangeRequest) returns (DepositRangeResponce);
  rpc WithdrawRange (WithdrawRangeRequest) returns (WithdrawRangeResponce);
```
  
   ```
  Examples of CashService proto-entity
   ```
  
  
GetBalance
  ```
{
	"profileid":"01e0c4c2-1842-4c8d-8bfc-a0ab6a646ec6"
}

```
Deposit
```
{
	"deposit":
	{
		"profileid":"01e0c4c2-1842-4c8d-8bfc-a0ab6a646ec6","transactions":
	 [
		 {
			 "transactionid":"186e1ae1-fd84-465e-962c-6fb4812a70dd","cashtype":1,"amount":95.0
		 },
		{
			"transactionid":"bc218c4d-18ee-49db-80b6-e6d739e48134","cashtype":2,"amount":50.0
		}
	 ]
	}
}
```

```

```

