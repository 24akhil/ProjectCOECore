FROM node:latest as node
WORKDIR /app
#COPY . .   inside WeatherApp

COPY ./WeatherApp .


RUN npm install
RUN npm rebuild node-sass
RUN npm run build --prod

# ENTRYPOINT [ "ng","serve"]  
# stage 2
FROM nginx:alpine

COPY --from=node /app/dist/WeatherMan-app /usr/share/nginx/html
