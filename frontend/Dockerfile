### STAGE 1: Restore ###
FROM node:20.19-alpine AS restore
WORKDIR /usr/src/app
COPY package.json ./
COPY package-lock.json ./
RUN npm ci
### STAGE 2: Build ###
FROM node:20.19-alpine AS build
WORKDIR /usr/src/app
COPY --from=restore /usr/src/app /usr/src/app
COPY . .
RUN npm run build

### STAGE 3: Run ###
FROM nginx:1.17.1-alpine
COPY --from=build /usr/src/app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf

CMD ["/bin/sh",  "-c",  "exec nginx -g 'daemon off;'"]

EXPOSE 80