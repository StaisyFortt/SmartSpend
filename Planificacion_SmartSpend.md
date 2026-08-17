# PLANIFICACIÓN DE PROYECTO: SMARTSPEND

## 1. Portada del Trabajo

* **Institución:** [Completar con el nombre de tu Universidad]
* **Asignatura:** Programación 3
* **Proyecto:** SmartSpend (Sistema de Gestión de Gastos Personales)
* **Estudiante:** Staisyfort [Completar Apellidos]
* **Matrícula:** [Completar Matrícula]
* **Fecha:** 15 de agosto de 2026

---

## 2. Nombre del Proyecto de Software

**SmartSpend** – Sistema de Control y Planificación de Gastos Personales.

---

## 3. Tecnología Aplicada

Para el desarrollo de la aplicación se ha seleccionado un conjunto de tecnologías que separan la lógica de negocio de la interfaz visual, asegurando que el sistema sea fácil de mantener y escalar:

* **Backend:**
  * **Plataforma:** .NET 8.0 con ASP.NET Core Web API.
  * **Arquitectura:** Estructura limpia organizada en capas:
    * **Dominio:** Contiene las entidades principales (`Usuario`, `Gasto`, `Categoria`, `MetodoPago`, `Presupuesto`).
    * **Aplicación:** Define la lógica de uso, interfaces de servicios y DTOs.
    * **Infraestructura:** Maneja el acceso a la base de datos usando el patrón repositorio genérico.
    * **API:** Controladores que exponen endpoints HTTP protegidos, documentados con Swagger.
  * **Persistencia:** Entity Framework Core (Code-First) con base de datos en Microsoft SQL Server.
  * **Seguridad:** Autenticación con JSON Web Tokens (JWT) y cifrado de contraseñas con la librería BCrypt.Net.

* **Frontend:**
  * **Estructura e interfaz:** Páginas web estáticas con HTML5, CSS3 personalizado y Bootstrap 5 (con fuente Outfit).
  * **Interactividad:** JavaScript nativo (ES6) para consumir la API mediante peticiones asíncronas (`fetch`).
  * **Visualización:** Chart.js para gráficos dinámicos y FontAwesome para la iconografía.

---

## 4. Objetivo del Proyecto

El objetivo de SmartSpend es ofrecer una herramienta web sencilla que permita a los usuarios registrar sus gastos diarios, clasificarlos y establecer límites de presupuesto mensuales por categoría para evitar gastos excesivos, promoviendo el control financiero en tiempo real.

---

## 5. Alcance del Proyecto

### Cobertura (Lo que incluye el sistema):
* **Acceso de Usuarios:** Registro y login de usuarios con control de sesión seguro para proteger los datos financieros de cada persona.
* **Control de Transacciones:** Registro completo de gastos especificando monto, fecha, descripción, categoría y método de pago.
* **Personalización:** Creación de categorías y métodos de pago adaptados al usuario.
* **Planificación Financiera:** Configuración de presupuestos mensuales asignados a categorías específicas.
* **Visualización de Datos:** Tablero con total de gastos del mes, número de movimientos, gráfico de distribución y alertas visuales de presupuestos sobregirados.
* **Portabilidad:** Importación masiva de gastos y exportación del historial financiero a formato CSV.

### Límites (Lo que NO incluye el sistema):
* Conexión o sincronización automática con cuentas bancarias reales.
* Gestión de múltiples monedas (se asume una única moneda local).
* Módulos de inversión, ahorro a largo plazo o cálculo de impuestos.
* Versiones móviles nativas (la plataforma funciona de forma responsiva en la web).

---

## 6. Cronograma del Proyecto

El desarrollo del proyecto está diseñado para ejecutarse en 5 semanas de la siguiente manera:

| Actividad | Duración Estimada | Entregable / Resultado | Responsable |
| :--- | :--- | :--- | :--- |
| **Fase 1: Análisis e Inicio** | Semana 1 | Requerimientos definidos y diagrama de base de datos. | Staisyfort |
| **Fase 2: Configuración e Infraestructura** | Semana 2 | Solución estructurada en capas, entidades y migraciones en SQL Server. | Staisyfort |
| **Fase 3: Desarrollo Backend (API)** | Semanas 2 - 3 | Controladores y servicios de autenticación, gastos, categorías y presupuestos. | Staisyfort |
| **Fase 4: Desarrollo Frontend** | Semana 4 | Pantallas de login, registro, dashboard, gráficos e integración de peticiones a la API. | Staisyfort |
| **Fase 5: Integración y Pruebas** | Semana 4 | Pruebas de importación/exportación CSV, validación de presupuestos y correcciones. | Staisyfort |
| **Fase 6: Entrega y Cierre** | Semana 5 | Carga de datos de prueba y entrega de la documentación final. | Staisyfort |

---

## 7. Definición del Primer Release

La primera versión funcional se enfocará en las características esenciales de control de gastos, asegurando la privacidad de cada cuenta.

### Requerimientos Funcionales:
* **RF-01 (Acceso Seguro):** El usuario debe poder registrarse e iniciar sesión de forma segura para obtener su token JWT de acceso.
* **RF-02 (Registro de Gastos):** Permitir añadir gastos ingresando monto positivo, fecha, descripción opcional, categoría y método de pago, además de listar los gastos aplicando filtros por fecha y búsqueda de texto.
* **RF-03 (Parámetros Personalizados):** Permitir al usuario crear y modificar sus propias categorías de gastos y métodos de pago.
* **RF-04 (Límites de Presupuesto):** Permitir fijar un monto máximo de dinero a gastar por categoría para un mes y año concretos.
* **RF-05 (Tablero Principal):** Mostrar el resumen del mes en curso, incluyendo el gasto total acumulado, cantidad de movimientos, el gráfico de distribución por categorías y el estado de consumo de cada presupuesto.
* **RF-06 (Portabilidad CSV):** Habilitar botones para descargar los gastos actuales en un archivo CSV e importar de forma masiva a través de una plantilla compatible.

