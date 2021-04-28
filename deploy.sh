#!/bin/bash

# Get the latest code from origin
git pull origin master

# Build the docker image
docker build -t mgkaiser/webtest1:`git rev-parse --short HEAD` .

# Tag this build as latest
docker tag mgkaiser/webtest1:`git rev-parse --short HEAD` mgkaiser/webtest1:latest

# Push the docker image to the repository (DockerHub in this case)
docker push mgkaiser/webtest1:`git rev-parse --short HEAD`

# Execute the Helm chart
helm upgrade --install -f ./charts/webtest1/values.yaml webtest1 ./charts/webtest1 --namespace incomm-poc --set image.repository=mgkaiser/webtest1 --set image.tag=`git rev-parse --short HEAD`

# Purge any temporary images created while creating the image
for i in $(docker images --filter=dangling=true --format "{{.ID}}"); do docker rm $i; done

