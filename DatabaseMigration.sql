-- ============================================================
-- SMART WORKSPACE DATABASE - COMPLETE SINGLE FILE
-- Run this entire script once in SQL Server Management Studio
-- ============================================================

-- Create Database
CREATE DATABASE SmartWorkspaceDB;
GO

USE SmartWorkspaceDB;
GO

-- ============================================================
-- TABLE: Member
-- ============================================================

CREATE TABLE Member (
    MemberID              INT IDENTITY(1,1) PRIMARY KEY,
    FullName              NVARCHAR(100) NOT NULL,
    DigitalID             NVARCHAR(50) NULL,
    CorporateAffiliation  NVARCHAR(150) NULL,
    TotalReservedHours    INT NOT NULL DEFAULT 0
);
GO

-- ============================================================
-- TABLE: Workspace
-- ============================================================

CREATE TABLE Workspace (
    WorkspaceID   INT IDENTITY(1,1) PRIMARY KEY,
    WorkspaceType NVARCHAR(50) NOT NULL,
    HubName       NVARCHAR(100) NOT NULL,
    PricePerHour  DECIMAL(10,2) NOT NULL,
    Status        NVARCHAR(20) NOT NULL
);
GO

-- ============================================================
-- TABLE: Reservation
-- ============================================================

CREATE TABLE Reservation (
    ReservationID   INT IDENTITY(1,1) PRIMARY KEY,
    MemberID        INT NOT NULL,
    WorkspaceID     INT NOT NULL,
    ReservationDate DATE NOT NULL,
    HoursReserved   INT NOT NULL,
    Status          NVARCHAR(20) NOT NULL DEFAULT 'Running',

    CONSTRAINT CK_Reservation_Status
        CHECK (Status IN ('Running', 'Finished')),

    CONSTRAINT FK_Reservation_Member
        FOREIGN KEY (MemberID)
        REFERENCES Member(MemberID)
        ON DELETE CASCADE,

    CONSTRAINT FK_Reservation_Workspace
        FOREIGN KEY (WorkspaceID)
        REFERENCES Workspace(WorkspaceID)
        ON DELETE CASCADE
);
GO

-- ============================================================
-- TABLE: Equipment
-- ============================================================

CREATE TABLE Equipment (
    EquipmentID    INT IDENTITY(1,1) PRIMARY KEY,
    EquipmentName  NVARCHAR(100) NOT NULL,
    EquipmentType  NVARCHAR(50) NOT NULL DEFAULT 'General',
    HubName        NVARCHAR(100) NOT NULL DEFAULT 'Unknown'
);
GO

-- ============================================================
-- TABLE: ReservationEquipment
-- ============================================================

CREATE TABLE ReservationEquipment (
    ID            INT IDENTITY(1,1) PRIMARY KEY,
    ReservationID INT NOT NULL,
    EquipmentID   INT NOT NULL,

    CONSTRAINT FK_RE_Reservation
        FOREIGN KEY (ReservationID)
        REFERENCES Reservation(ReservationID)
        ON DELETE CASCADE,

    CONSTRAINT FK_RE_Equipment
        FOREIGN KEY (EquipmentID)
        REFERENCES Equipment(EquipmentID)
        ON DELETE CASCADE
);
GO

-- ============================================================
-- SAMPLE DATA: Member
-- ============================================================

INSERT INTO Member
(FullName, DigitalID, CorporateAffiliation)
VALUES
('Ahmed Hassan', 'D001', 'TechCo'),
('Sara Ali', 'D002', 'StartupHub'),
('Omar Khaled', 'D003', 'FreelanceInc'),
('Nora Samir', 'D004', 'DesignStudio'),
('Karim Mostafa', 'D005', NULL);
GO

-- ============================================================
-- SAMPLE DATA: Workspace
-- ============================================================

INSERT INTO Workspace
(WorkspaceType, HubName, PricePerHour, Status)
VALUES
('Private Office', 'Downtown Hub', 50.00, 'Available'),
('Open Desk', 'Cairo Hub', 20.00, 'Available'),
('Meeting Room', 'Giza Hub', 80.00, 'Available'),
('Private Office', 'Cairo Hub', 55.00, 'Available'),
('Open Desk', 'Downtown Hub', 18.00, 'Available'),
('Meeting Room', 'Alexandria Hub', 75.00, 'Available');
GO

-- ============================================================
-- SAMPLE DATA: Equipment
-- ============================================================

INSERT INTO Equipment
(EquipmentName, EquipmentType, HubName)
VALUES
('Projector', 'AV', 'Downtown Hub'),
('Whiteboard', 'Office', 'Cairo Hub'),
('Video Conferencing System', 'AV', 'Giza Hub'),
('Standing Desk', 'Furniture', 'Cairo Hub'),
('Printer', 'Office', 'Downtown Hub');
GO

-- ============================================================
-- SAMPLE DATA: Reservation
-- ============================================================

INSERT INTO Reservation
(MemberID, WorkspaceID, ReservationDate, HoursReserved, Status)
VALUES
(1, 1, '2025-01-10', 3, 'Running'),
(2, 2, '2025-01-11', 5, 'Running'),
(1, 3, '2025-01-12', 2, 'Finished'),
(3, 4, '2025-01-13', 4, 'Running'),
(2, 5, '2025-01-14', 6, 'Finished');
GO

-- ============================================================
-- SAMPLE DATA: ReservationEquipment
-- ============================================================

INSERT INTO ReservationEquipment
(ReservationID, EquipmentID)
VALUES
(1, 1),
(1, 2),
(3, 3),
(4, 2),
(5, 1);
GO

-- ============================================================
-- TRIGGER: Update TotalReservedHours Automatically
-- ============================================================

CREATE OR ALTER TRIGGER trg_UpdateTotalHours
ON Reservation
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE m
    SET m.TotalReservedHours = ISNULL(
        (
            SELECT SUM(r.HoursReserved)
            FROM Reservation r
            WHERE r.MemberID = m.MemberID
        ), 0
    )
    FROM Member m
    WHERE m.MemberID IN (
        SELECT MemberID FROM inserted
        UNION
        SELECT MemberID FROM deleted
    );
END;
GO

-- ============================================================
-- INITIAL BACKFILL FOR EXISTING DATA
-- ============================================================

UPDATE m
SET m.TotalReservedHours = ISNULL(
    (
        SELECT SUM(r.HoursReserved)
        FROM Reservation r
        WHERE r.MemberID = m.MemberID
    ), 0
)
FROM Member m;
GO

PRINT '================================================';
PRINT 'SmartWorkspaceDB created successfully.';
PRINT 'Database is fully ready to use.';
PRINT '================================================';
GO