### Requerimientos No Funcionales:
* **RNF-01 (Seguridad de Credenciales):** Las contraseñas en la base de datos deben ser almacenadas mediante algoritmo de hash BCrypt.
* **RNF-02 (Arquitectura Limpia):** El código del backend debe mantener una separación estricta en capas (Domain, Application, Infrastructure, API) para garantizar su mantenibilidad.
* **RNF-03 (Desempeño):** Las consultas y reportes deben responder en menos de 1 segundo en condiciones normales.
* **RNF-04 (Diseño Adaptativo):** La interfaz web debe ser responsiva mediante Bootstrap 5 para visualizarse correctamente tanto en ordenadores como en móviles.
* **RNF-05 (Seguridad en Rutas):** Todos los endpoints de negocio de la API deben validar el token JWT en cada petición HTTP.

---

## 8. Metodología Scrum

Para asegurar un desarrollo organizado, mitigar desviaciones y realizar entregas funcionales incrementales, implementamos el marco ágil de Scrum estructurado en sprints.

### 8.1. Definir Tareas a Ejecutar
Para materializar el sistema, el trabajo se descompone en las siguientes tareas técnicas específicas que guían el desarrollo de cada módulo:

* **T01 (Backend - Arquitectura):** Estructurar la solución en .NET 8 separando los proyectos de dominio, aplicación, infraestructura y API, y configurar las referencias correspondientes.
* **T02 (Backend - Base de Datos):** Crear las entidades de datos en la capa Domain, definir las relaciones mediante Fluent API y configurar el `DbContext` de Entity Framework Core para conectarse a SQL Server.
* **T03 (Backend - Migraciones):** Implementar las migraciones de EF Core, aplicar la siembra (seeding) de datos iniciales en la base de datos y verificar la persistencia física en SQL Server.
* **T04 (Backend - Seguridad):** Implementar el cifrado de contraseñas con BCrypt y el generador de tokens JWT configurando el tiempo de expiración y claves secretas en `appsettings.json`.
* **T05 (Backend - Catálogos):** Construir los servicios y controladores REST para el mantenimiento (CRUD) de Categorías y Métodos de Pago asociados al usuario logueado.
* **T06 (Backend - Transacciones):** Implementar la lógica del controlador de Gastos, incluyendo el guardado de datos, la validación de montos positivos y las consultas con filtros por rango de fechas y texto.
* **T07 (Backend - Integración de Archivos):** Codificar el servicio para la lectura de streams de archivos, validación sintáctica del CSV, mapeo hacia las entidades y cálculo de importación masiva.
* **T08 (Frontend - Maquetado):** Diseñar las vistas del Login y Registro de usuarios usando HTML5 semántico y CSS3 con layouts responsivos basados en Bootstrap 5.
* **T09 (Frontend - Autenticación):** Programar las funciones en JavaScript nativo para capturar credenciales del formulario, enviarlas al backend y almacenar el token de retorno en el `localStorage` del navegador.
* **T10 (Frontend - Interfaz Principal):** Diseñar la plantilla del Dashboard Web principal instalando las tarjetas superiores y las secciones para desplegar el historial de gastos.
* **T11 (Frontend - Visualización):** Conectar el Dashboard a los servicios de la API usando `fetch` para listar transacciones y pintar el gráfico de distribución interactivo con la librería Chart.js.
* **T12 (Frontend - Lógica de Presupuestos):** Integrar visualmente las alertas y barras de progreso de presupuestos calculando porcentajes consumidos mediante la manipulación dinámica del DOM en JavaScript.
* **T13 (Frontend - Integración CSV):** Conectar los botones de exportación e importación de CSV del frontend con los endpoints respectivos de la API REST.
* **T14 (QA/Pruebas):** Ejecutar pruebas manuales de registro de usuarios, creación de presupuestos límites y subida de archivos CSV inválidos para verificar el comportamiento de los errores controlados.

---

### 8.2. Definir el Equipo de Trabajo
Se define un equipo multidisciplinar con roles específicos y responsabilidades delimitadas para cubrir todo el ciclo de desarrollo:

* **Product Owner / Analista de Negocio:** **Staisyfort**
  * *Habilidades requeridas:* Comprensión del modelo de negocio financiero, definición y priorización de requisitos de usuario, modelado conceptual de datos y validación funcional del software.
  * *Responsabilidades:* Redactar y priorizar las historias de usuario del backlog. Aclarar dudas funcionales al equipo de desarrollo y dar el visto bueno (firma de aceptación) a cada funcionalidad terminada durante la review de cada sprint.
* **Scrum Master & Desarrollador Frontend:** **[Nombre del Compañero 1]**
  * *Habilidades requeridas:* Dominio de HTML5, CSS3, Bootstrap 5 para diseño responsivo, control de flujos asíncronos en JavaScript (ES6) mediante `fetch`, manejo de manipulación del DOM y renderizado dinámico con Chart.js.
  * *Responsabilidades:* Eliminar obstáculos que impidan el avance técnico, coordinar las ceremonias y encargarse del maquetado visual, la interacción de la interfaz y la integración con las rutas del backend.
