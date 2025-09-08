-- User table
CREATE TABLE IF NOT EXISTS User (
    UserId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Email TEXT NOT NULL,
    PasswordHash TEXT NOT NULL
);

-- Inventory table
CREATE TABLE IF NOT EXISTS Inventory (
    InventoryId INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NOT NULL,
    Name TEXT NOT NULL,
    CreatedAt TEXT NOT NULL,
    Description TEXT,
    FOREIGN KEY (UserId) REFERENCES User(UserId) ON DELETE CASCADE
);

-- Item table
CREATE TABLE IF NOT EXISTS Item (
    ItemId INTEGER PRIMARY KEY AUTOINCREMENT,
    InventoryId INTEGER NOT NULL,
    Name TEXT NOT NULL,
    Quantity INTEGER NOT NULL,
    IsConsumable INTEGER NOT NULL,
    CreatedAt TEXT NOT NULL,
    Description TEXT,
    ResupplyThreshold INTEGER,
    FOREIGN KEY (InventoryId) REFERENCES Inventory(InventoryId) ON DELETE CASCADE
);

-- Tag table
CREATE TABLE IF NOT EXISTS Tag (
    TagId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL
);

-- ItemTag (many-to-many between Item and Tag)
CREATE TABLE IF NOT EXISTS ItemTag (
    ItemId INTEGER NOT NULL,
    TagId INTEGER NOT NULL,
    PRIMARY KEY (ItemId, TagId),
    FOREIGN KEY (ItemId) REFERENCES Item(ItemId) ON DELETE CASCADE,
    FOREIGN KEY (TagId) REFERENCES Tag(TagId) ON DELETE CASCADE
);

-- MaintenanceSettings table
CREATE TABLE IF NOT EXISTS MaintenanceSettings (
    ItemId INTEGER PRIMARY KEY,
    IntervalMonths INTEGER,
    NextDueDate TEXT,
    FOREIGN KEY (ItemId) REFERENCES Item(ItemId) ON DELETE CASCADE
);

-- MaintenanceRecord table
CREATE TABLE IF NOT EXISTS MaintenanceRecord (
    MaintenanceId INTEGER PRIMARY KEY AUTOINCREMENT,
    ItemId INTEGER NOT NULL,
    DatePerformed TEXT NOT NULL,
    Cost REAL,
    Notes TEXT,
    FOREIGN KEY (ItemId) REFERENCES Item(ItemId) ON DELETE CASCADE
);