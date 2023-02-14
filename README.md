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

withdraw
```
{
	"withdrawrequest":
	{
		"profileid":"01e0c4c2-1842-4c8d-8bfc-a0ab6a646ec6","transactions":
	 [
		 {
			 "transactionid":"f2860463-27a1-404b-b1c0-b5967d6241de","cashtype":1,"amount":140.0
		 },
		{
			"transactionid":"00dcf3a5-9c0c-444b-ab9a-035e9699b187","cashtype":2,"amount":60.0
		}
	 ]
	}
}
```

DepositRange
```
{"depositrangerequest":[{"profileid":"8e9575d3-71e2-4ecb-9a1f-323b916be689","transactions":[{"transactionid":"48bbcad3-1a76-49d6-bb55-49d76d2fb706","cashtype":1,"amount":140.0},{"transactionid":"cfefc26d-54a3-45ab-8c73-2e5b87d4138e","cashtype":2,"amount":50.0}]},{"profileid":"b5428f14-997e-4240-9ad9-b74bef23518c","transactions":[{"transactionid":"183d3d04-75e3-480d-af58-77512af25bf7","cashtype":1,"amount":100.0},{"transactionid":"55af84e7-d111-4dfc-8808-e226837e2638","cashtype":2,"amount":60.0}]}]}
```

WithdrawRange
```
{"withdrawrangerequest":[{"profileid":"56ee7606-130d-4553-9fa0-c532d153e60a","transactions":[{"transactionid":"6f87e0cc-44de-40a0-99ba-16da37ee13be","cashtype":1,"amount":100.0},{"transactionid":"928f8451-9af0-4333-acee-3082901f1dd6","cashtype":2,"amount":30.0}]},{"profileid":"a651f00b-5559-4be2-aa06-a52cedd1d08d","transactions":[{"transactionid":"7fc4d382-4966-455c-adec-1cc7d9c03279","cashtype":1,"amount":50.0},{"transactionid":"71d0f4f2-a8b9-4a45-96da-d07a11f93b01","cashtype":2,"amount":10.0}]}]}
```
