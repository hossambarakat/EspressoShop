docker build . -f EspressoShop.Web/dockerfile -t hossambarakat/espresso-shop-web
docker push hossambarakat/espresso-shop-web

docker build . -f EspressoShop.ProductCatalog/dockerfile -t hossambarakat/espresso-shop-product-catalog
docker push hossambarakat/espresso-shop-product-catalog

docker build . -f EspressoShop.Reviews/dockerfile -t hossambarakat/espresso-shop-reviews
docker push hossambarakat/espresso-shop-reviews