* **Desarrollador Backend:** **[Nombre del Compañero 2]**
  * *Habilidades requeridas:* Programación sólida en C# (.NET 8), desarrollo de APIs REST, Entity Framework Core, gestión de bases de datos relacionales en SQL Server, cifrado de datos (BCrypt) e implementación de seguridad con tokens JWT.
  * *Responsabilidades:* Configurar la solución del backend en capas, programar la lógica del negocio en la base de datos y la capa de aplicación, codificar los controladores expuestos por la API y configurar la seguridad perimetral del sistema.

---

### 8.3. Herramientas que Usarían
* **GitHub Projects:** Funciona como nuestro tablero Kanban virtual. Las historias de usuario y tareas técnicas se mueven a través de cinco columnas: *Backlog* (pila de producto), *To Do* (tareas del sprint seleccionado), *In Progress* (en desarrollo), *Testing* (en pruebas manuales o unitarias) y *Done* (completadas y validadas por el Product Owner).
* **Control de Versiones (GitHub):** Trabajamos con ramas bajo un esquema de GitFlow simplificado. La rama `main` contiene el software estable para entrega, `develop` consolida las características completadas y se crean ramas temporales tipo `feature/nombre-funcionalidad` para el trabajo diario (por ejemplo, `feature/auth-jwt` o `feature/csv-importer`). Todo cambio requiere un Pull Request revisado y aprobado por otro compañero antes de integrarse a `develop`.
* **Discord / WhatsApp:** Canales de comunicación diaria para resolver dudas rápidas e impartir las ceremonias agiles de forma remota.

---

### 8.4. Definir las Épicas
El trabajo se estructura en 4 épicas principales que agrupan las historias de usuario con objetivos comunes:

* **Épica 1: Acceso Seguro y Gestión de Usuarios (AUTH)**
  * Cubre los flujos de creación de cuentas, inicio de sesión seguro, cifrado criptográfico y la protección de datos por usuario mediante políticas de seguridad JWT.
* **Épica 2: Control de Transacciones y Categorías (TX)**
  * Agrupa las funciones básicas del sistema relacionadas con la creación, edición, listado y eliminación de gastos individuales, categorías personalizadas y métodos de pago.
* **Épica 3: Planificación de Presupuestos y Monitoreo (BUD)**
  * Contempla las reglas de negocio necesarias para establecer límites mensuales financieros por categoría de gasto y pintar el panel dinámico con barras de consumo y alertas críticas.
* **Épica 4: Integración e Importación/Exportación de Datos (DAT)**
  * Abarca el desarrollo de las herramientas de lectura y escritura para el intercambio de información financiera mediante archivos de formato plano (CSV).

---

### 8.5. Ceremonias de Scrum
Para encajar en las 5 semanas de cronograma de la asignatura, el desarrollo se divide en **2 Sprints de 2 semanas cada uno** (usando la semana 1 para análisis preliminar y preparación de requerimientos).

#### **Sprint 1: Cimientos y Core de la API Backend**
* **Duración:** Del 24 de agosto al 4 de septiembre de 2026.
* **Sprint Goal:** Desarrollar y verificar la arquitectura de base de datos SQL Server y los endpoints esenciales de autenticación (JWT) y transacciones (gastos y categorías) mediante pruebas en Swagger.
* **Reuniones y Calendario:**
  * **Sprint Planning:** 24 de agosto de 2026 (9:00 AM - 11:00 AM). Definición de objetivos, estimación final de tareas del Sprint Backlog (T01 a T06) y asignación a los miembros del equipo.
  * **Daily Standups:** Lunes, miércoles y viernes a las 9:00 AM (duración máxima de 15 minutos vía Discord). Cada miembro responde las preguntas base del marco ágil e informa de impedimentos inmediatos.
  * **Sprint Review:** 4 de septiembre de 2026 (4:00 PM - 5:00 PM). Demostración en vivo de los endpoints backend ejecutando peticiones reales desde Swagger. Firma de aceptación por parte de Staisyfort (Product Owner).
  * **Sprint Retrospective:** 4 de septiembre de 2026 (5:15 PM - 6:00 PM). Discusión sobre la dinámica interna, revisión de problemas encontrados con la configuración de Entity Framework y acuerdos de mejora técnica para el siguiente ciclo.

#### **Sprint 2: Desarrollo Frontend, Integración y Pruebas**
* **Duración:** Del 7 de septiembre al 18 de septiembre de 2026.
* **Sprint Goal:** Integrar el frontend web al backend mediante JavaScript asíncrono, renderizar reportes visuales con Chart.js, habilitar presupuestos y portabilidad de datos, y corregir errores visuales para la entrega final.
* **Reuniones y Calendario:**
  * **Sprint Planning:** 7 de septiembre de 2026 (9:00 AM - 11:00 AM). Planificación de tareas restantes (T07 a T14) y desglose de las historias de integración en el tablero.
  * **Daily Standups:** Lunes, miércoles y viernes a las 9:00 AM (duración máxima de 15 minutos vía Discord).
  * **Sprint Review:** 18 de septiembre de 2026 (4:00 PM - 5:00 PM). Demostración completa del sistema integrado: simulación de login, registro de gastos, revisión de alertas presupuestarias e importación de un archivo CSV de ejemplo.
  * **Sprint Retrospective & Cierre:** 18 de septiembre de 2026 (5:15 PM - 6:00 PM). Evaluación final del proyecto, consolidación de lecciones aprendidas y empaquetado del entregable final.

---

### 8.6. Historias de Usuario

