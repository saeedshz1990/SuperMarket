using System;
using FluentMigrator;

namespace Supermarket.Migrations
{
    [Migration(202205012217)]
    public class _202205012217_InitialDatabase : Migration
    {
        public override void Up()
        {
            CreateCategoriesTable();
            
            CreateGoodsTable();
            
            CreateSalesInvoicesTable();
            
            CreateEntryDocumentsTable();

        }
        
        public override void Down()
        {
            Delete.Table("Categories");
            Delete.Table("Goods");
            Delete.Table("SalesInvoices");
            Delete.Table("EntryDocuments");
        }
        
        private void CreateEntryDocumentsTable()
        {
            Create.Table("EntryDocuments")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("Count").AsInt32().NotNullable()
                .WithColumn("SalesPrice").AsInt32().NotNullable()
                .WithColumn("SalesDate").AsDateTime().NotNullable()
                .WithColumn("GoodsId").AsInt32()
                .ForeignKey("Goods", "Id").NotNullable();
        }

        private void CreateSalesInvoicesTable()
        {
            Create.Table("SalesInvoices")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("CustomerName").AsString(50).NotNullable()
                .WithColumn("Count").AsInt32().NotNullable()
                .WithColumn("SalePrice").AsInt32().NotNullable()
                .WithColumn("SalesDate").AsDateTime().NotNullable()
                .WithColumn("GoodsId").AsInt32()
                .ForeignKey("Goods", "Id").NotNullable();
        }

        private void CreateGoodsTable()
        {
            Create.Table("Goods")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("Name").AsString(25).NotNullable()
                .WithColumn("Count").AsInt32().NotNullable()
                .WithColumn("MinimumInventory").AsInt32().NotNullable()
                .WithColumn("SalesPrice").AsInt32().NotNullable()
                .WithColumn("UniqueCode").AsString(25).NotNullable()
                .WithColumn("SalesInvoiceId").AsInt32().NotNullable()
                .WithColumn("EntryDocumentId").AsInt32().NotNullable()
                .WithColumn("CategoryId").AsInt32()
                .ForeignKey("Categories", "Id").NotNullable();
        }

        private void CreateCategoriesTable()
        {
            Create.Table("Categories")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("Name").AsString(25).NotNullable();
        }

    }
}
