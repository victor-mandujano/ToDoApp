using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Core.DataContracts;
using ToDoApp.Core.Models;
using ToDoApp.Core.Services.Abstractions;

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
        /// Gets all Todo items.
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
        /// Gets all Todo items with the specified completion status.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">List of Todo items found with the specified completion status.</response>
        [HttpGet]
        [ActionName("complete")]
        [Route("api/[controller]/[action]/{isCompleted}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(List<TodoItemDto>)))]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetByCompletion(bool isCompleted)
        {
            try
            {
                var filteredTodos = await _todoService.GetByCompletion(isCompleted);
                var filteredTodoDtos = _mapper.Map<IEnumerable<TodoItem>, IEnumerable<TodoItemDto>>(filteredTodos);
                return Ok(filteredTodoDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while executing {nameof(GetByCompletion)}.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Gets an existing Todo item with the specified ID.
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
        /// <param name="todoDto"></param>
        /// <response code="201">Todo item successfully created.</response>
        [HttpPost]
        [Route("api/[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TodoItemDto))]
        public async Task<ActionResult<TodoItemDto>> Create([FromBody] TodoItemDto todoDto)
        {
            try
            {
                var createdTodo = await _todoService.Add(_mapper.Map<TodoItem>(todoDto));
                return CreatedAtAction(nameof(Get), new { id = createdTodo.Id }, _mapper.Map<TodoItemDto>(createdTodo));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while executing {nameof(Create)}.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates an existing Todo item.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoDto"></param>
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
                await _todoService.Update(id, _mapper.Map<TodoItem>(todoDto));
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
            try
            {
                await _todoService.DeleteById(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Todo item not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while executing {nameof(Delete)}.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deletes all Todo items.
        /// </summary>
        /// <response code="204">Todo items successfully deleted.</response>
        [HttpDelete]
        [Route("api/[controller]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteAll()
        {
            try
            {
                await _todoService.DeleteAll();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while executing {nameof(DeleteAll)}.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates the completion status for an existing Todo item.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isCompleted"></param>
        /// <response code="204">Todo item successfully updated.</response>
        /// <response code="404">Todo item not found.</response>
        [HttpPatch]
        [ActionName("complete")]
        [Route("api/[controller]/{id}/complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateCompletion(int id, bool isCompleted)
        {
            try
            {
                await _todoService.UpdateCompletionById(id, isCompleted);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Todo item not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while executing {nameof(UpdateCompletion)}.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates the completion status for all existing Todo items.
        /// </summary>
        /// <param name="isCompleted"></param>
        /// <response code="204">Todo items successfully updated.</response>
        [HttpPatch]
        [ActionName("complete")]
        [Route("api/[controller]/complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateCompletionAll(bool isCompleted)
        {
            try
            {
                await _todoService.UpdateCompletionAll(isCompleted);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while executing {nameof(UpdateCompletionAll)}.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