#### **HU-01: Registro de nuevo usuario**
* **Épica:** AUTH | **Puntos de Historia (SP):** 3
* **Descripción:** Como usuario nuevo quiero crear una cuenta con mi correo y una contraseña para poder tener un perfil personalizado.
* **Criterios de Aceptación:**
  * **Escenario de éxito:** Si el correo no existe en la base de datos y la contraseña es válida, el sistema cifra la contraseña usando BCrypt y crea el registro del usuario asociándolo a un ID único en la tabla `Usuarios`.
  * **Validación de formato:** El backend debe validar mediante expresiones regulares que el correo cumpla el formato estándar (`ejemplo@dominio.com`). Si es inválido, debe retornar un error `400 Bad Request` explicando el fallo.
  * **Validación de duplicados:** Si el correo ya existe en la base de datos, el sistema debe impedir el registro y retornar el mensaje "El correo ya se encuentra registrado".
  * **Redirección:** Al completarse el registro con éxito, el sistema del frontend muestra un mensaje flotante y redirige al usuario automáticamente a la vista de login (`index.html`).

#### **HU-02: Inicio de sesión seguro (JWT)**
* **Épica:** AUTH | **Puntos de Historia (SP):** 3
* **Descripción:** Como usuario registrado quiero iniciar sesión con mis credenciales para acceder al sistema y visualizar mi información.
* **Criterios de Aceptación:**
  * **Generación de Token:** Ante datos válidos, el servidor web emite un token JWT que contiene el `UsuarioId`, `Email` y firma segura.
  * **Persistencia del Estado:** El script frontend guarda el token devuelto en el `localStorage` del navegador bajo la clave `jwtToken` y redirige a `dashboard.html`.
  * **Manejo de Errores:** Si el correo no existe o la contraseña no coincide con el hash almacenado, el backend devuelve un error `401 Unauthorized` con el texto "Credenciales incorrectas" (sin detallar cuál campo falló por motivos de seguridad).
  * **Rutas Protegidas:** Si un usuario intenta ingresar directamente a `dashboard.html` sin el token JWT en memoria, el frontend debe redirigirlo forzosamente a `index.html`.

#### **HU-03: Crear categoría de gastos personalizada**
* **Épica:** TX | **Puntos de Historia (SP):** 2
* **Descripción:** Como usuario quiero crear categorías personalizadas de gastos para clasificar mis egresos financieros de manera estructurada.
* **Criterios de Aceptación:**
  * **Restricción de Nombre:** El nombre de la categoría es obligatorio, no debe contener caracteres especiales y su longitud máxima permitida es de 50 caracteres.
  * **Vinculación de Datos:** Cada categoría guardada en la base de datos se relaciona mediante clave foránea con el `UsuarioId` del token autenticado. Ningún usuario puede ver o utilizar las categorías creadas por otros.
  * **Refresco Visual:** Al guardar una nueva categoría desde el modal del Dashboard, el selector de categorías del formulario de gastos debe actualizarse de inmediato sin requerir recargar toda la página web.

#### **HU-04: Registrar un gasto cotidiano**
* **Épica:** TX | **Puntos de Historia (SP):** 5
* **Descripción:** Como usuario quiero agregar un gasto indicando monto, fecha, categoría y método de pago para documentar mis movimientos financieros.
* **Criterios de Aceptación:**
  * **Validación del Monto:** El monto ingresado debe ser un número positivo mayor que cero. El formulario y la API deben bloquear montos menores o iguales a cero devolviendo un error de validación.
  * **Validación de Fechas:** La fecha seleccionada por el usuario no puede ser posterior a la fecha y hora actual del servidor. Si el usuario no elige una fecha, el sistema debe asignarle automáticamente la del día en curso.
  * **Asociaciones Requeridas:** El gasto debe vincularse forzosamente a una categoría activa y a un método de pago guardados en la cuenta del usuario.

#### **HU-05: Listar gastos con filtros y búsquedas**
* **Épica:** TX | **Puntos de Historia (SP):** 3
* **Descripción:** Como usuario quiero visualizar una lista histórica de mis gastos registrados y poder filtrarla para auditar mis consumos específicos.
* **Criterios de Aceptación:**
  * **Estructura de Visualización:** La tabla de gastos debe mostrar la fecha formateada (`DD/MM/AAAA`), el nombre de la categoría, el método de pago utilizado, la descripción provista y el monto en formato de moneda local.
  * **Filtros Temporales:** Al seleccionar una fecha de inicio y una de fin, la API debe devolver únicamente los registros comprendidos en dicho intervalo inclusive.
  * **Búsqueda por Texto:** Al escribir en la barra de búsqueda, el frontend debe filtrar y actualizar la lista mostrada basándose en coincidencias parciales (sin importar mayúsculas o minúsculas) de la descripción del gasto.

#### **HU-06: Definir presupuestos límite**
* **Épica:** BUD | **Puntos de Historia (SP):** 3
* **Descripción:** Como usuario quiero establecer un presupuesto máximo para una categoría en un mes y año específicos para evitar gastar de más.
* **Criterios de Aceptación:**
  * **Ingreso de Datos:** El usuario selecciona el mes (1 al 12), el año, la categoría a limitar y el monto máximo asignado.
  * **Control de Duplicidad:** Si ya existe un límite presupuestado para la combinación de mes, año y categoría dada, el backend debe actualizar el monto máximo existente (usando lógica *Upsert*) en vez de arrojar un error de clave duplicada o guardar filas redundantes.
  * **Validación de Valores:** El monto del presupuesto debe ser estrictamente un valor numérico positivo.

