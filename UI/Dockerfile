# Use the official Node v16 runtime as the base image.
FROM node:18-alpine

# Sets the container's working directory.
WORKDIR /app

# Copy the package.json and package-lock.json to the container.
COPY package*.json .

RUN npm install

# Copy the UI directory to the container.
COPY . .

# Expose port 4200.
EXPOSE 4200

# Run the ng serve command using the container's Angular CLI.
CMD ["npx", "ng", "serve", "--host", "0.0.0.0"]
