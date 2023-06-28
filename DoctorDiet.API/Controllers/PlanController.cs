using DoctorDiet.Dto;
using DoctorDiet.Models;
using DoctorDiet.Repository.UnitOfWork;
using DoctorDiet.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;


namespace DoctorDiet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : Controller
    {
        private readonly PlanService _planService;
        private readonly IUnitOfWork _unitOfWork;

        public PlanController(PlanService planService, IUnitOfWork unitOfWork)
        {
            _planService = planService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllPlans()
        {
            var plans = _planService.GetAllPlans();
            _unitOfWork.CommitChanges();
            return Ok(plans);
        }



        [HttpGet("GetAllPlansByDoctotId")]
        public IActionResult GetAllPlansByDoctotId(string doctorID)
        {
            IEnumerable<Plan> plans = _planService.GetPlanByDoctorId(doctorID);

            return Ok(plans);
        }

        [HttpGet("GetDaysByPlanId")]
        public IActionResult GetDaysByPlanId(int planId)
        {
            IQueryable<DayDTO> days = _planService.GetDaysDTOByPlanId(planId);

            return Ok(days);
        }

        [HttpGet("GetMealsByDayId")]
        public IActionResult GetMealsByDayId(int dayId)
        {
            List<MealDTO> meals = _planService.GetMealsByDayId(dayId);

            return Ok(meals);
        }



        [HttpGet("{id}")]
        public IActionResult GetPlanById(int id)
        {
            var plan = _planService.GetPlanById(id);
            if (plan == null)
                return NotFound();

            _unitOfWork.CommitChanges();
            return Ok(plan);
        }

    [HttpPost]
    public IActionResult AddPlan(AddPlanDTO plan)
    {

      _planService.AddPlan(plan);
      _unitOfWork.CommitChanges();
      return Ok();
    }

    [HttpPut("{id}")]
        public IActionResult UpdatePlan(int id, Plan plan)
        {
            if (id != plan.Id)
                return BadRequest();

            _planService.UpdatePlan(plan);
            _unitOfWork.CommitChanges();
            return NoContent();
        }

        [HttpDelete("DeletePlan{id}")]
        public IActionResult DeletePlan(int id)
        {
            _planService.DeletePlan(id);
            _unitOfWork.CommitChanges();
            return NoContent();
        }

        [HttpGet("GetMealById{id}")]
        public IActionResult GetMealById(int id)
        {
            MealDTO meal = _planService.GetMealById(id);
            if (meal == null)
                return NotFound();

            
            return Ok(meal);
        }

        [HttpPut("UpdateMeal")]
        public IActionResult UpdateMealInPlan([FromForm] UpdateMealDTO mealPlanDTO, [FromForm] params string[] properties)
        {
            List<string> Properties = properties[0].Split(',').ToList();
            string[] propertiesArray = Properties.ToArray();

            if (ModelState.IsValid)
            {
                _planService.UpdateMealInPlan(mealPlanDTO, propertiesArray);
                _unitOfWork.CommitChanges();
                return Ok();
            }
            return BadRequest();

        }

    }
}
