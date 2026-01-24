using FluentMigrator;

namespace Inventoria.Infra.Data.Migrations;

[Migration(2)]
public class Migration_002_AddTransactions : Migration
{
    public override void Up()
    {
        // InventoryTransaction table
        Create.Table("InventoryTransaction")
            .WithColumn("TransactionId").AsInt32().PrimaryKey().Identity()
            .WithColumn("Date").AsString().NotNullable()
            .WithColumn("Type").AsInt32().NotNullable()
            .WithColumn("Notes").AsString().Nullable();

        // InventoryTransactionLine table
        Create.Table("InventoryTransactionLine")
            .WithColumn("LineId").AsInt32().PrimaryKey().Identity()
            .WithColumn("TransactionId").AsInt32().NotNullable()
                .ForeignKey("InventoryTransaction", "TransactionId").OnDelete(System.Data.Rule.Cascade)
            .WithColumn("ItemId").AsInt32().NotNullable()
                .ForeignKey("Item", "ItemId").OnDelete(System.Data.Rule.Cascade)
            .WithColumn("QuantityChange").AsInt32().NotNullable()
            .WithColumn("SourceInventoryId").AsInt32().Nullable()
                .ForeignKey("Inventory", "InventoryId")
            .WithColumn("TargetInventoryId").AsInt32().Nullable()
                .ForeignKey("Inventory", "InventoryId");
    }

    public override void Down()
    {
        Delete.Table("InventoryTransactionLine");
        Delete.Table("InventoryTransaction");
    }
}