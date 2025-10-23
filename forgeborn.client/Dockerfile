FROM node:alpine AS builder
WORKDIR /app
COPY ["package*.json", "."]
RUN npm install
COPY . .
RUN npm run build

FROM nginx:alpine
COPY --from=builder /app/dist /usr/share/nginx/html
COPY ./nginx.conf /etc/nginx/conf.d
EXPOSE 3000
ENTRYPOINT ["nginx"]
CMD ["-g", "daemon off;"] 