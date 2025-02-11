namespace SmartFactoryApplication.Utils
{
    public static class ConstantMessages
    {
        #region Inventory
        // Material
        public const string MATERIAL_NOT_FOUND = "Material não encontrado.";
        public const string FAILED_DELETE_MATERIAL = "Falha ao excluir material";
        public const string MATERIAL_DELETED_SUCCESSFULLY = "Material excluído com sucesso!";
        public const string DUPLICATE_MATERIAL_CODE = "Já existe um material com esse código.";
        public const string DUPLICATE_MATERIAL_NAME = "Já existe um material com esse nome.";
        public const string REQUIRED_MATERIAL_NAME = "O nome do material é obrigatório.";
        public const string REQUIRED_MATERIAL_CODE = "O código do material é obrigatório.";

        // Product
        public const string REQUIRED_PRODUCT_NAME = "O nome do produto é obrigatório.";
        public const string REQUIRED_PRODUCT_CODE = "O código do produto é obrigatório.";
        public const string REQUIRED_PRODUCT_CATEGORY = "A categoria do produto é obrigatório.";
        public const string DUPLICATE_PRODUCT_CODE = "Já existe um produto com esse código.";
        public const string DUPLICATE_PRODUCT_NAME = "Já existe um produto com esse nome.";
        #endregion

        // General
        public const string REQUIRED_UNIT_OF_MESUARE = "A unidade de medida é obrigatória.";
        public const string QUANTITY_STOCK_CANNOT_BE_NEGATIVE = "A quantidade em estoque não pode ser negativa.";
        public const string UNIT_PRICE_CANNOT_BE_NEGATIVE = "O preço unitário não pode ser negativo.";
        public const string SUCCESS = "Sucesso";
    }
}
