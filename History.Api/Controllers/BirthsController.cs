﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using History.Api.Data;
using History.Shared.Models;
using History.Api.Services;

namespace History.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirthsController : ControllerBase
    {
        private readonly HistoryDbContext _context;
        private readonly UnitOfWork unitOfWork;
        public BirthsController(HistoryDbContext context)
        {
            _context = context;
            unitOfWork = new UnitOfWork(_context);
        }
        /// <summary>
        /// Returns all births for a day
        /// </summary>
        ///  <remarks>
        /// Sample request:
        /// /api/Events/GetAllBirthFor?Day=August_1
        /// </remarks>
        /// <returns>Notable births for a day</returns>
        /// <response code="200">Returns notable births for a given day</response>
        /// <response code ="404">If the births' page is null</response>
        [HttpGet("GetAllBirthsForDay", Name = nameof(GetAllBirthsForDay))]
        public ActionResult GetAllBirthsForDay(string Day)
        {
            var Births = unitOfWork.BirthRepository.GetAllForDay(Day);
            if (Births == null)
                return NotFound();
            return Ok(Births);
        }
        /// <summary>
        /// Returns all births for a year
        /// </summary>
        ///  <remarks>
        /// Sample request:
        /// /api/Events/GetAllBirthFor?Year=1996
        /// </remarks>
        /// <returns>Notable births for a year</returns>
        /// <response code="200">Returns notable births for a given year</response>
        /// <response code ="404">If the births' page is null</response>
        [HttpGet("GetAllBirthsForYear", Name = nameof(GetAllBirthsForYear))]
        public ActionResult GetAllBirthsForYear(string Year)
        {
            var Births = unitOfWork.BirthRepository.GetAllForYear(Year);
            if (Births == null)
                return NotFound();
            return Ok(Births);
        }
        /// <summary>
        /// Returns all births for a day and year 
        /// </summary>
        ///  <remarks>
        /// Sample request:
        /// /api/Events/GetAllBirthFor?Day=August_1?Year=1992
        /// </remarks>
        /// <returns>Notable births for a day and year</returns>
        /// <response code="200">Returns notable births for a given day and year</response>
        /// <response code ="404">If the births' page is null</response>
        [HttpGet("GetAllBirthsForDayAndYear", Name = nameof(GetAllBirthsForDayAndYear))]
        public ActionResult GetAllBirthsForDayAndYear(string Year, string Day)
        {
            var Births = unitOfWork.BirthRepository.GetAllForDayAndYear(Year, Day);
            if (Births == null)
                return NotFound();
            return Ok(Births);
        }
        [HttpOptions]
        public IActionResult GetBirthOptions()
        {
            Response.Headers.Add("Allow", "GET");
            return Ok();
        }

        // GET: api/Births/5
        [HttpGet("{id}")]
        public IActionResult GetBirthById(int id)
        {
            var Birth = unitOfWork.BirthRepository.GetById(id);

            if (Birth == null)
            {
                return NotFound();
            }

            return Ok(Birth);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPut("{id}")]
        public IActionResult PutBirth(int id, Event @Birth)
        {


            return NoContent();
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        public ActionResult PostBirth(Event @Birth)
        {
            return NotFound();
        }

        [ApiExplorerSettings(IgnoreApi =true)]
        [HttpDelete("{id}")]
        public IActionResult DeleteBirth(Event ev)
        {
            unitOfWork.BirthRepository.Delete(ev);
            return Ok();

        }

        private bool BirthExists(int id)
        {
            return _context.Birth.Any(e => e.Id == id);
        }
    }
}
