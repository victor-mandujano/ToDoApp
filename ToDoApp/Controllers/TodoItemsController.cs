using Microsoft.AspNetCore.Mvc;
using ToDoApp.DataContracts;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(ILogger<TodoItemsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets all Todo items
        /// </summary>
        /// <returns></returns>
        /// <response code="200">List of Todo items found.</response>
        [HttpGet]
        [Route("api/[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(List<TodoItemDto>)))]
        public IEnumerable<TodoItemDto> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all completed Todo items
        /// </summary>
        /// <returns></returns>
        /// <response code="200">List of completed Todo items found.</response>
        [HttpGet]
        [ActionName("complete")]
        [Route("api/[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(List<TodoItemDto>)))]
        public IEnumerable<TodoItemDto> GetAllCompleted()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Gets a specific Todo item by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Todo item found.</response>
        /// <response code="404">Item not found.</response>
        [HttpGet]
        [Route("api/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoItemDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public TodoItemDto Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new Todo item.
        /// </summary>
        /// <param name="value"></param>
        /// <response code="201">Todo item successfully created.</response>
        [HttpPost]
        [Route("api/[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TodoItemDto))]
        public TodoItemDto Create([FromBody] TodoItemDto todoDto)
        {
            // https://stackoverflow.com/questions/1860645/create-request-with-post-which-response-codes-200-or-201-and-content
            // https://stackoverflow.com/questions/19199872/best-practice-for-restful-post-response
            // Returning the new object fits with the REST principle of "Uniform Interface - Manipulation of resources through representations."
            // The complete object is the representation of the new state of the object that was created.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates an existing Todo item.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <response code="204">Todo item successfully updated.</response>
        /// <response code="404">Todo item not found.</response>
        [HttpPut]
        [Route("api/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public void Update(int id, [FromBody] TodoItemDto todoDto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes an existing Todo item.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Todo item successfully deleted.</response>
        /// <response code="404">Todo item not found.</response>
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes all Todo items.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Todo item successfully deleted.</response>
        [HttpDelete]
        [Route("api/[controller]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Completes the given Todo item
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Todo item successfully updated.</response>
        /// <response code="404">Todo item not found.</response>
        [HttpPatch]
        [ActionName("complete")]
        [Route("api/[controller]/{id}/complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public void Complete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Completes all Todo items
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Todo items successfully updated.</response>
        /// <response code="404">No todo item not found.</response>
        [HttpPatch]
        [ActionName("complete")]
        [Route("api/[controller]/complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public void CompleteAll()
        {
            throw new NotImplementedException();
        }
    }
}