#### **HU-07: Resumen rápido en el Dashboard**
* **Épica:** BUD | **Puntos de Historia (SP):** 2
* **Descripción:** Como usuario quiero ver el total acumulado de gastos y la cantidad de transacciones registradas del mes para conocer mi estado general de forma rápida.
* **Criterios de Aceptación:**
  * **Cálculo Automático:** Al cargar el dashboard, el sistema debe realizar una consulta agregada para sumar los montos de todos los gastos del usuario correspondientes al mes y año actuales.
  * **Actualización en Caliente:** Si el usuario realiza una acción que altere la base de datos (por ejemplo, registrar un gasto nuevo o importar un archivo), las tarjetas con el conteo de movimientos y la suma de gastos deben refrescar su valor en pantalla.

#### **HU-08: Estado de presupuestos consumidos**
* **Épica:** BUD | **Puntos de Historia (SP):** 5
* **Descripción:** Como usuario quiero ver de forma gráfica las alertas y barras de avance de mis presupuestos para monitorear mis límites de consumo.
* **Criterios de Aceptación:**
  * **Comparación de Totales:** Para cada categoría con presupuesto en el mes activo, el sistema calcula el total de gastos acumulado y lo compara con el monto límite establecido.
  * **Interfaz Visual:** Debe pintar una barra de progreso que indique el porcentaje consumido (por ejemplo: `60% gastado de $5,000`).
  * **Alertas Dinámicas:** Si el consumo se encuentra por debajo del 80%, la barra se mantiene en color verde. Si se ubica entre el 80% y el 99%, cambia a color amarillo. Si alcanza o sobrepasa el 100%, la barra se pinta en color rojo y despliega un icono de alerta crítica (ejemplo: "Límite Excedido").

#### **HU-09: Descarga de reporte de gastos (CSV)**
* **Épica:** DAT | **Puntos de Historia (SP):** 2
* **Descripción:** Como usuario quiero exportar mi historial de gastos en un archivo plano para poder trabajarlo fuera de la plataforma (por ejemplo, en Excel).
* **Criterios de Aceptación:**
  * **Generación del Archivo:** Al hacer clic en "Descargar Reporte", el backend genera en memoria un archivo de texto con codificación UTF-8 separado por comas.
  * **Esquema de Datos:** La primera fila debe contener las cabeceras exactas: `Id,Fecha,Categoria,MetodoPago,Descripcion,Monto`. Las filas siguientes mapean la información del listado actual.

#### **HU-10: Importación masiva de gastos**
* **Épica:** DAT | **Puntos de Historia (SP):** 5
* **Descripción:** Como usuario quiero subir un archivo CSV con mis gastos para realizar un registro masivo rápido.
* **Criterios de Aceptación:**
  * **Validación de Archivo:** El sistema debe verificar que el archivo subido no esté vacío y tenga formato CSV válido. De lo contrario, devuelve un error claro: "Archivo no compatible".
  * **Mapeo y Consistencia:** Para cada fila del archivo, el sistema busca si la categoría y el método de pago escritos existen en la cuenta del usuario. Si no existen, puede crear la categoría automáticamente con estado activo o detener la carga de esa fila informando la advertencia.
  * **Informe de Resultados:** Una vez concluido el proceso, la pantalla debe mostrar un mensaje detallado informando el estatus de la carga, por ejemplo: *"Carga finalizada. Se registraron 15 gastos correctamente, 2 filas omitidas por formato inválido"*.

---

## 9. Plan de Pruebas

Para garantizar la calidad de la plataforma, el correcto funcionamiento de las reglas de negocio y mitigar riesgos antes del despliegue final, se establece la siguiente estrategia de pruebas.

### 9.1. Relación de Requerimientos y Casos de Pruebas
Los requerimientos definidos se vinculan directamente con las historias de usuario de la metodología Scrum, sirviendo de base para el diseño del plan de pruebas:

| Código Requerimiento | Tipo | Descripción Simplificada | Historias de Usuario Asociadas |
| :--- | :--- | :--- | :--- |
| **RF-01** | Funcional | Registro y autenticación de usuarios mediante cifrado y JWT. | HU-01, HU-02 |
| **RF-02** | Funcional | Módulo CRUD de gastos y consulta histórica filtrada. | HU-04, HU-05 |
| **RF-03** | Funcional | Personalización de categorías y métodos de pago por perfil. | HU-03 |
| **RF-04** | Funcional | Definición de límites mensuales de presupuesto por categoría. | HU-06 |
| **RF-05** | Funcional | Visualización en tiempo real de consumos y alertas visuales. | HU-07, HU-08 |
| **RF-06** | Funcional | Módulos de descarga y carga masiva de transacciones en CSV. | HU-09, HU-10 |
| **RNF-01** | No Funcional | Cifrado criptográfico de contraseñas de usuario en base de datos. | HU-01 |
| **RNF-02** | No Funcional | Arquitectura de software en capas sin dependencias circulares. | Todas las tareas |
| **RNF-03** | No Funcional | Tiempo de respuesta de API REST inferior a 1 segundo. | HU-05, HU-07, HU-10 |
| **RNF-04** | No Funcional | Interfaz web responsiva adaptada a móviles (Bootstrap 5). | HU-02, HU-07, HU-08 |
| **RNF-05** | No Funcional | Protección perimetral de endpoints de API mediante token JWT. | HU-02, HU-04, HU-06 |

---

### 9.2. Criterios de Aceptación y Rechazo de Pruebas
Los criterios de evaluación dictaminan si un caso de prueba o la suite completa se considera aprobada para su integración o liberación:

