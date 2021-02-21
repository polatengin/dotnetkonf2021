FROM node:15-stretch AS builder

WORKDIR /src

COPY . .

RUN npm install

RUN npm run build

FROM nginx:1.19-alpine

WORKDIR /usr/share/nginx/html

COPY --from=builder /src/build .

RUN nginx
