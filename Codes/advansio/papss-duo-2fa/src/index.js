// SPDX-FileCopyrightText: 2021 Lukas Hroch
// SPDX-FileCopyrightText: 2022 Cisco Systems, Inc. and/or its affiliates
//
// SPDX-License-Identifier: MIT

const express = require('express');
const session = require('express-session');
const { json, urlencoded } = require('body-parser');
const nunjucks = require('nunjucks');
const config = require('./config');
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

  // Duo client
  const { clientId, clientSecret, apiHost, redirectUrl, appUrl, encryptString } = config;
  const duoClient = new Client({ clientId, clientSecret, apiHost, redirectUrl });

  // Routes
  app.get('/', (req, res) => {
    res.render('index.html', { message: 'This is a demo' });
  });

  app.post('/', async (req, res) => {
    let { username } = req.body;
  req.session.username = username;
  console.log("Body :::::: ",req.body);
  
  console.log("Session Store :::::: ",req.sessionStore);
  console.log("Session :::::: ",req.session);
  
    username= "req.body.username";
    password= "req.body.password";

    
    // return;
    
    if (!username || !password) {
      res.render('index.html', { message: 'Missing username or password' });
      return;
    }
    
    try {
      await duoClient.healthCheck();
      
      const state = duoClient.generateState();
      req.session.duo = { state, username };
      const authUrl = duoClient.createAuthUrl(username, state);

      localStorage.setItem(state, username)

      res.json({ authUrl });

      // res.redirect(302, url);
    } catch (err) {
      console.error(err);
      res.render('index.html', { message: err.message });
    }
  });

  app.get('/redirect', async (req, res) => {
    const { query, session } = req;
    const { duo_code, state } = query;
    
    console.log("Session Duo  :::::: ", session);
    console.log('request ::::: ', req);
    
    if (!duo_code || typeof duo_code !== 'string') {
      res.render('index.html', { message: `Missing 'duo_code' query parameters` });
      return;
    }

    if (!state || typeof state !== 'string') {
      res.render('index.html', { message: `Missing 'state' query parameters` });
      return;
    }

    const savedState = session.duo?.state;
    const savedUsername = session.duo?.username;

    req.session.destroy();

    if (
      !savedState ||
      typeof savedState !== 'string' ||
      !savedUsername ||
      typeof savedUsername !== 'string'
    ) {
      res.render('index.html', { message: 'Missing user session information' });
      return;
    }

    if (state !== savedState) {
      res.render('index.html', { message: 'Duo state does not match saved state' });
      return;
    }

    try {
      const decodedToken = await duoClient.exchangeAuthorizationCodeFor2FAResult(
        duo_code,
        savedUsername
      );
      res.render('success.html', { message: JSON.stringify(decodedToken, null, '\t') });
    } catch (err) {
      console.error(err);

      res.render('index.html', {
        message: 'Error decoding Duo result. Confirm device clock is correct.',
      });
    }
  });


app.get('/callback', async (req, res) => {
  const { query, session } = req;
  console.log("session  :::::: ", session);
  console.log("query  :::::: ", query);

  const { duo_code, state } = query;
  
  if (!duo_code || typeof duo_code !== 'string') {
    res.render('index.html', { message: `Missing 'duo_code' query parameters` });
    return;
  }

  if (!state || typeof state !== 'string') {
    res.render('index.html', { message: `Missing 'state' query parameters` });
    return;
  }

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
      // .exchangeAuthorizationCodeFor2FAResult(duo_code, 'req.body.username');
      .exchangeAuthorizationCodeFor2FAResult(duo_code, savedUsername);
      
      if(result?.sub) {
        console.log('Success :::: ', result);

      if(result?.auth_result?.result === "allow") {
        const subjectAuthority = `${result.sub}::${result.aud}`;
        const subjectAuthorityEnc= encryptString(subjectAuthority);
        const redirectUrl = `${appUrl}/${subjectAuthorityEnc}`;
        console.log("App Url  :::::: ", redirectUrl);

        res.writeHead(302, { 'Location': redirectUrl });

        res.end();
        }
      } else {
        // Handle authentication failure
        res.render('index.html', {
          message: 'Error decoding Duo result. Confirm device clock is correct.',
        });
      }

}catch(err){
  console.log("Error :::: ", err);
  res.render('index.html', {
      message: 'Error decoding Duo result. Confirm device clock is correct.',
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
