FROM node:18-alpine AS build 
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build

FROM nginx AS final
COPY --from=build /app/dist /usr/share/nginx/html