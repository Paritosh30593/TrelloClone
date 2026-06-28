using Microsoft.AspNetCore.Mvc;
using TC.Application.DTO.ColumnDTO;
using TC.Application.ServiceContracts;
using TC.WebAPI.Controllers.Base;

namespace TC.WebAPI.Controllers
{
    public class ColumnController : BaseController
    {
        private readonly IColumnService _columnService;

        public ColumnController(IColumnService columnService)
        {
            _columnService = columnService;
        }

        #region Getters
        public async Task<ActionResult<List<ColumnResponse>>> GetAll()
        {
            List<ColumnResponse> columns = await _columnService.GetAllColumnsAsync();

            return columns == null
                ? NotFound()
                : Ok(columns);
        }

        public async Task<ActionResult<List<ColumnResponse>>> GetAllByBoardId(int boardId)
        {
            List<ColumnResponse> columns = await _columnService.GetAllColumnsByBoardIdAsync(boardId);

            return columns == null
                ? NotFound()
                : Ok(columns);
        }

        public async Task<ActionResult<ColumnResponse>> GetColumnById(int id)
        {
            ColumnResponse column = await _columnService.GetColumnByIdAsync(id);

            return column == null
               ? NotFound()
               : Ok(column);
        }
        #endregion


        #region Adders
        public async Task<ActionResult<ColumnResponse>> AddColumn(ColumnAddRequest columnRequest)
        {
            ColumnResponse column = await _columnService.AddColumnAsync(columnRequest);

            return column == null
                ? BadRequest()
                : Ok(column);
        }
        #endregion


        #region Updaters
        public async Task<ActionResult<ColumnResponse>> UpdateColumn(ColumnUpdateRequest columnRequest)
        {
            ColumnResponse updatedColumn = await _columnService.UpdateColumnAsync(columnRequest);

            return updatedColumn == null
                ? NotFound()
                : Ok(updatedColumn);
        }
        #endregion


        #region Deleters
        public async Task<ActionResult> DeleteColumn(int id)
        {
            bool isDeleted = await _columnService.DeleteColumnAsync(id);

            return isDeleted
                ? Ok()
                : NotFound();
        }
        #endregion
    }
}