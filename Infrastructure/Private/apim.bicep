@description('Name of the APIM instance')
param apimName string

@description('Email address of the APIM publisher')
param apimPublisherEmail string

@description('APIM publisher')
param apimPublisherName string

@description('ID of the subnet where the APIM instance private endpoint will go')
param apimSubnetId string

@description('APIM SKU')
param sku string


resource apiManagementInstance 'Microsoft.ApiManagement/service@2020-12-01' = {
  name: apimName
  location: resourceGroup().location
  sku:{
    capacity: 1
    name: sku
  }
  properties:{
    virtualNetworkType: 'Internal'
    virtualNetworkConfiguration: {
      subnetResourceId: apimSubnetId
    }
    publisherEmail: apimPublisherEmail
    publisherName: apimPublisherName
    customProperties: {
      // Only allow TLS 1.2+
      'Microsoft.WindowsAzure.ApiManagement.Gateway.Security.Protocols.Tls10': 'false'
      'Microsoft.WindowsAzure.ApiManagement.Gateway.Security.Protocols.Tls11': 'false'
      'Microsoft.WindowsAzure.ApiManagement.Gateway.Security.Backend.Protocols.Tls10': 'false'
      'Microsoft.WindowsAzure.ApiManagement.Gateway.Security.Backend.Protocols.Tls11': 'false'

      // Disable weak ciphers https://docs.microsoft.com/en-us/azure/templates/microsoft.apimanagement/2019-01-01/service?tabs=bicep#apimanagementserviceproperties
      'Microsoft.WindowsAzure.ApiManagement.Gateway.Security.Ciphers.TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA': 'false'
      'Microsoft.WindowsAzure.ApiManagement.Gateway.Security.Ciphers.TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA': 'false'
      'Microsoft.WindowsAzure.ApiManagement.Gateway.Security.Ciphers.TLS_RSA_WITH_AES_128_GCM_SHA256': 'false'
      'Microsoft.WindowsAzure.ApiManagement.Gateway.Security.Ciphers.TLS_RSA_WITH_AES_256_CBC_SHA256': 'false'
      'Microsoft.WindowsAzure.ApiManagement.Gateway.Security.Ciphers.TLS_RSA_WITH_AES_128_CBC_SHA256': 'false'
      'Microsoft.WindowsAzure.ApiManagement.Gateway.Security.Ciphers.TLS_RSA_WITH_AES_256_CBC_SHA': 'false'
      'Microsoft.WindowsAzure.ApiManagement.Gateway.Security.Ciphers.TLS_RSA_WITH_AES_128_CBC_SHA': 'false'
    }
  }
}

output name string = apiManagementInstance.name
output serverUrl string  = apiManagementInstance.properties.gatewayUrl
