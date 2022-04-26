param environmentName string = 'demo'
param environmentSuffix string = '001'
param region string = 'eastus'
param owner string = 'scrutz@microsoft.com'
param costCenter string = 'rutzsco-core-infra'
param addressPrefix string = '10.0.0.0/15'

var vnetName = 'vnet-${environmentName}-${region}-${environmentSuffix}'

resource vnet 'Microsoft.Network/virtualNetworks@2020-06-01' = {
  name: vnetName
  location: resourceGroup().location
  tags: {
    Owner: owner
    CostCenter: costCenter
  }
  properties: {
    addressSpace: {
      addressPrefixes: [
        addressPrefix
      ]
    }
    enableVmProtection: false
    enableDdosProtection: false
    subnets: [
      {
        name: 'subnet001'
        properties: {
          addressPrefix: '10.0.0.0/24'
        }
      }
      {
        name: 'subnet002'
        properties: {
          addressPrefix: '10.0.1.0/24'
        }
      }
    ]
  }
}
output vnet_id string = vnet.id
