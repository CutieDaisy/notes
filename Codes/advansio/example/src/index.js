// SPDX-FileCopyrightText: 2021 Lukas Hroch
// SPDX-FileCopyrightText: 2022 Cisco Systems, Inc. and/or its affiliates
//
// SPDX-License-Identifier: MIT
const fs = require('fs');
const express = require('express');
const session = require('express-session');
const { json, urlencoded } = require('body-parser');
const nunjucks = require('nunjucks');
// const config = require('./config');
// const config = require('assets/config.json');
const { encryptString } = require('./config');
const { Client } = require('@duosecurity/duo_universal');
const { LocalStorage } = require('node-localstorage');
const localStorage = new LocalStorage('./scratch');
const startApp = async () => {
  // Express
  const app = express();

  // Express middlewares - request parsers / session / static files / templates
  app.use(json());
  app.use(urlencoded({ extended: false }));
  app.use(session({ secret: 'super-secret-phrase', resave: false, saveUninitialized: true }));
  // app.use(session({hello:'world'}));
  app.use(express.static('public', { index: false }));
  app.set("trust proxy", 1);

  app.use((req, res, next) => {
    res.header('Access-Control-Allow-Origin', '*');
    res.header('Access-Control-Allow-Headers', 'Origin, X-Requested-With, Content-Type, Accept');
    next();
});


  nunjucks.configure(`${__dirname}/views`, { autoescape: true, express: app });


  let config='';
  try {
    const data = fs.readFileSync('assets/config.json', 'utf8');
    config = JSON.parse(data);
} catch (err) {
    console.error('Error reading or parsing file:', err);
}
  // Duo client
  const { clientId, clientSecret, apiHost, redirectUrl, papssAppUrl } = config;
  const duoClient = new Client({ clientId, clientSecret, apiHost, redirectUrl });
 
  // Routes
  // app.get('/', (req, res) => {
  //   res.render('index.html', { message: 'This is a demo' });
  // });

  app.post('/', async (req, res) => {
    let { username } = req.body;
    
    if (!username) {
      res.render('index.html', { message: 'Missing username' });
      return;
    }
    
    try {
      await duoClient.healthCheck();
      
      const state = duoClient.generateState();
      req.session.duo = { state, username };
      const authUrl = duoClient.createAuthUrl(username, state);

      // store username in local storage for later use
      localStorage.setItem(state, username)

      res.json({ authUrl });
    } catch (err) {
      console.error(err);
      res.render('index.html', { message: err.message });
    }
  });

app.get('/callback', async (req, res) => {
  const { query, session } = req;
  const { duo_code, state } = query;
  
  if (!duo_code || typeof duo_code !== 'string') {
    res.render('index.html', { message: `Missing 'duo_code' query parameters` });
    return;
  }

  if (!state || typeof state !== 'string') {
    res.render('index.html', { message: `Missing 'state' query parameters` });
    return;
  }

  // get username from local storage
  const savedUsername = localStorage.getItem(state);
  localStorage.removeItem(state);

  if (!savedUsername ||
    typeof savedUsername !== 'string'
  ) {
    res.render('index.html', { message: 'Missing user session information' });
    return;
  }

try{
    var result = await duoClient
      .exchangeAuthorizationCodeFor2FAResult(duo_code, savedUsername);
      
      let redirectUrl = `${papssAppUrl}/false`;
      if(result?.sub) {
        if(result?.auth_result?.result === "allow" && result?.auth_result?.status === "allow") {
          const subjectAuthority = `${result.sub}::${result.aud}`;
          const subjectAuthorityEnc= encryptString(subjectAuthority);
          redirectUrl = `${papssAppUrl}/${subjectAuthorityEnc}`;
          console.log("App Url  :::::: ", redirectUrl);
        }
        console.log('Result :::: ', result.auth_result);
        
        res.writeHead(302, { 'Location': redirectUrl });
        res.end();

      } else {
        // Handle authentication failure
        res.render('index.html', {
          message: 'Error decoding Duo result. Confirm device clock is correct.',
        });
      }

}catch(err){
  console.log("Error :::: ", err);
  res.render('index.html', {
      message: 'An error occurred. Please try again.',
  });
      // res.json({ success: false, error: err });
}

});
















  // Start listening
  app.listen(config.port, config.url, (err) => {
    if (err) {
      console.log(err);
      return;
    }
    console.log(`App is listening on port ${config.port}!`);
  });
};



startApp().catch((err) => {
  console.error(err);
});
