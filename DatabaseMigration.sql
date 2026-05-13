-- ============================================================
-- SMART WORKSPACE DATABASE - COMPLETE SINGLE FILE (FIXED)
-- Fix: Replaced AFTER trigger on Reservation with stored
--      procedures to resolve the SQL Server error:
--      "Target table cannot have enabled triggers if the
--       statement contains an OUTPUT clause without INTO."
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
-- HELPER: Internal procedure to recalculate TotalReservedHours
-- Called by all Reservation stored procedures below
-- ============================================================

CREATE OR ALTER PROCEDURE sp_RefreshMemberHours
    @MemberID INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Member
    SET TotalReservedHours = ISNULL(
        (SELECT SUM(HoursReserved) FROM Reservation WHERE MemberID = @MemberID),
        0
    )
    WHERE MemberID = @MemberID;
END;
GO

-- ============================================================
-- STORED PROCEDURE: Insert Reservation
-- Returns the new ReservationID via SCOPE_IDENTITY()
-- Use this instead of a direct INSERT to avoid the OUTPUT
-- clause conflict that was caused by the AFTER trigger.
-- ============================================================

CREATE OR ALTER PROCEDURE sp_InsertReservation
    @MemberID        INT,
    @WorkspaceID     INT,
    @ReservationDate DATE,
    @HoursReserved   INT,
    @Status          NVARCHAR(20) = 'Running'
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Reservation (MemberID, WorkspaceID, ReservationDate, HoursReserved, Status)
    VALUES (@MemberID, @WorkspaceID, @ReservationDate, @HoursReserved, @Status);

    DECLARE @NewID INT = SCOPE_IDENTITY();

    -- Recalculate total hours for this member
    EXEC sp_RefreshMemberHours @MemberID;

    -- Return the new ReservationID to the caller
    SELECT @NewID AS ReservationID;
END;
GO

-- ============================================================
-- STORED PROCEDURE: Update Reservation
-- ============================================================

CREATE OR ALTER PROCEDURE sp_UpdateReservation
    @ReservationID   INT,
    @MemberID        INT,
    @WorkspaceID     INT,
    @ReservationDate DATE,
    @HoursReserved   INT,
    @Status          NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    -- Capture old MemberID in case it changed
    DECLARE @OldMemberID INT;
    SELECT @OldMemberID = MemberID FROM Reservation WHERE ReservationID = @ReservationID;

    UPDATE Reservation
    SET MemberID        = @MemberID,
        WorkspaceID     = @WorkspaceID,
        ReservationDate = @ReservationDate,
        HoursReserved   = @HoursReserved,
        Status          = @Status
    WHERE ReservationID = @ReservationID;

    -- Recalculate hours for both old and new member (handles member reassignment)
    EXEC sp_RefreshMemberHours @OldMemberID;
    IF @OldMemberID <> @MemberID
        EXEC sp_RefreshMemberHours @MemberID;
END;
GO

-- ============================================================
-- STORED PROCEDURE: Delete Reservation
-- ============================================================

CREATE OR ALTER PROCEDURE sp_DeleteReservation
    @ReservationID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MemberID INT;
    SELECT @MemberID = MemberID FROM Reservation WHERE ReservationID = @ReservationID;

    DELETE FROM Reservation WHERE ReservationID = @ReservationID;

    -- Recalculate total hours after deletion
    EXEC sp_RefreshMemberHours @MemberID;
END;
GO

-- ============================================================
-- SAMPLE DATA: Member
-- ============================================================

INSERT INTO Member (FullName, DigitalID, CorporateAffiliation)
VALUES
('Ahmed Hassan', 'D001', 'TechCo'),
('Sara Ali',     'D002', 'StartupHub'),
('Omar Khaled',  'D003', 'FreelanceInc'),
('Nora Samir',   'D004', 'DesignStudio'),
('Karim Mostafa','D005', NULL);
GO

-- ============================================================
-- SAMPLE DATA: Workspace
-- ============================================================

INSERT INTO Workspace (WorkspaceType, HubName, PricePerHour, Status)
VALUES
('Private Office', 'Downtown Hub',    50.00, 'Available'),
('Open Desk',      'Cairo Hub',       20.00, 'Available'),
('Meeting Room',   'Giza Hub',        80.00, 'Available'),
('Private Office', 'Cairo Hub',       55.00, 'Available'),
('Open Desk',      'Downtown Hub',    18.00, 'Available'),
('Meeting Room',   'Alexandria Hub',  75.00, 'Available');
GO

-- ============================================================
-- SAMPLE DATA: Equipment
-- ============================================================

INSERT INTO Equipment (EquipmentName, EquipmentType, HubName)
VALUES
('Projector',                  'AV',        'Downtown Hub'),
('Whiteboard',                 'Office',    'Cairo Hub'),
('Video Conferencing System',  'AV',        'Giza Hub'),
('Standing Desk',              'Furniture', 'Cairo Hub'),
('Printer',                    'Office',    'Downtown Hub');
GO

-- ============================================================
-- SAMPLE DATA: Reservation
-- Uses stored procedure so TotalReservedHours is kept in sync
-- ============================================================

EXEC sp_InsertReservation 1, 1, '2025-01-10', 3, 'Running';
EXEC sp_InsertReservation 2, 2, '2025-01-11', 5, 'Running';
EXEC sp_InsertReservation 1, 3, '2025-01-12', 2, 'Finished';
EXEC sp_InsertReservation 3, 4, '2025-01-13', 4, 'Running';
EXEC sp_InsertReservation 2, 5, '2025-01-14', 6, 'Finished';
GO

-- ============================================================
-- SAMPLE DATA: ReservationEquipment
-- ============================================================

INSERT INTO ReservationEquipment (ReservationID, EquipmentID)
VALUES
(1, 1),
(1, 2),
(3, 3),
(4, 2),
(5, 1);
GO

-- ============================================================
-- NOTE: The AFTER trigger (trg_UpdateTotalHours) has been
-- intentionally REMOVED. SQL Server does not allow an AFTER
-- trigger on a table that is the target of a DML statement
-- using OUTPUT without INTO — which is what caused the error.
--
-- TotalReservedHours is now maintained by:
--   sp_InsertReservation  ? use instead of direct INSERT
--   sp_UpdateReservation  ? use instead of direct UPDATE
--   sp_DeleteReservation  ? use instead of direct DELETE
--
-- In your application, replace direct SQL like:
--   INSERT INTO Reservation (...) OUTPUT INSERTED.ReservationID VALUES (...)
-- With a stored procedure call:
--   EXEC sp_InsertReservation @MemberID, @WorkspaceID, @Date, @Hours, @Status
-- The procedure returns the new ReservationID via SELECT.
-- ============================================================

PRINT '================================================';
PRINT 'SmartWorkspaceDB created successfully (FIXED).';
PRINT 'OUTPUT clause conflict resolved via stored procs.';
PRINT 'Database is fully ready to use.';
PRINT '================================================';
GO