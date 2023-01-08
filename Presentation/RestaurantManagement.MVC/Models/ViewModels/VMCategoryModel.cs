﻿using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NuGet.Protocol;
using RestaurantManagement.Application;
using RestaurantManagement.Domain.Dto;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.MVC.Models.ComponentModels;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;

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
        public int getDailyTotalOrderCount
        {
            get
            {
                return service.OrderRepository.GetList(x => x.CreatedDate > DateTime.Now.Date).ToList().Count;
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

        public WidgetModel getAbout
        {
            get
            {
                WidgetModel model = new WidgetModel();
                model.widgetName = "Kategori Hakkında";
                model.widgetText = DateTime.Now.ToString("dd.MM.yyyy dddd");
                model.widgetIcon = "fas fa-tags";
                model.widgetLink = "/Category/List";

                List<ListEntity> links = new List<ListEntity>()
                {
                    new ListEntity()
                    {
                        Text="Kategori Tablosu",SubText="Category/List"
                    }
                };

                model.widgetActions.AddRange(links);

                List<ListEntity> entities = new List<ListEntity>()
                {
                    new ListEntity()
                    {
                        Text="Aktif Kategori Sayısı",
                        SubText="Aktif Kategori Sayısı",
                        Count = getActiveCategoryCount.ToString(),
                        iconClass="text-success fas fa-tag",
                        textClass="label-light-success",
                        Tooltip ="Aktif olan tüm kategorilerin sayısını belirtir"

                    },
                    new ListEntity()
                    {
                        Text="Pasif Kategori Sayısı",
                        SubText="Pasif Kategori Sayısı",
                        Count = getPassiveCategoryCount.ToString(),
                        iconClass="text-danger fas fa-tag",
                        textClass="label-light-danger",
                        Tooltip ="Pasif olan tüm kategorilerin sayısını belirtir"
                    },
                    new ListEntity()
                    {
                        Text="Toplam Kategori",
                        SubText="Toplam Kategori",
                        Count = getTotalCategoryCount.ToString(),
                        iconClass="text-primary fas fa-tag",
                        textClass="label-light-primary",
                        Tooltip ="Tüm kategorilerin sayısını belirtir"
                    },
                    new ListEntity()
                    {
                        Text="Fiyat Ortalamaları",
                        SubText="Fiyat Ortalamaları",
                        Count = getAveragePrice,
                        iconClass="text-info fas fa-chart-area",
                        textClass="label-light-info",
                        Tooltip ="Tüm ürünlerin fiyat ortalamasını belirtir"
                    },
                };
                model.widgetEntities.AddRange(entities);

                return model;
            }
        }
        public WidgetModel getRecentlyAdded
        {
            get
            {
                int count = 3;
                var data = list.Where(x => x.Active).OrderByDescending(x => x.CreatedDate)
                .Select(x => new ListEntity
                {
                    Id = x.Id.ToString(),
                    Text = x.Name,
                    SubText = DateTime.Now
                    .Subtract(x.CreatedDate).Days != 0
                    ? DateTime.Now.Subtract(x.CreatedDate).Days + " gün önce"
                    : " Bugün",
                    bgClass = "symbol-light-success"
                })
                .Take(count)
                .ToList();

                WidgetModel model = new WidgetModel();
                model.widgetName = "Son Eklenenler";
                model.widgetText = String.Format("Son eklenen {0} kategori", count);
                List<ListEntity> links = new List<ListEntity>()
                {
                    new ListEntity()
                    {
                        Text="Kategori Tablosu",SubText="Category/List"
                    }
                };

                model.widgetActions.AddRange(links);

                model.widgetEntities.AddRange(data);

                return model;
            }
        }
        public WidgetModel getRecentlyUpdated
        {
            get
            {
                int count = 3;
                var data = list.Where(x => x.Active).OrderByDescending(x => x.UpdatedDate)
                .Select(x => new ListEntity
                {
                    Id = x.Id.ToString(),
                    Text = x.Name,
                    SubText = DateTime.Now
                    .Subtract(x.UpdatedDate).Days != 0
                    ? DateTime.Now.Subtract(x.UpdatedDate).Days + " gün önce"
                    : " Bugün",
                    bgClass = "symbol-light-danger"
                })
                .Take(count)
                .ToList();

                WidgetModel model = new WidgetModel();
                model.widgetName = "Son Güncellenenler";
                model.widgetText = String.Format("Son güncellenen {0} kategori", count);
                List<ListEntity> links = new List<ListEntity>()
                {
                    new ListEntity()
                    {
                        Text="Kategori Tablosu",SubText="Category/List"
                    }
                };

                model.widgetActions.AddRange(links);

                model.widgetEntities.AddRange(data);

                return model;
            }
        }
        public WidgetModel getSalesChart
        {
            get
            {

                WidgetModel model = new WidgetModel();
                model.widgetName = "Kategori Satış Grafiği";

                List<ListEntity> links = new List<ListEntity>()
                {
                    new ListEntity()
                    {
                        Text="Kategori Tablosu",SubText="Category/List"
                    }
                };

                List<ListEntity> data = new List<ListEntity>()
                {
                    new ListEntity()
                    {
                        Text="Günlük Satış",SubText="/Category/List",textClass="text-info",iconClass="fas fa-chart-bar"
                    },
                    new ListEntity()
                    {
                        Text="Haftalık Satış",SubText="/Category/List",textClass="text-warning",iconClass="fas fa-chart-bar"
                    },
                    new ListEntity()
                    {
                        Text="Aylık Satış",SubText="/Category/List",textClass="text-danger",iconClass="fas fa-chart-bar"
                    },
                    new ListEntity()
                    {
                        Text="Yıllık Satış",SubText="/Category/List",textClass="text-success",iconClass="fas fa-chart-bar"
                    },
                };

                model.widgetActions.AddRange(links);

                model.widgetEntities.AddRange(data);

                return model;
            }
        }
        public WidgetModel getMostOrdered
        {
            get
            {
                WidgetModel model = new WidgetModel();
                model.widgetName = "En çok sipariş edilenler";

                List<ListEntity> links = new List<ListEntity>()
                {
                    new ListEntity()
                    {
                        Text="Kategori Tablosu",SubText="Category/List"
                    }
                };

                model.widgetActions.AddRange(links);

                var entities = service.OrderDetailRepository.GetTopSellingCategories()
                    .Select(x => new ListEntity()
                    {
                        Id = x.Category.Id.ToString(),
                        Count = x.Count.ToString(),
                        SubText = "Sipariş adeti",
                        Text = x.Category.Name
                    }).ToList();
                ;

                model.widgetEntities.AddRange(entities);

                return model;
            }
        }

    }
}
