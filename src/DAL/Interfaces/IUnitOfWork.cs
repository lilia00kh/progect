using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        ITreeRepository Tree { get; }
        IToyRepository Toy { get; }
        ITreeSizeAndPriceRepository TreeSizeAndPrice { get; }
        ISizeRepository Size { get; }
        ICommentRepository Comment { get; }
        IBasketRepository Basket { get; }
        IBasketAndGoodRepository BasketAndGood { get; }
        IAnswerToCommentRepository AnswerToComment { get; }
        IDetailsAboutGoodRepository DetailsAboutGood { get; }
        IOrderRepository Order { get; }
        IDeliveryRepository Delivery { get; }
        IOrderDeliveryDetailsAboutGoodPaymentRepository OrderDeliveryDetailsAboutGoodPayment { get; }
        IDetailsAboutDeliveryRepository DetailsAboutDelivery { get; }
        IPaymentRepository Payment { get; }
        IImageRepository Image { get; }
        IImageAndGoodRepository ImageAndGood { get; }
        IRecomendationRepository Recomendation { get; }

        void Save();
    }
}
