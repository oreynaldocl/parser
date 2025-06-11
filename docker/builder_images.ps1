cd ../src
docker build -t dmed-parser -f .\Parser\Dockerfile .
docker build -t dmed-conversor -f .\Conversor\Dockerfile .
docker build -t dmed-api -f .\API\Dockerfile .
