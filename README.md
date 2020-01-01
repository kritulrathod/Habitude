# Habitude
Habitude with UWP &amp; DotNetCore on AWS


**Note:**

In order to run the integration tests on local `DynamoDb` instace run Batch file from `\docs` folder

```
PM> .\docs\Start_LocalDynamoDb.bat
```
Here is how the PS shell looks like after running the batch file

```
PM> .\docs\Start_LocalDynamoDb.bat

D:\Code\Projects\Habitude>C:

C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE>cd C:\dynamodb_local_latest\ 

C:\dynamodb_local_latest>java -Djava.library.path=./DynamoDBLocal_lib -jar DynamoDBLocal.jar -sharedDb 
Initializing DynamoDB Local with the following configuration:
Port:	8000
InMemory:	false
DbPath:	null
SharedDb:	true
shouldDelayTransientStatuses:	false
CorsParams:	*
```
