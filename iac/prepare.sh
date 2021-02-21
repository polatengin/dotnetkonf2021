PROJECT_NAME="dotnetkonf2021"

LOGIN_COUNT=$(az account list --query "length([])")

if [[ $LOGIN_COUNT -eq 0 ]]
then
  az login
fi

echo "Creating Resource Group"

az group create --name "${PROJECT_NAME}-rg" --location "westeurope"

echo "Creating Container Registry"

# az acr create --resource-group "${PROJECT_NAME}-rg" --name "${PROJECT_NAME}acr" --sku Basic --admin-enabled true

ACR_ID=$(az acr show --resource-group "${PROJECT_NAME}-rg" --name "${PROJECT_NAME}acr" --query "id" --output tsv)

echo "Creating Kubernetes"

# az aks create --resource-group "${PROJECT_NAME}-rg" --name "${PROJECT_NAME}-aks" --kubernetes-version 1.19.7 --no-ssh-key --attach-acr ${ACR_ID}

az aks get-credentials --resource-group "${PROJECT_NAME}-rg" --name "${PROJECT_NAME}-aks"

az acr login  --name "${PROJECT_NAME}acr" --expose-token

kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v0.40.1/deploy/static/provider/cloud/deploy.yaml

kubectl apply -f ./ingress.yaml
