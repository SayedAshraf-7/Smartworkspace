-- =========================================
-- DROP & CREATE DATABASE
-- =========================================

IF DB_ID('smart_workspace_hub') IS NOT NULL
BEGIN
    ALTER DATABASE smart_workspace_hub SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE smart_workspace_hub;
END
GO

CREATE DATABASE smart_workspace_hub;
GO

USE smart_workspace_hub;
GO

-- =========================================
-- TABLES
-- =========================================

CREATE TABLE Hubs (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255) NOT NULL,
    specific_district NVARCHAR(255) NOT NULL,
    architectural_layout NVARCHAR(MAX) NOT NULL
);
GO

CREATE TABLE Workspaces (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,

    type NVARCHAR(50) NOT NULL
        CHECK (type IN (
            'private_office',
            'open_desk',
            'meeting_pod'
        )),

    status NVARCHAR(50) NOT NULL
        CHECK (status IN (
            'available',
            'reserved',
            'maintenance'
        )),

    hub_id BIGINT NOT NULL,

    hourly_rate BIGINT NOT NULL,
    daily_rate BIGINT NOT NULL,

    CONSTRAINT FK_Workspaces_Hubs
        FOREIGN KEY (hub_id)
        REFERENCES Hubs(id)
        ON DELETE CASCADE
);
GO

CREATE TABLE Members (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,

    name NVARCHAR(255) NOT NULL,

    corporate_affiliation NVARCHAR(255) NOT NULL,

    digital_identification NVARCHAR(255) NOT NULL UNIQUE
);
GO

CREATE TABLE Equipments (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,

    type NVARCHAR(50) NOT NULL
        CHECK (type IN (
            'projector',
            'standing_desk',
            'monitor',
            'speaker',
            'webcam'
        ))
);
GO

CREATE TABLE Reservations (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,

    pricing_type NVARCHAR(50) NOT NULL
        CHECK (pricing_type IN (
            'hourly',
            'daily'
        )),

    duration BIGINT NOT NULL,

    start_date DATETIME NOT NULL,
    end_date DATETIME NOT NULL,

    member_id BIGINT NOT NULL,
    workspace_id BIGINT NOT NULL,

    number_of_equipments BIGINT NOT NULL DEFAULT 0,

    status NVARCHAR(20) NOT NULL DEFAULT 'running'
        CHECK (status IN (
            'running',
            'finished',
            'cancelled'
        )),

    CONSTRAINT CK_Reservations_DateRange
        CHECK (end_date > start_date),

    CONSTRAINT FK_Reservations_Members
        FOREIGN KEY (member_id)
        REFERENCES Members(id)
        ON DELETE CASCADE,

    CONSTRAINT FK_Reservations_Workspaces
        FOREIGN KEY (workspace_id)
        REFERENCES Workspaces(id)
        ON DELETE CASCADE
);
GO

CREATE TABLE Reserved_equipments (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,

    equipment_id BIGINT NOT NULL,
    reservation_id BIGINT NOT NULL,

    duration BIGINT NOT NULL,

    CONSTRAINT FK_ReservedEquipments_Equipments
        FOREIGN KEY (equipment_id)
        REFERENCES Equipments(id)
        ON DELETE CASCADE,

    CONSTRAINT FK_ReservedEquipments_Reservations
        FOREIGN KEY (reservation_id)
        REFERENCES Reservations(id)
        ON DELETE CASCADE
);
GO

-- =========================================
-- TRIGGERS
-- =========================================

CREATE TRIGGER trg_reserved_equipment_insert
ON Reserved_equipments
AFTER INSERT
AS
BEGIN
    UPDATE Reservations
    SET number_of_equipments = (
        SELECT COUNT(*)
        FROM Reserved_equipments re
        WHERE re.reservation_id = Reservations.id
    )
    WHERE id IN (
        SELECT DISTINCT reservation_id
        FROM inserted
    );
END;
GO

CREATE TRIGGER trg_reserved_equipment_delete
ON Reserved_equipments
AFTER DELETE
AS
BEGIN
    UPDATE Reservations
    SET number_of_equipments = (
        SELECT COUNT(*)
        FROM Reserved_equipments re
        WHERE re.reservation_id = Reservations.id
    )
    WHERE id IN (
        SELECT DISTINCT reservation_id
        FROM deleted
    );
END;
GO

-- =========================================
-- DUMMY DATA
-- =========================================

-- Hubs
INSERT INTO Hubs
(name, specific_district, architectural_layout)
VALUES
('Downtown Tech Hub', 'Nasr City', 'Modern glass architecture with open collaboration areas'),
('Creative Space Hub', 'Maadi', 'Industrial minimalist design with private offices'),
('Innovation Hub', 'New Cairo', 'Smart eco-friendly workspace with modular rooms');
GO

-- Workspaces
INSERT INTO Workspaces
(type, status, hub_id, hourly_rate, daily_rate)
VALUES
('private_office', 'available', 1, 100, 700),
('open_desk', 'available', 1, 50, 300),
('meeting_pod', 'reserved', 2, 80, 500),
('private_office', 'maintenance', 3, 120, 850),
('open_desk', 'available', 2, 40, 250);
GO

-- Members
INSERT INTO Members
(name, corporate_affiliation, digital_identification)
VALUES
('Alaa Okasha', 'Google', 'DIGI-1001'),
('Sarah Ahmed', 'Microsoft', 'DIGI-1002'),
('Omar Khaled', 'Amazon', 'DIGI-1003'),
('Mona Adel', 'IBM', 'DIGI-1004');
GO

-- Equipments
INSERT INTO Equipments (type)
VALUES
('projector'),
('standing_desk'),
('monitor'),
('speaker'),
('webcam');
GO

-- Reservations
INSERT INTO Reservations
(
    pricing_type,
    duration,
    start_date,
    end_date,
    member_id,
    workspace_id,
    status
)
VALUES
(
    'hourly',
    3,
    '2026-05-17 09:00:00',
    '2026-05-17 12:00:00',
    1,
    1,
    'running'
),
(
    'hourly',
    10,
    '2026-05-18 08:00:00',
    '2026-05-18 18:00:00',
    2,
    3,
    'running'
),
(
    'hourly',
    5,
    '2026-05-19 10:00:00',
    '2026-05-19 15:00:00',
    3,
    2,
    'running'
);
GO

-- Reserved Equipments
INSERT INTO Reserved_equipments
(equipment_id, reservation_id, duration)
VALUES
(1, 1, 3),
(2, 1, 3),

(3, 2, 1),

(1, 3, 5),
(4, 3, 5),
(5, 3, 5);
GO