* **Criterios de Aceptación (Aprobación):**
  * **Cero Errores Críticos:** No deben existir errores de tipo bloqueante (ejemplo: excepciones no controladas 500, fugas de datos cruzados entre usuarios, fallos de inicio de sesión).
  * **Cobertura Mínima:** Las pruebas unitarias automatizadas deben cubrir al menos el 80% del código lógico de la capa de aplicación.
  * **Alineación de Interfaces:** El diseño visual en pantallas de diferentes tamaños (responsivo) cumple con los componentes básicos sin desbordes ni texto superpuesto.
  * **Validación de Códigos HTTP:** Los endpoints REST deben retornar códigos de estado estándar de la industria (ej. exitosos: `200 OK`, `201 Created`; errores del cliente: `400 Bad Request`, `401 Unauthorized`, `404 Not Found`).
  * **Veracidad del CSV:** El archivo exportado debe coincidir exactamente con los datos que el usuario tiene en su tabla, y las cabeceras deben ser idénticas al formato establecido.

* **Criterios de Rechazo (Fallo):**
  * **Contraseñas en Texto Plano:** Si en la base de datos se almacena cualquier contraseña sin aplicar el hash de BCrypt.
  * **Errores de Concurrencia:** Fugas de datos que permitan a un usuario autenticado ver, modificar o eliminar transacciones de otro usuario.
  * **Rendimiento Lento:** Tiempos de respuesta locales de la API superiores a 2 segundos en solicitudes de listados o almacenamiento.
  * **Desactualización del UI:** El Dashboard no refresca los montos ni alerta de sobregiro inmediatamente tras registrar un gasto.

---

### 9.3. Herramientas de Pruebas y Justificación
Se han seleccionado herramientas del ecosistema .NET y tecnologías estándar para pruebas funcionales y de interfaz:

* **xUnit (Pruebas Unitarias e Integración Backend):**
  * *Justificación:* Es el framework moderno estándar recomendado por Microsoft para .NET 8. Proporciona una sintaxis limpia, aislamiento rápido de datos de prueba y alta velocidad de ejecución en paralelo.
* **Moq (Simulación de Dependencias / Mocking):**
  * *Justificación:* Permite aislar completamente las pruebas de la capa de servicios (`Application`) simulando las llamadas a los repositorios de datos de la base de datos. De esta forma, las pruebas unitarias validan solo lógica de negocio pura sin tocar base de datos real.
* **Microsoft.AspNetCore.Mvc.Testing (Pruebas de Integración API):**
  * *Justificación:* Levanta un servidor web de prueba en memoria (`WebApplicationFactory`) para simular peticiones HTTP reales a la API REST. Permite evaluar flujos completos de controladores, middlewares de autorización JWT y el ruteo de ASP.NET Core sin necesidad de un despliegue físico.
* **Selenium WebDriver (Pruebas Automatizadas End-to-End):**
  * *Justificación:* Es la biblioteca de automatización obligatoria de la asignatura para la interacción y automatización sobre la interfaz de usuario en navegadores (Chrome/Edge). Permite simular los flujos interactivos de SmartSpend y validar los escenarios de Happy Path, pruebas negativas y de límites mediante código estructurado en C# y xUnit.
* **Postman (Pruebas Manuales de API):**
  * *Justificación:* Permite realizar pruebas exploratorias rápidas de los endpoints construidos, guardando colecciones de peticiones con tokens JWT almacenados en variables de entorno para simplificar la validación manual antes del desarrollo del frontend.

---

### 9.4. Cronograma de Ejecución de Pruebas
Las pruebas se desarrollan en paralelo con el ciclo de programación del software:

* **Semana 2 (Fase de Cimientos):**
  * Configuración del proyecto de pruebas en la solución .NET.
  * Redacción de las primeras pruebas unitarias del modelo de persistencia (Infrastructure/Persistencia).
* **Semana 3 (Fase Backend):**
  * Ejecución de pruebas unitarias sobre los servicios lógicos en la capa `Application` (Usuario, Gastos, Presupuestos) usando Moq.
  * Validación manual de la API a través de Postman y documentación Swagger a medida que se liberan los controladores.
* **Semana 4 (Fase Frontend e Integración):**
  * Pruebas manuales exploratorias de la UI (responsividad en diferentes tamaños de pantalla).
  * Pruebas de integración frontend/backend (validar el flujo de promesas en JS y persistencia en la base de datos).
  * Creación y ejecución de scripts automatizados básicos en Selenium (ej. flujo automático de inicio de sesión y registro de gasto).
* **Semana 5 (Fase de Cierre):**
  * Pruebas de estrés y límites: importación de archivos CSV con cargas grandes de datos.
  * Regresión automatizada completa (ejecutar la suite de xUnit y Selenium para asegurar que los cambios de última hora no dañaron la funcionalidad existente).
  * Firma y aprobación del reporte final de pruebas.

---

### 9.5. Plantillas de Casos de Pruebas
Para la ejecución y documentación estructurada de las pruebas, los casos se dividen en los flujos principales evaluados en la asignatura (Login y CRUD sobre formulario):

#### **FLUJO 1: Inicio de Sesión (Login)**

##### **Caso 01: Camino Feliz (Login Exitoso)**
* **ID:** CP-AUTH-01 (Happy Path)
* **Precondiciones:** El usuario "wanda@prueba.com" debe estar previamente registrado en la base de datos con la contraseña cifrada "MiPasswordSeguro123".
* **Pasos de Ejecución:**
  1. Navegar a la URL de inicio del sistema (`index.html`).
  2. Introducir en el campo de texto del correo: `wanda@prueba.com`.
  3. Introducir en el campo de texto de la contraseña: `MiPasswordSeguro123`.
  4. Hacer clic en el botón de envío "Ingresar".
* **Resultado Esperado:** El sistema autentica con éxito las credenciales, redirige al usuario a la pantalla del dashboard (`dashboard.html`), almacena de forma local el token JWT en el objeto `localStorage` del navegador y despliega el mensaje de bienvenida.
* **Estatus de Prueba:** [Pendiente / Aprobado / Rechazado]

