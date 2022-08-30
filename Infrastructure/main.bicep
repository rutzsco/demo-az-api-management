@description('The name of the API Management service instance')
param apiManagementServiceName string

@description('The email address of the owner of the service')
@minLength(1)
param publisherEmail string = 'scrutz@microsoft.com'

@description('The name of the owner of the service')
@minLength(1)
param publisherName string = 'rutzsco'

@description('The pricing tier of this API Management service')
@allowed([
  'Developer'
  'Standard'
  'Premium'
])
param sku string = 'Developer'

@description('The instance size of this API Management service.')
@allowed([
  1
  2
])
param skuCount int = 1

@description('Location for all resources.')
param location string = resourceGroup().location

param logAnalyticsWorkspaceName string = apiManagementServiceName


// Log Analytics
module logAnalytics 'log-analytics.bicep' = {
  name: 'logAnalytics' 
  params: {
    location: location
    name: logAnalyticsWorkspaceName
  }
}

// API Management
module apim 'api-management-core.bicep' = {
  name: 'apim' 
  params: {
    apiManagementServiceName: apiManagementServiceName
    publisherEmail: publisherEmail
    publisherName: publisherName
    sku: sku
    skuCount: skuCount
    location: location
  }
}
