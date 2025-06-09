### 📦 יצירת פרויקט:

```bash
dotnet new webapi -n ProductManagerApi --no-https
cd ProductManagerApi
```

### 📦 הוספת חבילות:

````bash
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Swashbuckle.AspNetCore


## 🌐 Frontend – Angular 17 (Standalone)

### יצירת פרויקט:

```bash
ng new product-manager --standalone --routing --style=css
# בחר Standalone
cd product-manager
````

### 📦 התקנת חבילות:

```bash
npm install bootstrap
```

### 🔧 angular.json:

```json
"styles": ["node_modules/bootstrap/dist/css/bootstrap.min.css", "src/styles.css"]
```

### 🧩 רכיבים:

```bash
ng g component components/ProductList --standalone
ng g component components/ProductForm --standalone
```
