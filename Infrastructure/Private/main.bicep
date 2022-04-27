// Configurable input parameters
@description('The environment suffix to append to resource names.')
param environmentSuffix string = 'apidemo'

@description('The environment prefix to append to resource names.')
param environmentName string = 'apidemo'

@description('The environment region location.')
param location string = 'eastus'

@description('The environment region location.')
param apimPublisherEmail string = 'scrutz@microsoft.com'

// Resource names
var apimResourceName = '${environmentName}-apim-${environmentSuffix}'

// VNET
module vnet 'vnet.bicep' = {
  name: 'vnet'
  params: {
    environmentSuffix: environmentSuffix
    environmentName: environmentName
    region: location
  }
}

// APIM
var apimSubnetId='${vnet.outputs.vnet_id}/subnets/subnet001'
module apiManagement 'apim.bicep' = {
  name: 'apim'
  params: {
    apimPublisherName: 'rutzsco'
    apimName: apimResourceName
    apimPublisherEmail: apimPublisherEmail
    apimSubnetId: apimSubnetId
    sku: 'developer'
  }
}