##### **Caso 02: Prueba Negativa (Login Fallido por Contraseña Errónea)**
* **ID:** CP-AUTH-02 (Negative Test)
* **Precondiciones:** El usuario "wanda@prueba.com" debe estar previamente registrado en la base de datos con la contraseña cifrada "MiPasswordSeguro123".
* **Pasos de Ejecución:**
  1. Navegar a la URL de inicio del sistema (`index.html`).
  2. Introducir en el campo del correo: `wanda@prueba.com`.
  3. Introducir una contraseña incorrecta: `ClaveInvalida123`.
  4. Hacer clic en el botón de envío "Ingresar".
* **Resultado Esperado:** El sistema deniega el acceso con un código HTTP `401 Unauthorized`, mantiene la navegación en la página de login (`index.html`), no almacena ningún token en `localStorage` y muestra un banner de error que indica: "Credenciales incorrectas".
* **Estatus de Prueba:** [Pendiente / Aprobado / Rechazado]

##### **Caso 03: Prueba de Límites (Login con Formato de Correo Inválido)**
* **ID:** CP-AUTH-03 (Boundary Test)
* **Precondiciones:** El servidor de base de datos y la API se encuentran en ejecución.
* **Pasos de Ejecución:**
  1. Navegar a la URL de inicio del sistema (`index.html`).
  2. Escribir en el correo una cadena que no cumpla con la estructura de correo electrónico estándar (ejemplo: `wanda.pruebadominio.com` o una cadena vacía).
  3. Dejar vacío el campo de la contraseña.
  4. Hacer clic en el botón "Ingresar".
* **Resultado Esperado:** El formulario en el cliente previene el envío debido a las validaciones de estructura HTML5 / JS y la API REST responde con un error de validación `400 Bad Request` indicando que el correo no es válido o que los campos requeridos no se han suministrado, impidiendo procesar la autenticación.
* **Estatus de Prueba:** [Pendiente / Aprobado / Rechazado]

---

#### **FLUJO 2: Operaciones CRUD (Formulario de Gastos)**

##### **Caso 04: Camino Feliz (Registro de Gasto Exitoso)**
* **ID:** CP-TX-01 (Happy Path)
* **Precondiciones:** El usuario ha iniciado sesión con éxito y se encuentra posicionado en el modal para agregar un nuevo gasto en el Dashboard. Existen las categorías "Comida" y el método de pago "Efectivo" en su perfil.
* **Pasos de Ejecución:**
  1. Escribir en el campo del Monto: `350.00`.
  2. Escribir en el campo de Descripción: `Compra de insumos de cena`.
  3. Seleccionar la categoría "Comida" y el método de pago "Efectivo".
  4. Hacer clic en "Guardar Gasto".
* **Resultado Esperado:** El sistema procesa la solicitud enviando la cabecera del token JWT, inserta el registro del gasto en la base de datos SQL Server, cierra el modal, refresca los acumulados de gastos del Dashboard y agrega la transacción a la tabla de historial.
* **Estatus de Prueba:** [Pendiente / Aprobado / Rechazado]

##### **Caso 05: Prueba Negativa (Registro de Gasto con Monto Inválido)**
* **ID:** CP-TX-02 (Negative Test)
* **Precondiciones:** El usuario ha iniciado sesión con éxito y se encuentra posicionado en el modal para agregar un nuevo gasto en el Dashboard.
* **Pasos de Ejecución:**
  1. Escribir en el campo del Monto un valor negativo o cero: `-150.00` o `0.00`.
  2. Rellenar los campos requeridos restantes con datos válidos.
  3. Hacer clic en "Guardar Gasto".
* **Resultado Esperado:** La interfaz del cliente bloquea el envío o la API del backend rechaza la petición con un código `400 Bad Request` argumentando que el monto de la transacción debe ser un valor positivo mayor que cero. El registro no se almacena en la base de datos y la interfaz muestra una advertencia de validación.
* **Estatus de Prueba:** [Pendiente / Aprobado / Rechazado]

##### **Caso 06: Prueba de Límites (Registro de Gasto en Límite de Caracteres en Descripción)**
* **ID:** CP-TX-03 (Boundary Test)
* **Precondiciones:** El usuario ha iniciado sesión con éxito y se encuentra posicionado en el modal para agregar un nuevo gasto en el Dashboard.
* **Pasos de Ejecución:**
  1. Escribir en el campo del Monto: `100.00`.
  2. Escribir en el campo de Descripción una cadena de texto que contenga exactamente **200 caracteres** (el límite máximo definido en la entidad de base de datos) o intentar ingresar 201 caracteres para comprobar el recorte.
  3. Completar los campos requeridos y hacer clic en "Guardar Gasto".
* **Resultado Esperado:** El sistema debe aceptar y guardar con éxito el registro si la cadena tiene exactamente 200 caracteres. Si se intenta ingresar un carácter número 201, la interfaz debe recortar el texto o la API debe sanitizarlo y guardarlo cortando la cadena al límite configurado para evitar excepciones de desbordamiento en la base de datos SQL Server.
* **Estatus de Prueba:** [Pendiente / Aprobado / Rechazado]

---

### 9.6. Equipos de Pruebas y Responsabilidades
Las pruebas se distribuyen entre los miembros del equipo para asegurar revisiones independientes:

* **Staisyfort (Product Owner & QA Lead):**
  * Diseña los escenarios críticos de prueba (UAT), revisa los resultados finales, coordina el cronograma de pruebas y firma la aprobación de calidad de cada sprint.
