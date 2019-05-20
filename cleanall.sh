kubectl delete -f kubernetes --ignore-not-found=true
kubectl delete -f istio/security --ignore-not-found=true
kubectl delete -f istio --ignore-not-found=true
kubectl delete deployment espresso-shop-reviews-v2 --ignore-not-found=true
clear