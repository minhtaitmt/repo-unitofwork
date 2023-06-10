﻿using AutoMapper;
using GenericRepositoryAndUnitofWork.Entities;
using GenericRepositoryAndUnitofWork.Models;
using GenericRepositoryAndUnitofWork.Repositories;
using GenericRepositoryAndUnitofWork.UnitofWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace GenericRepositoryAndUnitofWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitofWork unitOfWork, IMapper mapper) 
        { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory() 
        { 
            var categories = await _unitOfWork.CategoryRepository.GetAllCategoryAsync();
            if (categories == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<List<CategoryModel>>(categories);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<CategoryModel>(category);
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryModel model)
        {
            var category = _mapper.Map<Category>(model);
            try
            {
                await _unitOfWork.CategoryRepository.AddCategoryAsync(category);
                await _unitOfWork.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }
            return CreatedAtAction("GetCategoryById", new {id =  category.Id}, _mapper.Map<CategoryModel>(category));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryModel model)
        {
            if(id != model.Id)
            {
                return NotFound();
            }
            var category = _mapper.Map<Category>(model);
            try
            {
                await _unitOfWork.CategoryRepository.UpdateCategoryAsync(id, category);
                await _unitOfWork.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _unitOfWork.CategoryRepository.DeleteCategoryAsync(id);
                await _unitOfWork.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }
            return NoContent();
        }


        



    }
}
