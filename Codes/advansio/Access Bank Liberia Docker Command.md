## Build
docker build --no-cache -t papss-bridge1-09052025 .

## Run
docker run --restart always --name papss-bridge1-09052025 -p 11000:11000 -v /opt/papss-bridge1/:/opt/papss-bridge1/ -v /opt/papss-bridge1/logs/:/logs/ -e PAPSS_ID=LR100012 -e PAPSS_BRIDGE2_BASE_URL=http://172.17.0.1:10500/ --log-opt max-size=10m --log-opt max-file=3 -d papss-bridge1-09052025 