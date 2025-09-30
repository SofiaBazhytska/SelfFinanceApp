using Microsoft.AspNetCore.Mvc;
using SelfFinance.Core.Services;
using SelfFinanceAPI.Mappers;
using SelfFinance.Shared.Dtos.ReportDtos;
using System.ComponentModel.DataAnnotations;

namespace SelfFinanceAPI.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportController : Controller
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("daily")]
        public async Task<IActionResult> GetDailyReport([FromQuery] DateOnly date)
        {
            if (date == default)
                throw new ValidationException("Date is required for the daily report.");

            var data = await _reportService.GetDailyReportAsync(date);

            var reportDto = new DisplayDailyReportDto
            {
                Date = data.Date,
                Operations = data.Operations.Select(o => o.ToDisplayOperationDto()).ToList(),
                TotalIncome = data.TotalIncome,
                TotalExpenses = data.TotalExpenses
            };

            return Ok(reportDto);
        }

        [HttpGet("period")]
        public async Task<IActionResult> GetPeriodReport([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
        {
            if (startDate == default || endDate == default)
                throw new ValidationException("Start date and end date are required.");

            if (startDate > endDate)
                throw new ValidationException("Start date cannot be later than end date.");

            var data = await _reportService.GetPeriodReportAsync(startDate, endDate);

            var reportDto = new DisplayPeriodReportDto
            {
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Operations = data.Operations.Select(o => o.ToDisplayOperationDto()).ToList(),
                TotalIncome = data.TotalIncome,
                TotalExpenses = data.TotalExpenses
            };

            return Ok(reportDto);
        }
    }
}
