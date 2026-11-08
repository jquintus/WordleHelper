# BlazorApp1
 
[![Deploy to GitHub Pages](https://github.com/jquintus/WordleHelper/actions/workflows/main.yml/badge.svg)](https://github.com/jquintus/WordleHelper/actions/workflows/main.yml)

[![pages-build-deployment](https://github.com/jquintus/WordleHelper/actions/workflows/pages/pages-build-deployment/badge.svg)](https://github.com/jquintus/WordleHelper/actions/workflows/pages/pages-build-deployment)

Visit the site and [getting some hints for Wordle](https://quintussential.com/WordleHelper/)

# Running Local

You can run this project locally in docker

```bash
docker run --rm -it -p 5123:5123 -v "$PWD:/src" -w /src \
    -e ASPNETCORE_URLS=http://0.0.0.0:5123 \
    mcr.microsoft.com/dotnet/sdk:6.0 \
    dotnet run --project WordleHelper/WordleHelper.csproj --no-launch-profile
```
