# Feeds Web API

Egyszerű web API feedek kezelésére.

## Fordítás, futtatás

### Előfeltételek

.NET 8 SDK telepítése

### Git repository letöltése

- `[cd <base dir>]`
- `git clone https://github.com/28b35a7dbc94f792/feeds-web-api.git`

### Fordítás

- `[cd <base dir>]`
- `cd feeds-web-api/`
- `dotnet build`
- `cd src`
- `dotnet ef database update`

### Futtatás

- `[cd <base dir>]`
- `cd feeds-web-api/src/`
- `dotnet run`

Az alkalmazás alapértelmezés szerint itt lesz elérhető:

http://localhost:5097/

### Tesztek futtatása

- `[cd <base dir>]`
- `cd feeds-web-api/tests/`
- `dotnet test`

## Funkciók

### Swagger

Az alkalmazás indítását követően a végpontok specifikációja itt érhető el:

http://localhost:5097/swagger/index.html

### Végpontok hívása curl segítségével

#### Felhasználó lekérése
- `curl -X 'GET' 'http://localhost:5097/api/users/1'`

#### Felhasználó létrehozása
- `curl -X 'POST' 'http://localhost:5097/api/users' -F 'Username=ezra.hay' -F 'Name=Ezra Hay'`

#### Felhasználó módosítása
- `curl -X 'PUT' 'http://localhost:5097/api/users/4' -F 'Username=ezra.h' -F 'Name=Ezra H.'`

#### Felhasználó törlése
- `curl -X 'DELETE' 'http://localhost:5097/api/users/4'`

#### Feed like-olása (felhasználó 3 like-olja feed 2-t)
- `curl -X 'POST' 'http://localhost:5097/api/users/3/likes/2'`

#### Feed unlike-olása
- `curl -X 'DELETE' 'http://localhost:5097/api/users/3/likes/2'`

#### Összes feed lekérése (külsők is, lásd Megjegyzések)
- `curl -X 'GET' 'http://localhost:5097/api/feeds'`

#### Feed lekérése
- `curl -X 'GET' 'http://localhost:5097/api/feeds/1'`

#### Feedhez tartozó kép lekérése
- `curl -X 'GET' 'http://localhost:5097/api/feeds/1/image' --output feed-image.gif`

#### Feed létrehozása
- `curl -X 'POST' 'http://localhost:5097/api/feeds' -F 'Title=This just happened!' -F 'Description=Incredible!' -F 'AuthorId=2' -F 'Type=1'`
- `curl -X 'POST' 'http://localhost:5097/api/feeds' -F 'Title=This just happened!' -F 'Description=Incredible photo!' -F 'AuthorId=2' -F 'Type=2' -F 'Image=@<képfájl elérési útja>'`
- `curl -X 'POST' 'http://localhost:5097/api/feeds' -F 'Title=This just happened!' -F 'Description=Incredible footage!' -F 'AuthorId=2' -F 'Type=3' -F 'VideoUrl=<videó elérési útja>'`
- `curl -X 'POST' 'http://localhost:5097/api/feeds' -F 'Title=This just happened!' -F 'Description=Incredible footage!' -F 'AuthorId=2' -F 'Type=3' -F 'Image=@<képfájl elérési útja>' -F 'VideoUrl=<videó elérési útja>'`

#### Feed módosítása
- `curl -X 'PUT' 'http://localhost:5097/api/feeds/2' -F 'Title=This just happened!' -F 'Description=Incredible!' -F 'AuthorId=2' -F 'Type=1'`
- `curl -X 'PUT' 'http://localhost:5097/api/feeds/2' -F 'Title=This just happened!' -F 'Description=Incredible photo!' -F 'AuthorId=2' -F 'Type=2' -F 'Image=@<képfájl elérési útja>'`
- `curl -X 'PUT' 'http://localhost:5097/api/feeds/2' -F 'Title=This just happened!' -F 'Description=Incredible footage!' -F 'AuthorId=2' -F 'Type=3' -F 'VideoUrl=<videó elérési útja>'`
- `curl -X 'PUT' 'http://localhost:5097/api/feeds/2' -F 'Title=This just happened!' -F 'Description=Incredible footage!' -F 'AuthorId=2' -F 'Type=3' -F 'Image=@<képfájl elérési útja>' -F 'VideoUrl=<videó elérési útja>'`

#### Feed törlése
- `curl -X 'DELETE' 'http://localhost:5097/api/feeds/2'`

## Megjegyzések

- Minden feedhez kötelező `Title`, `Description` és `AuthorId` megadása, utóbbi egy létező felhasználó ID-ja kell, hogy legyen.
- Text feedekhez (`Type=1`) nem tartozhat `Image` és `VideoUrl`.
- Image feedekhez (`Type=2`) tartoznia kell `Image`-nek, de nem tartozhaz hozzá `VideoUrl`.
- Video feedekhez (`Type=3`) tartozhat `Image` és tartoznia kell `VideoUrl`-nek.
- Felhasználókhoz kötelező egyedi `Username` megadása, a `Name` opcionális.
- Egy felhasználó egy feedet csak egyszer like-olhat.
- Felhasználó törlése a felhasználó feedjeinek és like-jainak törlését vonja maga után.
- Feed törlése a hozzá tartozó like-ok törlését vonja maga után.
- Az alkalmazás képes rá, hogy az összes feed lekérésekor (`GET /api/feeds`) egy külső RSS feedet is lekérjen, és elemeit az eredményhez fűzve visszaadja. Ez alapértelmezés szerint engedélyezve van, de opcionális. A külső RSS feed URL-je és az átveendő elemek maximális száma a `feeds-web-api/src/appsettings.json` fájlban konfigurálható, üres `Url` vagy 0 `MaxItems` beállításával kikapcsolható.
