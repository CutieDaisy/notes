// server.js (Node.js/Express example)

// const express = require('express');
const session = require('express-session');
// const { json, urlencoded } = require('body-parser');
const nunjucks = require('nunjucks');

const express = require('express');
const { json, urlencoded } = require('body-parser');
const config = require('./config');

const Duo = require('@duosecurity/duo_universal');

const app = express();

app.use(json());
app.use(urlencoded({ extended: false }));


// Middleware to disable CORS
app.use((req, res, next) => {
    res.header('Access-Control-Allow-Origin', '*');
    res.header('Access-Control-Allow-Headers', 'Origin, X-Requested-With, Content-Type, Accept');
    next();
});

  // Duo client
  const { clientId, clientSecret, apiHost, redirectUrl, appUrl } = config;
  const duoClient = new Duo.Client({ 
    clientId: clientId, 
    clientSecret: clientSecret, 
    apiHost: apiHost, 
   redirectUrl: redirectUrl });

app.post('/', async (req, res) => {

    console.log("req  :::::: ", req.body );

    const { username, password } = {
        username: "req.body.username",
        password: "req.body.password"
    };


    // if (!username || !password) {
    //   res.render('index.html', { message: 'Missing username or password' });
    //   return;
    // }

    console.log("username :::::: ",username);
    console.log("password :::::: ",password);

    await duoClient.healthCheck();

      const state = duoClient.generateState();
      console.log("State ::::::: ",state);
    //   req.session.duo = { state, username };
    //   const url = duoClient.createAuthUrl(username, state);

    
    const authUrl = duoClient.createAuthUrl(username, state);
    console.log("url :::::: ",authUrl);
  res.json({ authUrl });
});

app.get('/callback', async (req, res) => {

    const { query, session } = req;
    
    console.log("session  :::::: ", session);

    
    const { duo_code, state } = query;
    

    console.log("query  :::::: ", query);

//   duo_code,
//   savedUsername

 var result = await duoClient
    .exchangeAuthorizationCodeFor2FAResult(duo_code, 'req.body.username');

    console.log("result  :::::: ", result);
    if(result.sub) {
      // Handle successful authentication
      console.log('Success :::: ', result);

      console.log("App Url  :::::: ", appUrl);
 
res.render('success.html', { message: JSON.stringify(result, null, '\t') });
return;
    //   res.writeHead(302, { 'Location': appUrl });
    // res.end();

    //   res.json({ success: true, result });
    } else {
      // Handle authentication failure
      console.log('Error :::: ', result);
      res.json({ success: false, error: result });
    }

    // .then(result => {
    //   // Handle successful authentication
    //   console.log('Success :::: ', result);
    // //   res.redirect(302, '/');
    //   res.json({ success: true, result });
    // })
    // .catch(err => {
    //   // Handle authentication failure
    //   console.log('Error :::: ', err);
    //   res.json({ success: false, error: err });
    // });
});

app.listen(3000, () => console.log('Server running on port 3000'));
