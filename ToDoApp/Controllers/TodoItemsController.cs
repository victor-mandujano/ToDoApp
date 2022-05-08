using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.DataContracts;
using ToDoApp.Models;
using ToDoApp.Services.Abstractions;

namespace ToDoApp.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ILogger<TodoItemsController> _logger;
        private readonly ITodoItemService _todoService;
        private readonly IMapper _mapper;

        public TodoItemsController(ITodoItemService todoService, IMapper mapper, ILogger<TodoItemsController> logger)
        {
            _todoService = todoService;
            _mapper = mapper;
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
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetAll()
        {
            try
            {
                var todos = await _todoService.GetAll();
                var todoDtos = _mapper.Map<IEnumerable<TodoItem>, IEnumerable<TodoItemDto>>(todos);
                return Ok(todoDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while executing {nameof(GetAll)}.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets all completed Todo items
        /// </summary>
        /// <returns></returns>
        /// <response code="200">List of completed Todo items found.</response>
        [HttpGet]
        [ActionName("complete")]
        [Route("api/[controller]/[action]/{isCompleted}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(List<TodoItemDto>)))]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetByCompletion(bool isCompleted)
        {
            var filteredTodos = await _todoService.GetByCompletion(isCompleted);
            var filteredTodoDtos = _mapper.Map<IEnumerable<TodoItem>, IEnumerable<TodoItemDto>>(filteredTodos);
            return Ok(filteredTodoDtos);
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
        public async Task<ActionResult<TodoItemDto>> Get(int id)
        {
            try
            {
                var todo = await _todoService.GetById(id);
                var todoDto = _mapper.Map<TodoItemDto>(todo);
                return Ok(todoDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Todo item not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while executing {nameof(Get)}.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Creates a new Todo item.
        /// </summary>
        /// <param name="value"></param>
        /// <response code="201">Todo item successfully created.</response>
        [HttpPost]
        [Route("api/[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TodoItemDto))]
        public async Task<ActionResult<TodoItemDto>> Create([FromBody] TodoItemDto todoDto)
        {
            var createdTodo = await _todoService.Add(_mapper.Map<TodoItem>(todoDto));
            return CreatedAtAction(nameof(Create), new {id = createdTodo.Id}, _mapper.Map<TodoItemDto>(createdTodo));
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
        public async Task<ActionResult> Update(int id, [FromBody] TodoItemDto todoDto)
        {
            try
            {
                var updatedTodo = await _todoService.Update(id, _mapper.Map<TodoItem>(todoDto));
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Todo item not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while executing {nameof(Update)}.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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
        public async Task<ActionResult> Delete(int id)
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
        public async Task<ActionResult> DeleteAll()
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
        public async Task<ActionResult> Complete(int id)
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
        public async Task<ActionResult> CompleteAll()
        {
            throw new NotImplementedException();
        }
    }
}
