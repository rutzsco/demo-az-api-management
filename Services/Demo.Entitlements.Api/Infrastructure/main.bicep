param location string = 'eastus'

param functionAppStorageAccountName string
param functionAppName string


// Function
module function 'function.bicep' = {
  name: 'functionDeploy'
  params: {
    location: location
    function_app_name: functionAppName
    storage_account_name: functionAppStorageAccountName
  }
}

