using FluentMigrator;

namespace Inventoria.Infra.Data.Migrations;

[Migration(1)]
public class Migration_001_InitialSchema : Migration
{
    public override void Up()
    {
        // User table
        Create.Table("User")
            .WithColumn("UserId").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("Email").AsString().NotNullable()
            .WithColumn("PasswordHash").AsString().NotNullable();

        // Inventory table
        Create.Table("Inventory")
            .WithColumn("InventoryId").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserId").AsInt32().NotNullable()
                .ForeignKey("User", "UserId").OnDelete(System.Data.Rule.Cascade)
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("CreatedAt").AsString().NotNullable()
            .WithColumn("Description").AsString().Nullable();

        // Item table
        Create.Table("Item")
            .WithColumn("ItemId").AsInt32().PrimaryKey().Identity()
            .WithColumn("InventoryId").AsInt32().NotNullable()
                .ForeignKey("Inventory", "InventoryId").OnDelete(System.Data.Rule.Cascade)
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("Quantity").AsInt32().NotNullable()
            .WithColumn("IsConsumable").AsInt32().NotNullable()
            .WithColumn("CreatedAt").AsString().NotNullable()
            .WithColumn("Description").AsString().Nullable()
            .WithColumn("ResupplyThreshold").AsInt32().Nullable();

        // Tag table
        Create.Table("Tag")
            .WithColumn("TagId").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString().NotNullable();

        // ItemTag (many-to-many) - Use raw SQL for composite PK
        Execute.Sql(@"
            CREATE TABLE ItemTag (
                ItemId INTEGER NOT NULL,
                TagId INTEGER NOT NULL,
                PRIMARY KEY (ItemId, TagId),
                FOREIGN KEY (ItemId) REFERENCES Item(ItemId) ON DELETE CASCADE,
                FOREIGN KEY (TagId) REFERENCES Tag(TagId) ON DELETE CASCADE
            );
        ");

        // MaintenanceSettings table
        Create.Table("MaintenanceSettings")
            .WithColumn("ItemId").AsInt32().PrimaryKey()
                .ForeignKey("Item", "ItemId").OnDelete(System.Data.Rule.Cascade)
            .WithColumn("IntervalMonths").AsInt32().Nullable()
            .WithColumn("NextDueDate").AsString().Nullable();

        // MaintenanceRecord table
        Create.Table("MaintenanceRecord")
            .WithColumn("MaintenanceId").AsInt32().PrimaryKey().Identity()
            .WithColumn("ItemId").AsInt32().NotNullable()
                .ForeignKey("Item", "ItemId").OnDelete(System.Data.Rule.Cascade)
            .WithColumn("DatePerformed").AsString().NotNullable()
            .WithColumn("Cost").AsDouble().Nullable()
            .WithColumn("Notes").AsString().Nullable();
    }

    public override void Down()
    {
        Delete.Table("MaintenanceRecord");
        Delete.Table("MaintenanceSettings");
        Delete.Table("ItemTag");
        Delete.Table("Tag");
        Delete.Table("Item");
        Delete.Table("Inventory");
        Delete.Table("User");
    }
}