### Build Docker Container

```docker build -t stations-app .```

### Run Docker Container

```docker run --rm -d -p 8080:80 --name app-container stations-app```

Project runs on ```http://localhost:8080```
