sudo docker run --name papss-ui-gm -p 82:80 -p 8081:80 -v /home/PAPSS/papss-ui/gm/default.conf:/etc/nginx/conf.d/default.conf  -v /home/PAPSS/papss-ui/gm/demo3:/usr/share/nginx/html -d papss-ui-drc
 