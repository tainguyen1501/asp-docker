docker build . -t dockername
docker run --name dockername -p 8081:80 -d dockername

build with tag
docker build -t api-redis .
docker tag api-redis tainguyen1501/api-redis:api-redis

Run redis container at 6379 port with the name redis in detached mode.
docker run --name redis -p 6379:6379 -d redis

Run the redis-cli command in the container.
docker exec -it redis redis-cli

docker-compose build