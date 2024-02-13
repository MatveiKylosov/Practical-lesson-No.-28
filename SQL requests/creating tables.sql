CREATE TABLE ComputerClub (
    ClubID INT AUTO_INCREMENT,
    Name VARCHAR(255),
    Address VARCHAR(255),
    OpeningHours VARCHAR(255),
    PRIMARY KEY (ClubID)
);

CREATE TABLE ComputerRental (
    RentalID INT AUTO_INCREMENT,
    DateAndTime DATETIME,
    ClientFullName VARCHAR(255),
    ClubID INT,
    PRIMARY KEY (RentalID),
    FOREIGN KEY (ClubID) REFERENCES ComputerClub(ClubID)
);
