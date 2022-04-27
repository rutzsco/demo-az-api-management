// Configurable input parameters
@description('The environment suffix to append to resource names.')
param environmentSuffix string = 'apidemo'

@description('The environment prefix to append to resource names.')
param environmentName string = 'apidemo'

@description('The environment region location.')
param location string = 'eastus'

// Resource names
var ResourceName = '${environmentName}acr${environmentSuffix}'
var sqlServerResourceName = '${environmentName}sql-${environmentSuffix}'

// VNET
module vnet 'vnet.bicep' = {
  name: 'vnet'
  params: {
    environmentSuffix: environmentSuffix
    environmentName: environmentName
    region: location
  }
}
