FROM node:14.17.0-alpine as build-step
RUN mkdir -p /app
WORKDIR /app
COPY package.json /app
RUN npm install
COPY . /app
RUN npm run build -- --configuration=development

FROM nginx
RUN rm -rf /usr/share/nginx/html/* && rm -rf /etc/nginx/nginx.conf
COPY --from=build-step /app/dist/Frontend /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf

ENTRYPOINT  ["nginx", "-g", "daemon off;"]