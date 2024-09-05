// SPDX-FileCopyrightText: 2021 Lukas Hroch
// SPDX-FileCopyrightText: 2022 Cisco Systems, Inc. and/or its affiliates
//
// SPDX-License-Identifier: MIT

 encryptString= (rawText)=> {
  let result = '';
  const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
  const charactersLength = characters.length;
  let counter = 0;
  const length = 8;
  while (counter < length) {
    result += characters.charAt(Math.floor(Math.random() * charactersLength));
    counter += 1;
  }
  return result + btoa(rawText) + "==";
}


module.exports = {
  // port: 3000,
  // url: 'localhost',
  // clientId: 'DI2MW6012BQ7IKQ74RZK',
  // clientSecret: 'PFOm2bRyEPWGY4g7FuDd2kK1NdYp0u6LjsQV1GlM',
  // apiHost: 'api-7cfc9807.duosecurity.com',
  // redirectUrl: 'http://localhost:3000/callback',
  // appUrl:"http://localhost:4200/#/auth/login/callback",
  encryptString: encryptString,
};
// module.exports = {
//   port: 3000,
//   url: 'localhost',
//   clientId: 'DIRIGK1W8IUE20KZ922Z',
//   clientSecret: 'gqpiuBs0RDtfhLpIV841is6EUnnhyYZcTcCuvetA',
//   apiHost: 'api-97b91fb8.duosecurity.com',
//   redirectUrl: 'http://localhost:3000/redirect',
// };


