using PL.Models;
using BLL.Infrastracture;
using System.Linq;

namespace PL.Validations
{
    public class Validator
    {
        public static void ToyValidator(ToyModel toyModel)
        {
            if (toyModel == null)
            {
                throw new CustomException( "Toy model is empty","");
            }

            if (toyModel.Name == "" || toyModel.Description == "")
            {
                throw new CustomException("Toy name or description can not be empty","");
            }

            if (toyModel.Price <= 0)
            {
                throw new CustomException("Toy price can not be less than null","");
            }
        }

        public static bool TreeTypeValidator(string treeType)
        {
            if (treeType == "литі" || treeType == "комбіновані" || treeType == "з плівки пвх" || treeType == "засніжені" || treeType == "all")
            {
                return true;
            }
            return false;
        }

        public static void TreeValidator(TreeModel treeModel)
        {
            if (treeModel == null)
            {
                throw new CustomException("Tree model is empty","");
            }

            if (treeModel.PriceAndSizeModels.Count == 0)
            {
                throw new CustomException("Price and size models are empty", "");
            }

            if (treeModel.PriceAndSizeModels.Any(priceAndSizeModel => priceAndSizeModel.Price == 0 || priceAndSizeModel.Size == 0))
            {
                throw new CustomException("Price and size can`t be null", "");
            }

            if (treeModel.PriceAndSizeModels.Any(priceAndSizeModel => priceAndSizeModel.Price < 0 || priceAndSizeModel.Size < 0))
            {
                throw new CustomException("Price and size must be more than 0", "");
            }
        }
    }
}
