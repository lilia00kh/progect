using System;
using System.Collections.Generic;
using System.Text;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _repositoryContext;
        private UserRepository _userRepository;
        private ToyRepository _toyRepository;
        private TreeRepository _treeRepository;
        private SizeRepository _sizeRepository;
        private CommentRepository _commentRepository;
        private AnswerToCommentRepository _answerToCommentRepository;
        private BasketRepository _basketRepository;
        private BasketAndGoodRepository _basketAndGoodRepository;
        private TreeSizeAndPriceRepository _treeSizeAndPriceRepository;
        private DetailsAboutGoodRepository _detailsAboutGoodRepository;
        private OrderRepository _orderRepository;
        private DetailsAboutDeliveryRepository _detailsAboutDeliveryRepository;
        private DeliveryRepository _deliveryRepository;
        private OrderDeliveryDetailsAboutGoodPaymentRepository _orderDeliveryDetailsAboutGoodPaymentRepository;
        private PaymentRepository _paymentRepository;
        private ImageRepository _imageRepository;
        private ImageAndGoodRepository _imageAndGoodRepository;
        private RecomendationRepository _recomendationRepository;
        private readonly UserManager<User> _userManager;

        public UnitOfWork(ApplicationDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public ITreeRepository Tree
        {
            get
            {
                if (_treeRepository == null)
                    _treeRepository = new TreeRepository(_repositoryContext);

                return _treeRepository;
            }
        }

        public IToyRepository Toy
        {
            get
            {
                if (_toyRepository == null)
                    _toyRepository = new ToyRepository(_repositoryContext);

                return _toyRepository;
            }
        }

        public ISizeRepository Size
        {
            get
            {
                if (_sizeRepository == null)
                    _sizeRepository = new SizeRepository(_repositoryContext);

                return _sizeRepository;
            }
        }

        public ITreeSizeAndPriceRepository TreeSizeAndPrice
        {
            get
            {
                if (_treeSizeAndPriceRepository == null)
                    _treeSizeAndPriceRepository = new TreeSizeAndPriceRepository(_repositoryContext);

                return _treeSizeAndPriceRepository;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_repositoryContext);

                return _userRepository;
            }
        }

        public ICommentRepository Comment
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new CommentRepository(_repositoryContext);

                return _commentRepository;
            }
        }

        public IAnswerToCommentRepository AnswerToComment
        {
            get
            {
                if (_answerToCommentRepository == null)
                    _answerToCommentRepository = new AnswerToCommentRepository(_repositoryContext);

                return _answerToCommentRepository;
            }
        }

        public IBasketRepository Basket
        {
            get
            {
                if (_basketRepository == null)
                    _basketRepository = new BasketRepository(_repositoryContext);

                return _basketRepository;
            }
        }

        public IBasketAndGoodRepository BasketAndGood
        {
            get
            {
                if (_basketAndGoodRepository == null)
                    _basketAndGoodRepository = new BasketAndGoodRepository(_repositoryContext);

                return _basketAndGoodRepository;
            }
        }

        public IDetailsAboutGoodRepository DetailsAboutGood
        {
            get
            {
                if (_detailsAboutGoodRepository == null)
                    _detailsAboutGoodRepository = new DetailsAboutGoodRepository(_repositoryContext);

                return _detailsAboutGoodRepository;
            }
        }

        public IOrderRepository Order
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_repositoryContext);

                return _orderRepository;
            }
        }

        public IDeliveryRepository Delivery
        {
            get
            {
                if (_deliveryRepository == null)
                    _deliveryRepository = new DeliveryRepository(_repositoryContext);

                return _deliveryRepository;
            }
        }

        public IOrderDeliveryDetailsAboutGoodPaymentRepository OrderDeliveryDetailsAboutGoodPayment
        {
            get
            {
                if (_orderDeliveryDetailsAboutGoodPaymentRepository == null)
                    _orderDeliveryDetailsAboutGoodPaymentRepository = new OrderDeliveryDetailsAboutGoodPaymentRepository(_repositoryContext);

                return _orderDeliveryDetailsAboutGoodPaymentRepository;
            }
        }

        public IDetailsAboutDeliveryRepository DetailsAboutDelivery
        {
            get
            {
                if (_detailsAboutDeliveryRepository == null)
                    _detailsAboutDeliveryRepository = new DetailsAboutDeliveryRepository(_repositoryContext);

                return _detailsAboutDeliveryRepository;
            }
        }

        public IPaymentRepository Payment
        {
            get
            {
                if (_paymentRepository == null)
                    _paymentRepository = new PaymentRepository(_repositoryContext);

                return _paymentRepository;
            }
        }

        public IImageRepository Image
        {
            get
            {
                if (_imageRepository == null)
                    _imageRepository = new ImageRepository(_repositoryContext);

                return _imageRepository;
            }
        }
        public IImageAndGoodRepository ImageAndGood
        {
            get
            {
                if (_imageAndGoodRepository == null)
                    _imageAndGoodRepository = new ImageAndGoodRepository(_repositoryContext);

                return _imageAndGoodRepository;
            }
        }

        public IRecomendationRepository Recomendation
        {
            get
            {
                if (_recomendationRepository == null)
                    _recomendationRepository = new RecomendationRepository(_repositoryContext);

                return _recomendationRepository;
            }
        }
        public void Save() => _repositoryContext.SaveChanges();
    }
}
