using DoctorDiet.Dto;
using DoctorDiet.Models;
using DoctorDiet.Repository.UnitOfWork;
using DoctorDiet.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoctorDiet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomPlanController : Controller
    {
        private CustomPlanService _CustomPlanService;
        IUnitOfWork _unitOfWork;

        public CustomPlanController(CustomPlanService planService, IUnitOfWork unitOfWork)
        {
            _CustomPlanService = planService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllPlans()
        {
            var plans = _CustomPlanService.GetAllPlans();
            _unitOfWork.CommitChanges();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public IActionResult GetPlanById(int id)
        {
            CustomPlan plan = _CustomPlanService.GetPlanById(id);
            if (plan == null)
                return NotFound();

            _unitOfWork.CommitChanges();
            return Ok(plan);
        }

        [HttpGet("Days/{id}")]
        public IActionResult GetDaysById(int PlanId)
        {
            var days = _CustomPlanService.GetDaysById(PlanId);
            if (days == null)
                return NotFound();

            _unitOfWork.CommitChanges();
            return Ok(days);
        }

        [HttpPost]
        public IActionResult AddCustomPlan([FromBody] Patient CurrentPatient)
        {
            var addedPlan = _CustomPlanService.AddCustomPlan(CurrentPatient);
            _unitOfWork.CommitChanges();
            return Ok(addedPlan);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlan(int id, CustomPlan plan)
        {
            if (id != plan.Id)
                return BadRequest();

            _CustomPlanService.UpdatePlan(plan);
            _unitOfWork.CommitChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePlanProperties(int id, CustomPlan plan, params string[] properties)
        {
            if (id != plan.Id)
                return BadRequest();

            _CustomPlanService.UpdatePlanProperties(plan, properties);
            _unitOfWork.CommitChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlan(int id)
        {
            _CustomPlanService.DeletePlan(id);
            _unitOfWork.CommitChanges();
            return NoContent();
        }

        [HttpGet("GetDayCustomPlan")]
        public IActionResult GetDayCustomPlan(int customPlanId)
        {
            CustomDayDTO dayCustomPlan = _CustomPlanService.GetDayCustomPlan(customPlanId);

            if (dayCustomPlan == null)
            {
                return NotFound();
            }

            return Ok(dayCustomPlan);
        }

        [HttpPut("UpdateCustomMeal")]
        public IActionResult UpdateMealInCustomPlan( [FromForm] UpdateMealDTO mealCustomPlanDTO, [FromForm] params string[] properties)
        {
            List<string> Properties = properties[0].Split(',').ToList();
            string[] propertiesArray = Properties.ToArray();

            if (ModelState.IsValid)
            {
                _CustomPlanService.UpdateMealInCustomPlan(mealCustomPlanDTO, propertiesArray);
                _unitOfWork.CommitChanges();
                return Ok();
            }
            return BadRequest();

        }

        [HttpGet("GetDayCustomPlanByCusPlanId/{customPlanId}")]
        public IActionResult GetDayCustomPlanByCusPlanId(int customPlanId)
        {
            if (ModelState.IsValid)
            {
                List<CustomDayDTO> DaysCusPlanDto = _CustomPlanService.GetDayCustomPlanByCusPlanId(customPlanId);
                return Ok(DaysCusPlanDto);
            }
            return BadRequest();

        }

        [HttpGet("GetMealCustomPlanByCusDayId/{customDayId}")]
        public IActionResult GetMealCustomPlanByCusDayId(int customDayId)
        {
            if (ModelState.IsValid)
            {
                List<CustomMealsDTO> MealsCusPlanDto = _CustomPlanService.GetMealsByCusDayId(customDayId);
                return Ok(MealsCusPlanDto);
            }
            return BadRequest();

        }



        [HttpGet("GetCustomMealByID/{customDayId}")]
        public IActionResult GetCustomMealByID(int customDayId)
        {
            if (ModelState.IsValid)
            {
              CustomMealsDTO MealsCusPlanDto = _CustomPlanService.GetCustomMealByID(customDayId);
                return Ok(MealsCusPlanDto);
            }
            return BadRequest();

        }
    }
}
