# 🔗 URL Shortening Service

## 📌 Descripción

Este proyecto es un servicio backend para acortar URLs largas, permitiendo generar enlaces más cortos, redirigirlos y obtener estadísticas de uso.

Fue desarrollado como parte de un challenge técnico basado en el roadmap de backend, implementando funcionalidades clave como persistencia, redirección y conteo de accesos.

---

## 🚀 Características

* 🔹 Acortamiento de URLs largas
* 🔹 Redirección automática mediante código corto
* 🔹 Conteo de accesos (click tracking)
* 🔹 Listado de URLs almacenadas
* 🔹 Actualización de URLs existentes
* 🔹 Eliminación de URLs
* 🔹 Persistencia con base de datos SQLite
* 🔹 Arquitectura por capas (Endpoints, Services, DTOs)

---

## 🛠️ Tecnologías utilizadas

* **.NET (ASP.NET Core Minimal API)**
* **Entity Framework Core**
* **SQLite**
* **C#**

---

## 📂 Estructura del proyecto

```
URLShortening/
│── Endpoints/        # Definición de rutas (Minimal API)
│── Services/         # Lógica de negocio
│── DTOs/             # Objetos de transferencia de datos
│── Models/           # Entidades
│── Data/             # DbContext
│── Program.cs        # Punto de entrada
```

---

## ⚙️ Instalación y ejecución

### 1. Clonar el repositorio

```bash
git clone https://github.com/tu-usuario/url-shortening.git
cd url-shortening
```

### 2. Restaurar dependencias

```bash
dotnet restore
```

### 3. Ejecutar la aplicación

```bash
dotnet run
```

La API estará disponible en:

```
https://localhost:5001
```

---

## 📡 Endpoints

### 🔹 Crear URL corta

```http
POST /shorten-url
```

**Body:**

```json
{
  "url": "https://google.com"
}
```

---

### 🔹 Redirección

```http
GET /r/{shortCode}
```

Redirige automáticamente a la URL original.

---

### 🔹 Obtener estadísticas

```http
GET /stats/{shortCode}
```

**Respuesta:**

```json
{
  "url": "https://google.com",
  "clicks": 10
}
```

---

### 🔹 Listar URLs

```http
GET /urls
```

---

### 🔹 Actualizar URL

```http
PUT /urls/{shortCode}
```

---

### 🔹 Eliminar URL

```http
DELETE /urls/{shortCode}
```

---

## 🧠 Decisiones de diseño

* Se utilizó **Minimal API** para mantener el proyecto simple y enfocado.
* Separación en capas para mejorar la mantenibilidad.
* Uso de DTOs para desacoplar la API del modelo de datos.
* Servicio dedicado para manejo de clicks (`ClickService`).

---

## ⚠️ Mejoras futuras

* [ ] Soporte para **custom alias**
* [ ] Expiración de URLs
* [ ] Validación avanzada de URLs
* [ ] Manejo de colisiones en códigos cortos
* [ ] Implementación de interfaces y DI más robusto
* [ ] Autenticación de usuarios
* [ ] Dashboard frontend

---

## 📊 Estado del proyecto

🟢 Funcional y estable
🟡 En mejora continua

---

## 📎 Inspiración

Proyecto basado en:

https://roadmap.sh/projects/url-shortening-service

---

## 👨‍💻 Autor

Desarrollado por **Federico Núñez**

---

## 📄 Licencia

Este proyecto es de uso educativo y puede ser utilizado como base para otros desarrollos.
