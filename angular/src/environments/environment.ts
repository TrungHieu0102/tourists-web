import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'TrungHieuTourists Admin',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:5000/',
    redirectUri: baseUrl,
    clientId: 'TrungHieuTourists_Admin',
    dummyClientSecret:'1q2w3e*',
    responseType: 'code',
    scope: 'TrungHieuTourists.Admin offline_access',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:5001',
      rootNamespace: 'TrungHieuTourists.Admin',
    },
  },
} as Environment;