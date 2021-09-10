using AutoMapper;
using BLL.EntitiesDTO;
using DAL.Entities;

namespace BLL
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {

            CreateMap<UserForRegistrationDto, User>()
                .ForMember(u => u.UserName, o => o.MapFrom(x => x.Email));

            CreateMap<UserForAuthenticationDto, User>()
               .ForMember(u => u.UserName, o => o.MapFrom(x => x.Email))
               .ForMember(u => u.PasswordHash, o => o.MapFrom(x => x.Password.GetHashCode()));

            CreateMap<Tree, TreeDto>()
                .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
                .ForMember(u => u.Name, o => o.MapFrom(x => x.Name))
                .ForMember(u => u.Description, o => o.MapFrom(x => x.Description))
                .ForMember(u => u.TreeType, o => o.MapFrom(x => x.TreeType))
                .ForMember(u => u.Color, o => o.MapFrom(x => x.Color));

            CreateMap<TreeDto, Tree>()
                .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
                .ForMember(u => u.Name, o => o.MapFrom(x => x.Name))
                .ForMember(u => u.Description, o => o.MapFrom(x => x.Description))
                .ForMember(u => u.TreeType, o => o.MapFrom(x => x.TreeType))
                .ForMember(u => u.Color, o => o.MapFrom(x => x.Color));
           
            CreateMap<Size, SizeDto>()
                .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
                .ForMember(u => u.NameOfSize, o => o.MapFrom(x => x.NameOfSize));

            CreateMap<SizeDto, Size>()
                .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
                .ForMember(u => u.NameOfSize, o => o.MapFrom(x => x.NameOfSize));

            CreateMap<TreeSizeAndPrice, TreeSizeAndPriceDto>()
                .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
                .ForMember(u => u.Price, o => o.MapFrom(x => x.Price))
                .ForMember(u => u.Size, o => o.MapFrom(x => x.Size))
                .ForMember(u => u.SizeDtoId, o => o.MapFrom(x => x.SizeId))
                .ForMember(u => u.TreeDtoId, o => o.MapFrom(x => x.TreeId))
                .ForMember(u => u.Tree, o => o.MapFrom(x => x.Tree))
                ;

            CreateMap<TreeSizeAndPriceDto, TreeSizeAndPrice>()
                .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
                .ForMember(u => u.Price, o => o.MapFrom(x => x.Price))
                .ForMember(u => u.Size, o => o.MapFrom(x => x.Size))
                .ForMember(u => u.SizeId, o => o.MapFrom(x => x.SizeDtoId))
                .ForMember(u => u.TreeId, o => o.MapFrom(x => x.TreeDtoId))
                .ForMember(u => u.Tree, o => o.MapFrom(x => x.Tree))
                ;

            CreateMap<OrderDto, Order>()
                .ForMember(u=>u.Id, o=>o.MapFrom(x=>x.Id))
                .ForMember(u => u.User, o => o.MapFrom(x => x.User))
                .ForMember(u => u.FirstName, o => o.MapFrom(x => x.FirstName))
                .ForMember(u => u.UserEmail, o => o.MapFrom(x => x.UserEmail))
                .ForMember(u => u.LastName, o => o.MapFrom(x => x.LastName))
                .ForMember(u => u.Phone, o => o.MapFrom(x => x.Phone))
                .ForMember(u => u.Date, o => o.MapFrom(x => x.Date))
                .ForMember(u => u.GoodId, o => o.MapFrom(x => x.GoodId));

            CreateMap<Order, OrderDto>()
    .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
    .ForMember(u => u.User, o => o.MapFrom(x => x.User))
    .ForMember(u => u.FirstName, o => o.MapFrom(x => x.FirstName))
    .ForMember(u => u.UserEmail, o => o.MapFrom(x => x.UserEmail))
    .ForMember(u => u.LastName, o => o.MapFrom(x => x.LastName))
    .ForMember(u => u.Phone, o => o.MapFrom(x => x.Phone))
    .ForMember(u => u.Date, o => o.MapFrom(x => x.Date))
    .ForMember(u => u.GoodId, o => o.MapFrom(x => x.GoodId));

            CreateMap<DetailsAboutDeliveryDto, DetailsAboutDelivery>()
                .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
                .ForMember(u => u.Details, o => o.MapFrom(x => x.Details));


            CreateMap<DetailsAboutDelivery, DetailsAboutDeliveryDto>()
                .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
                .ForMember(u => u.Details, o => o.MapFrom(x => x.Details));

            CreateMap<DeliveryDto, Delivery>()
                .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
                .ForMember(u => u.Name, o => o.MapFrom(x => x.Name))
                .ForMember(u => u.DetailsAboutDeliveryId, o => o.MapFrom(x => x.DetailsAboutDeliveryDtoId));

            CreateMap<Delivery, DeliveryDto>()
                .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
                .ForMember(u => u.Name, o => o.MapFrom(x => x.Name))
                .ForMember(u => u.DetailsAboutDeliveryDtoId, o => o.MapFrom(x => x.DetailsAboutDeliveryId));

            CreateMap<Payment, PaymentDto>()
                .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
                .ForMember(u => u.Status, o => o.MapFrom(x => x.Status));

            CreateMap<PaymentDto, Payment>()
               .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
               .ForMember(u => u.Status, o => o.MapFrom(x => x.Status));

            CreateMap<OrderDeliveryDetailsAboutGoodPayment, OrderDeliveryDetailsAboutGoodPaymentDto>()
                .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
                .ForMember(u => u.DetailsAboutGoodDtoId, o => o.MapFrom(x => x.DetailsAboutGoodId))
                .ForMember(u => u.DeliveryDtoId, o => o.MapFrom(x => x.DeliveryId))
                .ForMember(u => u.OrderDtoId, o => o.MapFrom(x => x.OrderId))
                .ForMember(u => u.PaymentDtoId, o => o.MapFrom(x => x.PaymentId));

            CreateMap<OrderDeliveryDetailsAboutGoodPaymentDto, OrderDeliveryDetailsAboutGoodPayment>()
                .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
                .ForMember(u => u.DetailsAboutGoodId, o => o.MapFrom(x => x.DetailsAboutGoodDtoId))
                .ForMember(u => u.DeliveryId, o => o.MapFrom(x => x.DeliveryDtoId))
                .ForMember(u => u.OrderId, o => o.MapFrom(x => x.OrderDtoId))
                .ForMember(u => u.PaymentId, o => o.MapFrom(x => x.PaymentDtoId));

            CreateMap<ToyDto, Toy>()
               .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
               .ForMember(u => u.Name, o => o.MapFrom(x => x.Name))
               .ForMember(u => u.Description, o => o.MapFrom(x => x.Description))
               .ForMember(u => u.Price, o => o.MapFrom(x => x.Price));
           
            CreateMap<Toy, ToyDto>()
               .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
               .ForMember(u => u.Name, o => o.MapFrom(x => x.Name))
               .ForMember(u => u.Description, o => o.MapFrom(x => x.Description))
               .ForMember(u => u.Price, o => o.MapFrom(x => x.Price));

            CreateMap<Comment, CommentDto>()
               .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
               .ForMember(u => u.UserName, o => o.MapFrom(x => x.UserName))
               .ForMember(u => u.Text, o => o.MapFrom(x => x.Text))
               .ForMember(u => u.Date, o => o.MapFrom(x => x.Date));

            CreateMap<CommentDto, Comment>()
               .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
               .ForMember(u => u.UserName, o => o.MapFrom(x => x.UserName))
               .ForMember(u => u.Text, o => o.MapFrom(x => x.Text))
               .ForMember(u => u.Date, o => o.MapFrom(x => x.Date));

            CreateMap<AnswerToComment, AnswerToCommentDto>()
               .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
               .ForMember(u => u.CommentId, o => o.MapFrom(x => x.CommentId))
               .ForMember(u => u.UserName, o => o.MapFrom(x => x.UserName))
               .ForMember(u => u.Text, o => o.MapFrom(x => x.Text))
               .ForMember(u => u.Date, o => o.MapFrom(x => x.Date));

            CreateMap<AnswerToCommentDto, AnswerToComment>()
               .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
               .ForMember(u => u.CommentId, o => o.MapFrom(x => x.CommentId))
               .ForMember(u => u.UserName, o => o.MapFrom(x => x.UserName))
               .ForMember(u => u.Text, o => o.MapFrom(x => x.Text))
               .ForMember(u => u.Date, o => o.MapFrom(x => x.Date));

            CreateMap<BasketDto, Basket>()
               .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
               .ForMember(u => u.UserName, o => o.MapFrom(x => x.UserName));

            CreateMap<Basket, BasketDto>()
               .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
               .ForMember(u => u.UserName, o => o.MapFrom(x => x.UserName));

            CreateMap<BasketAndGoodDto, BasketAndGood>()
               .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
               .ForMember(u => u.BasketId, o => o.MapFrom(x => x.BasketId))
               .ForMember(u => u.GoodId, o => o.MapFrom(x => x.GoodId))
               .ForMember(u => u.DetailsAboutGoodId, o => o.MapFrom(x => x.DetailsAboutGoodId));

            CreateMap<BasketAndGood, BasketAndGoodDto>()
               .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
               .ForMember(u => u.BasketId, o => o.MapFrom(x => x.BasketId))
               .ForMember(u => u.GoodId, o => o.MapFrom(x => x.GoodId))
               .ForMember(u => u.DetailsAboutGoodId, o => o.MapFrom(x => x.DetailsAboutGoodId));

            CreateMap<DetailsAboutGoodDto, DetailsAboutGood>()
                .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
                .ForMember(u => u.Name, o => o.MapFrom(x => x.Name))
                .ForMember(u => u.Price, o => o.MapFrom(x => x.Price))
                .ForMember(u => u.Size, o => o.MapFrom(x => x.Size))
                .ForMember(u => u.Count, o => o.MapFrom(x => x.Count))
                .ForMember(u => u.TypeOfGood, o => o.MapFrom(x => x.TypeOfGood))
                .ForMember(u => u.Color, o => o.MapFrom(x => x.Color));

            CreateMap<DetailsAboutGood, DetailsAboutGoodDto>()
                .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
                .ForMember(u => u.Name, o => o.MapFrom(x => x.Name))
                .ForMember(u => u.Price, o => o.MapFrom(x => x.Price))
                .ForMember(u => u.Size, o => o.MapFrom(x => x.Size))
                .ForMember(u => u.Count, o => o.MapFrom(x => x.Count))
                .ForMember(u => u.TypeOfGood, o => o.MapFrom(x => x.TypeOfGood))
                .ForMember(u => u.Color, o => o.MapFrom(x => x.Color));

            CreateMap<Image, ImageDto>()
               .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
               .ForMember(u => u.ImageName, o => o.MapFrom(x => x.ImageName))
               .ForMember(u => u.ImagePath, o => o.MapFrom(x => x.ImagePath));

            CreateMap<ImageDto, Image>()
               .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
               .ForMember(u => u.ImageName, o => o.MapFrom(x => x.ImageName))
               .ForMember(u => u.ImagePath, o => o.MapFrom(x => x.ImagePath));

            CreateMap<ImageAndGood, ImageAndGoodDto>()
               .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
               .ForMember(u => u.ImageId, o => o.MapFrom(x => x.ImageId))
               .ForMember(u => u.GoodId, o => o.MapFrom(x => x.GoodId));

            CreateMap<ImageAndGoodDto, ImageAndGood>()
                .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
                .ForMember(u => u.ImageId, o => o.MapFrom(x => x.ImageId))
                .ForMember(u => u.GoodId, o => o.MapFrom(x => x.GoodId));

        }
    }
}