* **[Nombre del Compañero 1] (Frontend & UI Tester):**
  * Encargado de codificar los scripts automatizados de Selenium WebDriver para pruebas funcionales de UI. Realiza pruebas manuales de responsividad en navegadores y validación de diseño gráfico.
* **[Nombre del Compañero 2] (Backend & API Tester):**
  * Responsable de escribir la suite de pruebas automatizadas en xUnit y Moq para el backend. Ejecuta pruebas de rendimiento y estrés en los endpoints REST e inspecciona la consistencia física de la base de datos SQL Server.

---

### 9.7. Plan de Automatización de Pruebas
Con miras a implementar pruebas automatizadas recurrentes en fases posteriores, establecemos una estrategia estructurada bajo la pirámide de automatización de pruebas:

* **Estrategia y Niveles de Automatización:**
  1. **Capa Unitarias (xUnit + Moq):** Centrada en probar métodos individuales de servicios en la capa `Application` de manera aislada. Por ejemplo, testear que `RegistrarGastoAsync` arroje una excepción si el monto es negativo, simulando el comportamiento de `IGenericRepository` con datos estáticos de Moq.
  2. **Capa de Integración de API (xUnit + WebApplicationFactory):** Valida el ciclo completo de peticiones HTTP en los controladores. Simula llamadas a los endpoints `/api/Auth/login` y `/api/Gastos` validando códigos de respuesta e integración de middlewares de seguridad.
  3. **Capa de Extremo a Extremo o E2E (Selenium WebDriver):** Automatiza flujos completos de interacción de usuario de forma directa en el navegador real. El código de prueba levantará el driver del navegador (ChromeDriver/EdgeDriver) mediante código de C# u otro lenguaje soportado, navegará a `index.html` para rellenar los datos de sesión, enviará el formulario y simulará la inserción de gastos en el modal verificando el comportamiento dinámico. El script configurará además la **captura de pantalla automática** ante cada fallo de aserción y generará el **reporte HTML automatizado** de los resultados de ejecución.
* **Integración Continua (CI):**
  * Planteamos la configuración de un archivo de flujo en GitHub Actions (`.github/workflows/dotnet-tests.yml`). En cada Pull Request que se realice a la rama `develop`, la plataforma de integración ejecutará automáticamente la compilación del backend, la restauración de dependencias NuGet y el comando `dotnet test` para asegurar que ninguna modificación rompa los casos de prueba establecidos.

---

## 10. Ejecución y Demostración

En esta sección se adjunta la evidencia visual de la ejecución de las pruebas automatizadas del sistema, demostrando el comportamiento real de los scripts frente a la interfaz web y la base de datos.

### 10.1. Evidencia de Pruebas Unitarias e Integración (xUnit / Postman)
* *[Pegar aquí captura de pantalla de la ventana "Explorador de Pruebas" de Visual Studio con las pruebas unitarias de xUnit ejecutadas con éxito (checks en verde)]*
* *[Pegar aquí captura de Postman realizando peticiones HTTP a los endpoints de la API (login de usuario, inserción de gastos y obtención de presupuestos)]*

### 10.2. Evidencia de Ejecución de Pruebas de Interfaz (Selenium WebDriver)
* *[Pegar aquí captura de pantalla del navegador web controlado automáticamente por ChromeDriver ejecutando el flujo del camino feliz en el Dashboard]*
* *[Pegar aquí captura de la terminal de comandos mostrando la finalización exitosa de los tests E2E y el reporte final de consola]*

### 10.3. Capturas Automáticas de Pantalla y Reporte HTML
* *[Pegar aquí captura del reporte final interactivo en formato HTML generado tras finalizar la ejecución de Selenium]*
* *[Pegar aquí captura de pantalla del listado de imágenes automáticas guardadas en la carpeta de evidencias físicas de las pruebas]*

---

## 11. Bibliografía y Referencias

* **Metodología Scrum:**
  * Schwaber, K., & Sutherland, J. (2020). *La Guía de Scrum: Las Reglas del Juego: La Guía Definitiva de Scrum*. Scrum.org. https://scrumguides.org/docs/scrumguide/v2020/2020-Scrum-Guide-Spanish-European.pdf

* **Plataforma y Arquitectura del Backend (.NET & API):**
  * Microsoft. (2024). *Documentación de ASP.NET Core: Creación de API web con ASP.NET Core*. Microsoft Learn. https://learn.microsoft.com/es-es/aspnet/core/web-api/
  * Microsoft. (2024). *Información general sobre Entity Framework Core*. Microsoft Learn. https://learn.microsoft.com/es-es/ef/core/

* **Seguridad y Cifrado:**
  * IETF (Internet Engineering Task Force). (2015). *RFC 7519: JSON Web Token (JWT)*. IETF Datatracker. https://datatracker.ietf.org/doc/html/rfc7519
  * Provos, N., & Mazières, D. (1999). *A Future-Adaptable Password Scheme (BCrypt)*. USENIX Association. https://www.usenix.org/legacy/publications/library/proceedings/usenix99/provos.html

* **Frontend y Visualización:**
  * Bootstrap. (2023). *Bootstrap v5.3 Documentation: Layout, Components, and Utilities*. https://getbootstrap.com/docs/5.3/
  * Chart.js. (2024). *Chart.js v4.4 Documentation: Doughnut and Pie Charts*. https://www.chartjs.org/docs/latest/charts/doughnut.html

* **Control de Versiones y Gestión:**
  * Driessen, V. (2010). *A successful Git branching model (GitFlow)*. https://nvie.com/posts/a-successful-git-branching-model/

* **Automatización de Pruebas:**
  * Selenium. (2024). *Selenium WebDriver Documentation: Getting Started*. https://www.selenium.dev/documentation/webdriver/
