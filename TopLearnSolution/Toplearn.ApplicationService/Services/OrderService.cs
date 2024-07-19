using Microsoft.EntityFrameworkCore;
using Toplearn.ApplicationService.Contract.Dtos.Order;
using Toplearn.ApplicationService.Contract.IService;
using Toplearn.Domain.IRepoitories;
using Toplearn.Domain.Models;
using Toplearn.InfraStructure.Context;

namespace Toplearn.ApplicationService.Services
{
    public class OrderService : IOrderService
    {
        private readonly TopleaarnContext context;
        public readonly ICourseRepository courseRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderDetailRepository orderDetailRepository;
        public OrderService(TopleaarnContext context, ICourseRepository courseRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            this.context = context;
            this.courseRepository = courseRepository;
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
        }

        public bool AddOrder(AddOrderDto dto,string userName)
        {
            var user = context.Users.Where(x => x.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                var course = courseRepository.GetAll().Where(x => x.Id == dto.CourseId).FirstOrDefault();
                if (course != null)
                {
                    var userOrder = context.Orders.Where(x => x.UserId == user.Id).ToList();
                    foreach(var item in userOrder)
                    {
                        if (item.CourseId == course.Id && item.UserId == user.Id)
                        {
                            return false;
                        }
                };
                    var order = new Order()
                    {
                        CourseId = course.Id,
                        UserId = user.Id,
                        CourseTitle = course.Title,
                        IsDelet = false,
                        IsFinaly = false,
                        Price = course.Price,
                        Id = dto.OrderId,
                    };
                    orderRepository.Add(order);
                    orderRepository.SaveChanges();
                    double sum = 0;
                    var orderList = orderRepository.GetAll().Where(x => x.UserId == user.Id).ToList();
                    for (int i = 0; i < orderList.Count; i++)
                    {
                        sum += orderList[i].Price;
                    }
                    var detail = new OrderDetail()
                    {
                        OrderId = order.Id,
                        OrderSum = sum
                    };
                    orderDetailRepository.Add(detail);
                    orderDetailRepository.SaveChanges();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
