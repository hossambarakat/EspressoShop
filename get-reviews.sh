#!/bin/bash 

REVIEWS_IP="$(kubectl get svc | grep espresso-shop-reviews-svc | awk '{ print $4 }')"
REVIEWS_URL="http://${REVIEWS_IP}:8092/api/version"

while true;
do 
    curl -s "${REVIEWS_URL}"
    echo "" ;
    sleep 0.5
done

# while true; do curl -s "http://$(kubectl get svc | grep espresso-shop-reviews-svc | awk '{ print $4 }'):8092/api/version" ; echo "" ; sleep 0.5; done

# while true; do curl -s "http://espresso-shop-reviews-svc:8092/api/version" ; echo "" ; sleep 0.5; done