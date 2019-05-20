istioctl kube-inject -f kubernetes/espresso-shop.yaml | kubectl apply -f -
kubectl apply -f istio/all-destination-rules.yaml
kubectl apply -f istio/all-virtual-services.yaml
clear