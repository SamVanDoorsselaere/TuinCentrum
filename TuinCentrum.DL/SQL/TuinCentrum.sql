CREATE TABLE Klanten (
    KlantID INT PRIMARY KEY NOT NULL,
    Naam VARCHAR(100) NOT NULL,
    Adres VARCHAR(255) NOT NULL
);

CREATE TABLE Producten (
    ProductID INT PRIMARY KEY NOT NULL,
    NederlandseNaam VARCHAR(100) NOT NULL,
    WetenschappelijkeNaam VARCHAR(100) NOT NULL,
    Beschrijving TEXT NOT NULL,
    Prijs DECIMAL(10, 2) NOT NULL
);

CREATE TABLE Offertes (
    OfferteID INT PRIMARY KEY NOT NULL,
    KlantID INT NOT NULL,
    Datum DATE NOT NULL,
    FOREIGN KEY (KlantID) REFERENCES Klanten(KlantID)
);

CREATE TABLE OfferteProducten (
    OfferteID INT NOT NULL,
    ProductID INT NOT NULL,
    Aantal INT NOT NULL,
    PRIMARY KEY (OfferteID, ProductID),
    FOREIGN KEY (OfferteID) REFERENCES Offertes(OfferteID),
    FOREIGN KEY (ProductID) REFERENCES Producten(ProductID)
);


