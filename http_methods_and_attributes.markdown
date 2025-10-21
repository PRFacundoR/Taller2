# Códigos de Estado y Métodos HTTP con Atributos de ASP.NET Core

## Códigos de Estado HTTP
Los códigos de estado HTTP son devueltos por un servidor en respuesta a una solicitud de un cliente. A continuación, se describen los códigos de estado más comunes y su significado, junto con ejemplos de retorno en ASP.NET Core con C#:

- **200 OK**: La solicitud fue exitosa y el servidor devuelve el recurso solicitado.  
  Ejemplo en C#: `return Ok();`

- **201 Created**: La solicitud fue exitosa y se creó un nuevo recurso. Generalmente incluye la URI del recurso creado.  
  Ejemplo en C#: `return Created("/api/productos/1", newProduct);`

- **204 No Content**: La solicitud fue exitosa, pero no hay contenido para devolver. Comúnmente usado en operaciones como eliminaciones donde no se necesita un cuerpo de respuesta.  
  Ejemplo en C#: `return NoContent();`

- **400 Bad Request**: El servidor no puede procesar la solicitud debido a un error del cliente (por ejemplo, sintaxis inválida).  
  Ejemplo en C#: `return BadRequest("Solicitud incorrecta");`

- **401 Unauthorized**: El cliente debe autenticarse para acceder al recurso.  
  Ejemplo en C#: `return Unauthorized();`

- **403 Forbidden**: El cliente está autenticado, pero no tiene permiso para acceder al recurso.  
  Ejemplo en C#: `return Forbid();`

- **404 Not Found**: El recurso solicitado no se encontró en el servidor.  
  Ejemplo en C#: `return NotFound("Recurso no encontrado");`

- **500 Internal Server Error**: Un error genérico que indica un problema en el servidor.  
  Ejemplo en C#: `return StatusCode(500, "Error interno del servidor");`

## Métodos HTTP
Los métodos HTTP definen la acción a realizar sobre un recurso. A continuación, se describen los métodos más comunes, sus propósitos y los atributos correspondientes en ASP.NET Core:

- **GET** `[HttpGet]`: Solicita un recurso del servidor. Los parámetros son visibles en la URL, lo que los hace menos seguros para datos sensibles. Adecuado para recuperar datos.  
  Ejemplo: Obtener una página web o datos de una API.

- **POST** `[HttpPost]`: Envía datos al servidor, generalmente a través de un formulario, para crear un nuevo recurso. Los datos se incluyen en el cuerpo de la solicitud, no son visibles en la URL. Adecuado para enviar formularios o crear recursos.  
  Ejemplo: Enviar datos de registro de un usuario.

- **PUT** `[HttpPut]`: Actualiza un recurso existente o crea uno si no existe. Los datos se envían en el cuerpo de la solicitud, no visibles en la URL.  
  Ejemplo: Actualizar la información de un perfil de usuario.

- **DELETE** `[HttpDelete]**: Elimina un recurso identificado por la URI. Si se elimina correctamente, puede devolver `200 OK` con un cuerpo de respuesta o `204 No Content` sin cuerpo.  
  Ejemplo: Eliminar un producto de una base de datos.

- **HEAD** `[HttpHead]**: Similar a GET, pero el servidor solo devuelve los encabezados de respuesta y el código de estado, no el recurso en sí. Se usa para verificar metadatos o disponibilidad de un recurso.  
  Ejemplo: Comprobar si un recurso existe sin descargarlo.

- **OPTIONS** `[HttpOptions]**: Describe las opciones de comunicación para el recurso objetivo, como los métodos permitidos. A menudo se usa en solicitudes previas de CORS.  
  Ejemplo: Determinar qué métodos HTTP soporta un servidor para un recurso.

- **PATCH** `[HttpPatch]**: Aplica actualizaciones parciales a un recurso, a diferencia de PUT, que reemplaza el recurso completo. Los datos se envían en el cuerpo de la solicitud.  
  Ejemplo: Actualizar un solo campo en el perfil de un usuario.

## Atributos de Enlace de Datos en ASP.NET Core
En ASP.NET Core, los atributos de enlace de datos especifican de dónde se obtienen los datos de una solicitud HTTP para mapearlos a parámetros de un método en un controlador. A continuación, se describen los atributos más comunes, su propósito y cómo usarlos:

- **[FromBody]**: Indica que los datos se obtienen del cuerpo de la solicitud HTTP. Se usa comúnmente con métodos `POST` o `PUT` para recibir datos en formato JSON o XML.  
  **Uso**: Asegúrate de que el cuerpo de la solicitud contenga datos en un formato compatible (por ejemplo, JSON) y que el modelo del parámetro coincida con la estructura de los datos.  
  Ejemplo en C#:
  ```csharp
  [HttpPost]
  public IActionResult CreateProduct([FromBody] Product product)
  {
      if (product == null) return BadRequest("Producto no válido");
      // Lógica para crear el producto
      return Created("/api/productos/1", product);
  }
  ```

- **[FromForm]**: Indica que los datos se obtienen de un formulario enviado en el cuerpo de la solicitud, típicamente con `multipart/form-data`. Se usa con métodos `POST` para formularios HTML.  
  **Uso**: Ideal para formularios que incluyen campos de texto o archivos. El modelo debe coincidir con los nombres de los campos del formulario.  
  Ejemplo en C#:
  ```csharp
  [HttpPost]
  public IActionResult SubmitForm([FromForm] FormModel form)
  {
      if (!ModelState.IsValid) return BadRequest("Formulario inválido");
      // Lógica para procesar el formulario
      return Ok();
  }
  ```

- **[FromHeader]**: Indica que los datos se obtienen de los encabezados de la solicitud HTTP. Útil para leer valores como tokens de autorización o metadatos.  
  **Uso**: Especifica el nombre del encabezado que contiene el valor.  
  Ejemplo en C#:
  ```csharp
  [HttpGet]
  public IActionResult GetData([FromHeader(Name = "Authorization")] string authToken)
  {
      if (string.IsNullOrEmpty(authToken)) return Unauthorized();
      // Lógica para procesar el token
      return Ok();
  }
  ```

- **[FromQuery]**: Indica que los datos se obtienen de los parámetros de la cadena de consulta (query string) en la URL. Comúnmente usado con métodos `GET`.  
  **Uso**: Los parámetros de la URL deben coincidir con los nombres de los parámetros del método.  
  Ejemplo en C#:
  ```csharp
  [HttpGet]
  public IActionResult Search([FromQuery] string query)
  {
      if (string.IsNullOrEmpty(query)) return BadRequest("Consulta vacía");
      // Lógica para buscar con el parámetro query
      return Ok();
  }
  ```

- **[FromRoute]**: Indica que los datos se obtienen de los valores definidos en la ruta de la solicitud. Se usa para capturar parámetros de la URL definidos en la plantilla de ruta.  
  **Uso**: Asegúrate de que la plantilla de ruta del controlador incluya el parámetro correspondiente.  
  Ejemplo en C#:
  ```csharp
  [HttpGet("{id}")]
  public IActionResult GetById([FromRoute] int id)
  {
      if (id <= 0) return BadRequest("ID inválido");
      // Lógica para obtener el recurso por ID
      return Ok();
  }
  ```