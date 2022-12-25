using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NuGet.Protocol;
using RestaurantManagement.Application;
using RestaurantManagement.Domain.Dto;
using RestaurantManagement.Domain.Entities;
using System.Collections;
using System.Collections.Generic;

namespace RestaurantManagement.MVC.Models.ViewModels
{
    public class VMCategoryModel
    {
        private IUnitOfWork service;
        private List<Category> list;

        public VMCategoryModel(IUnitOfWork service)
        {
            this.service = service;
            this.list = service.CategoryRepository.GetList(default, false);
        }
        public List<Category> GetCategories
        {
            get
            {
                return list.Where(x => x.Active).ToList();
            }
        }
        public int getActiveCategoryCount
        {
            get
            {
                return list.Where(x => x.Active).ToList().Count;
            }
        }
        public int getPassiveCategoryCount
        {
            get
            {
                return list.Where(x => x.Active == false).ToList().Count;
            }
        }
        public string getAveragePrice
        {
            get
            {
                return service.ProductRepository.GetList()
                    .Average(x => x.Price).ToString("#.##");

            }
        }
        public int getTotalCategoryCount
        {
            get
            {
                return list.Count;

            }
        }
        public List<TopSellingCategory> TopSellingCategories
        {
            get
            {
                var data = service.OrderDetailRepository.GetTopSellingCategories();

                return data;
            }
        }
        public int TotalOrder
        {
            get
            {
                return service.OrderDetailRepository.GetList(x => x.CreatedDate > DateTime.Now.Date).Count;
            }
        }
    }
}
