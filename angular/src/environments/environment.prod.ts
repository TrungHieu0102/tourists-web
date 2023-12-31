import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'TrungHieuTourists',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44339/',
    redirectUri: baseUrl,
    clientId: 'TrungHieuTourists_App',
    responseType: 'code',
    scope: 'offline_access TrungHieuTourists',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44396',
      rootNamespace: 'TrungHieuTourists',
    },
  },
} as Environment;
