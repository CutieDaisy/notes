Account Branch

1
ACCOUNTNO
1270049201

server {
    listen 82;

    location / {
        root   C:/papss/web;
        index  index.html index.htm;
        try_files $uri $uri/ =404;
    }

    location /admin {
     #   proxy_pass http://localhost:11201;
		proxy_pass http://localhost:7221;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection 'upgrade';
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
    }
}



Download the new UI build for TBL :
https://we.tl/t-LPSh5xvHkm